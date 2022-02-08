namespace OnlineCourseManagementSystem.Services.Data.MachineLearning
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.ML.Data;

    public class CommentInput
    {
        [LoadColumn(0)]
        public string Content { get; set; }

        [LoadColumn(1)]
        public bool IsPositive { get; set; }
    }
}
