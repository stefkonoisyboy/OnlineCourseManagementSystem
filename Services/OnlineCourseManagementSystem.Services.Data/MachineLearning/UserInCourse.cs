namespace OnlineCourseManagementSystem.Services.Data.MachineLearning
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.ML.Data;

    public class UserInCourse
    {
        [LoadColumn(0)]
        public int UserId { get; set; }

        [LoadColumn(1)]
        public int CourseId { get; set; }

        [LoadColumn(2)]
        public float Label { get; set; }

        [LoadColumn(3)]
        public string CourseName { get; set; }

        [LoadColumn(4)]
        public string CoursePrice { get; set; }

        [LoadColumn(5)]
        public int ParticipantsCount { get; set; }

        [LoadColumn(6)]
        public string StartDate { get; set; }

        [LoadColumn(7)]
        public int Id { get; set; }

        [LoadColumn(8)]
        public string RecommendationType { get; set; }
    }
}
