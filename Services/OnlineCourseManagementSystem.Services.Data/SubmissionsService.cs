namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.CodeAnalysis.CSharp.Scripting;
    using Microsoft.CodeAnalysis.Scripting;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Submissions;

    public class SubmissionsService : ISubmissionsService
    {
        private readonly IDeletableEntityRepository<Submission> submissionsRepository;
        private readonly IDeletableEntityRepository<Test> testsRepository;
        private readonly IDeletableEntityRepository<ExecutedTest> executedTestsRepository;
        private readonly IDeletableEntityRepository<Problem> problemsRepository;

        public SubmissionsService(
            IDeletableEntityRepository<Submission> submissionsRepository,
            IDeletableEntityRepository<Test> testsRepository,
            IDeletableEntityRepository<ExecutedTest> executedTestsRepository,
            IDeletableEntityRepository<Problem> problemsRepository)
        {
            this.submissionsRepository = submissionsRepository;
            this.testsRepository = testsRepository;
            this.executedTestsRepository = executedTestsRepository;
            this.problemsRepository = problemsRepository;
        }

        public async Task CreateAsync(CreateSubmissionInputModel input, string inputPath, string outputPath)
        {
            int points = 0;

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

            IEnumerable<Test> testsByProblem = this.testsRepository
                .All()
                .Where(t => t.ProblemId == input.ProblemId)
                .ToList();

            int pointsPerTest = 100 / testsByProblem.Count();

            foreach (var test in testsByProblem)
            {
                ExecutedTest executedTest = new ExecutedTest
                {
                    TestInput = test.Input,
                    ExpectedOutput = test.Output,
                    SubmissionId = submission.Id,
                };

                await this.executedTestsRepository.AddAsync(executedTest);
                await this.executedTestsRepository.SaveChangesAsync();

                using (StreamWriter writer = new StreamWriter(System.IO.Path.Combine("input.txt")))
                {
                    writer.WriteLine(test.Input);
                }

                using (StreamWriter writer = new StreamWriter(System.IO.Path.Combine("expectedOutput.txt")))
                {
                    writer.WriteLine(test.Output);
                }

                string inputContent = string.Empty;
                string expectedOutputContent = string.Empty;

                using (StreamReader reader = new StreamReader(System.IO.Path.Combine("input.txt")))
                {
                    string line = reader.ReadLine();
                    inputContent += line + Environment.NewLine;

                    while (line != null)
                    {
                        line = reader.ReadLine();
                        inputContent += line + Environment.NewLine;
                    }
                }

                using (StreamReader reader = new StreamReader(System.IO.Path.Combine("expectedOutput.txt")))
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
                string additionalCode = "int _indexCodeEvaluationSystem = 0; List<string> lines = new List<string>(); StreamReader streamReader = new StreamReader(System.IO.Path.Combine(\"input.txt\")); string line = streamReader.ReadLine(); while (line != null) { lines.Add(line); line = streamReader.ReadLine(); } streamReader.Close();";
                string mainCode = string.Empty;
                //int lineToReadFromInputFile = 0;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("Console.ReadLine"))
                    {
                        //inputContentLines[lineToReadFromInputFile] = inputContentLines[lineToReadFromInputFile].Insert(0, @"""");
                        //inputContentLines[lineToReadFromInputFile] = inputContentLines[lineToReadFromInputFile].Insert(inputContentLines[lineToReadFromInputFile].Length, @"""");
                        //lines[i] = lines[i].Replace("Console.ReadLine()", inputContentLines[lineToReadFromInputFile]);
                        //lineToReadFromInputFile++;

                        lines[i] = lines[i].Replace("Console.ReadLine()", "lines[_indexCodeEvaluationSystem++]");
                    }
                    else if (lines[i].Contains("Console.WriteLine"))
                    {
                        string code = lines[i];

                        string variable = code.Substring(code.IndexOf('('));
                        variable = variable.Remove(variable.IndexOf('('), 1);
                        variable = variable.Remove(variable.IndexOf(')'), 1);
                        variable = variable.Remove(variable.IndexOf(';'), 1);

                        // variable = variable.Insert(0, "\"");
                        // variable = variable.Insert(variable.Length, "\"");

                        code = code.Replace("Console.WriteLine", "File.AppendAllText");

                        int startIndex = code.IndexOf('(');
                        int endIndex = code.IndexOf(')');

                        code = code.Remove(startIndex);
                        string physicalPath = System.IO.Path.Combine("output.txt");
                        physicalPath = physicalPath.Insert(0, @"""");
                        physicalPath = physicalPath.Insert(physicalPath.Length, @"""");
                        code += $"({physicalPath}, {variable}.ToString() + Environment.NewLine);";

                        lines[i] = code;
                    }

                    mainCode += lines[i];
                }

                string finalCode = additionalCode + mainCode;

                try
                {
                    var script = CSharpScript.Create(finalCode, ScriptOptions.Default.WithImports("System", "System.IO", "System.Collections.Generic", "System.Text"));
                    await script.RunAsync();

                    //foreach (var line in lines.Skip(1))
                    //{
                    //    state = await state.ContinueWithAsync(line, ScriptOptions.Default.WithImports("System", "System.IO", "System.Collections.Generic", "System.Linq", "System.Text"));
                    //}

                    string userOutput = string.Empty;

                    using (StreamReader reader = new StreamReader(System.IO.Path.Combine("output.txt")))
                    {
                        string line = reader.ReadLine();
                        userOutput += line + Environment.NewLine;

                        while (line != null)
                        {
                            line = reader.ReadLine();
                            userOutput += line + Environment.NewLine;
                        }
                    }

                    executedTest.UserOutput = userOutput;

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
                        executedTest.HasPassed = true;
                    }
                    else
                    {
                        executedTest.HasPassed = false;
                    }

                    submission.Points = points;
                    await this.submissionsRepository.SaveChangesAsync();

                    await this.executedTestsRepository.SaveChangesAsync();

                    System.IO.File.Delete(System.IO.Path.Combine("input.txt"));
                    System.IO.File.Delete(System.IO.Path.Combine("expectedOutput.txt"));
                    System.IO.File.Delete(System.IO.Path.Combine("output.txt"));
                }
                catch (Exception ex)
                {
                    executedTest.UserOutput = ex.Message;
                    executedTest.HasPassed = false;

                    await this.executedTestsRepository.SaveChangesAsync();

                    System.IO.File.Delete($"{inputPath}/code/input/{test.Id}.txt");
                    System.IO.File.Delete($"{inputPath}/code/expectedOutput/{test.Id}.txt");
                    System.IO.File.Delete(System.IO.Path.Combine("output.txt"));
                }
            }

            submission.Points = points;
            await this.submissionsRepository.SaveChangesAsync();
        }

        public int GetProblemIdBySubmissionId(int submissionId)
        {
            return this.submissionsRepository
                .All()
                .FirstOrDefault(s => s.Id == submissionId)
                .ProblemId;
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
