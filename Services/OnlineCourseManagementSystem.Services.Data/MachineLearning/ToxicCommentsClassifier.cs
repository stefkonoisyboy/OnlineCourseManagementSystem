namespace OnlineCourseManagementSystem.Services.Data.MachineLearning
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.ML;
    using Microsoft.ML.Trainers;

    public static class ToxicCommentsClassifier
    {
        public static void TrainModel(string inputFile, string modelFile)
        {
            var context = new MLContext(seed: 1);
            IDataView trainingDataView = context.Data.LoadFromTextFile<CommentInput>(
                inputFile,
                hasHeader: true,
                separatorChar: ',',
                allowQuoting: true);

            var dataProcessPipeline = context.Transforms.Text.FeaturizeText("Content_tf", nameof(CommentInput.Content))
                .Append(context.Transforms.CopyColumns("Features", "Content_tf"))
                .Append(context.Transforms.NormalizeMinMax("Features", "Features")).AppendCacheCheckpoint(context);

            var trainer = context.BinaryClassification.Trainers.LbfgsLogisticRegression(
                new LbfgsLogisticRegressionBinaryTrainer.Options
                {
                    L2Regularization = 0.6925718f,
                    L1Regularization = 0.6819714f,
                    OptimizationTolerance = 0.0001f,
                    HistorySize = 50,
                    MaximumNumberOfIterations = 95499535,
                    InitialWeightsDiameter = 0.9147193f,
                    DenseOptimizer = true,
                    LabelColumnName = nameof(CommentInput.IsPositive),
                    FeatureColumnName = "Features",
                });

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            context.Model.Save(model, trainingDataView.Schema, modelFile);
        }

        public static IEnumerable<CommentOutput> TestModel(string modelFile, IEnumerable<string> testModelData)
        {
            var context = new MLContext();
            var model = context.Model.Load(modelFile, out _);
            var predictionEngine = context.Model.CreatePredictionEngine<CommentInput, CommentOutput>(model);

            foreach (var testData in testModelData)
            {
                var prediction = predictionEngine.Predict(new CommentInput { Content = testData });

                yield return prediction;

                // Console.WriteLine(new string('-', 60));
                // Console.WriteLine($"Content: {testData}");
                // Console.WriteLine($"Is OK? {prediction.Prediction}");
                // Console.WriteLine($"Score: {prediction.Score}");
            }
        }
    }
}
