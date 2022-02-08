namespace OnlineCourseManagementSystem.Services.Data.MachineLearning
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.ML.Data;

    public class CommentOutput
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Score { get; set; }
    }
}
