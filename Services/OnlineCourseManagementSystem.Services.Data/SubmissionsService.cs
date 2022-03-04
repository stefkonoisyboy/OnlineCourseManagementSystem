using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using OnlineCourseManagementSystem.Data.Common.Repositories;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using OnlineCourseManagementSystem.Web.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly IDeletableEntityRepository<Submission> submissionsRepository;
        private readonly IDeletableEntityRepository<Test> testsRepository;

        public SubmissionsService(
            IDeletableEntityRepository<Submission> submissionsRepository,
            IDeletableEntityRepository<Test> testsRepository)
        {
            this.submissionsRepository = submissionsRepository;
            this.testsRepository = testsRepository;
        }

        public async Task CreateAsync(CreateSubmissionInputModel input, string inputPath, string outputPath)
        {
            int points = 0;

            IEnumerable<Test> testsByProblem = this.testsRepository
                .All()
                .Where(t => t.ProblemId == input.ProblemId)
                .ToList();

            int pointsPerTest = 100 / testsByProblem.Count();

            foreach (var test in testsByProblem)
            {
                using (StreamWriter writer = new StreamWriter($"{inputPath}/code/input/{test.Id}.txt"))
                {
                    writer.WriteLine(test.Input);
                }

                using (StreamWriter writer = new StreamWriter($"{inputPath}/code/expectedOutput/{test.Id}.txt"))
                {
                    writer.WriteLine(test.Output);
                }

                string inputContent = string.Empty;
                string expectedOutputContent = string.Empty;

                using (StreamReader reader = new StreamReader($"{inputPath}/code/input/{test.Id}.txt"))
                {
                    string line = reader.ReadLine();
                    inputContent += line + Environment.NewLine;

                    while (line != null)
                    {
                        line = reader.ReadLine();
                        inputContent += line + Environment.NewLine;
                    }
                }

                using (StreamReader reader = new StreamReader($"{inputPath}/code/expectedOutput/{test.Id}.txt"))
                {
                    string line = reader.ReadLine();
                    expectedOutputContent += line + Environment.NewLine;

                    while (line != null)
                    {
                        line = reader.ReadLine();
                        expectedOutputContent += line + Environment.NewLine;
                    }
                }

                string[] inputContentLines = inputContent.Split(Environment.NewLine).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
                string[] expectedOutputContentLines = expectedOutputContent.Split(Environment.NewLine).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

                string[] lines = input.Code.Split(Environment.NewLine).ToArray();
                int lineToReadFromInputFile = 0;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("Console.ReadLine"))
                    {
                        inputContentLines[lineToReadFromInputFile] = inputContentLines[lineToReadFromInputFile].Insert(0, "\"");
                        inputContentLines[lineToReadFromInputFile] = inputContentLines[lineToReadFromInputFile].Insert(inputContentLines[lineToReadFromInputFile].Length, "\"");
                        lines[i] = lines[i].Replace("Console.ReadLine()", inputContentLines[lineToReadFromInputFile]);
                        lineToReadFromInputFile++;
                    }
                    else if (lines[i].Contains("Console.WriteLine"))
                    {
                        string code = lines[i];

                        string variable = code.Substring(code.IndexOf('('));
                        variable = variable.Remove(variable.IndexOf('('), 1);
                        variable = variable.Remove(variable.IndexOf(')'), 1);
                        variable = variable.Remove(variable.IndexOf(';'), 1);
                        //variable = variable.Insert(0, "\"");
                        //variable = variable.Insert(variable.Length, "\"");

                        code = code.Replace("Console.WriteLine", "File.AppendAllText");

                        int startIndex = code.IndexOf('(');
                        int endIndex = code.IndexOf(')');

                        code = code.Remove(startIndex);
                        string physicalPath = outputPath + "/code/output/product.txt";
                        code += $"({physicalPath}, {variable}.ToString() + Environment.NewLine);";

                        lines[i] = code;
                    }
                }

                var state = await CSharpScript.RunAsync(lines.FirstOrDefault(), ScriptOptions.Default.WithImports("System", "System.IO"));

                foreach (var line in lines.Skip(1))
                {
                    state = await state.ContinueWithAsync(line, ScriptOptions.Default.WithImports("System", "System.IO"));
                }

                string userOutput = string.Empty;

                using (StreamReader reader = new StreamReader($"{outputPath}/code/output/product.txt"))
                {
                    string line = reader.ReadLine();
                    userOutput += line + Environment.NewLine;

                    while (line != null)
                    {
                        line = reader.ReadLine();
                        userOutput += line + Environment.NewLine;
                    }
                }

                string[] userOutputArr = userOutput.Split(Environment.NewLine).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

                bool prime = true;

                for (int i = 0; i < userOutputArr.Length; i++)
                {
                    if (userOutputArr[i] != expectedOutputContentLines[i])
                    {
                        prime = false;
                        break;
                    }
                }

                if (prime)
                {
                    points += pointsPerTest;
                }

                System.IO.File.Delete($"{inputPath}/code/input/{test.Id}.txt");
                System.IO.File.Delete($"{inputPath}/code/expectedOutput/{test.Id}.txt");
                System.IO.File.Delete($"{outputPath}/code/output/product.txt");
            }

            Submission submission = new Submission
            {
                Code = input.Code,
                Points = points,
                ProblemId = input.ProblemId,
                ContestId = input.ContestId,
                UserId = input.UserId,
            };

            await this.submissionsRepository.AddAsync(submission);
            await this.submissionsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetTop5ByContestIdAndUserId<T>(int contestId, string userId)
        {
            return this.submissionsRepository
                .All()
                .Where(s => s.ContestId == contestId && s.UserId == userId)
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.CreatedOn)
                .Take(5)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetTop5ByProblemIdAndUserId<T>(int problemId, string userId)
        {
            return this.submissionsRepository
               .All()
               .Where(s => s.ProblemId == problemId && s.UserId == userId)
               .OrderByDescending(s => s.Points)
               .ThenByDescending(s => s.CreatedOn)
               .Take(5)
               .To<T>()
               .ToList();
        }
    }
}
