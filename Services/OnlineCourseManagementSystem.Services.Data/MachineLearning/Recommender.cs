namespace OnlineCourseManagementSystem.Services.Data.MachineLearning
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.ML;
    using Microsoft.ML.Trainers;

    public static class Recommender
    {
        public static void TrainModel(string inputFile, string modelFile)
        {
            // Create MLContext to be shared across the model creation workflow objects
            var context = new MLContext();

            // Load data
            IDataView trainingDataView = context.Data.LoadFromTextFile<UserInCourse>(
                inputFile,
                hasHeader: true,
                separatorChar: ',');

            // Build & train model
            IEstimator<ITransformer> estimator = context.Transforms.Conversion
                .MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: nameof(UserInCourse.UserId)).Append(
                    context.Transforms.Conversion.MapValueToKey(outputColumnName: "courseIdEncoded", inputColumnName: nameof(UserInCourse.CourseId)));
            var options = new MatrixFactorizationTrainer.Options
            {
                LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "courseIdEncoded",
                LabelColumnName = nameof(UserInCourse.Label),
                Alpha = 0.1,
                Lambda = 0.5,
                NumberOfIterations = 50,
            };

            var trainerEstimator = estimator.Append(context.Recommendation().Trainers.MatrixFactorization(options));
            ITransformer model = trainerEstimator.Fit(trainingDataView);

            // Save model
            context.Model.Save(model, trainingDataView.Schema, modelFile);
        }

        public static IEnumerable<UserInCourseScore> TestModel(string modelFile, IEnumerable<UserInCourse> testModelData)
        {
            var context = new MLContext();
            var model = context.Model.Load(modelFile, out _);
            var predictionEngine = context.Model.CreatePredictionEngine<UserInCourse, UserInCourseScore>(model);
            foreach (var testInput in testModelData)
            {
                var prediction = predictionEngine.Predict(testInput);
                Console.WriteLine($"User: {testInput.UserId}, Course: {testInput.CourseId}, Score: {prediction.Score}");
                yield return prediction;
            }
        }
    }
}
