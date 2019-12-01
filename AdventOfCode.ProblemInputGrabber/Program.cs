namespace AdventOfCode.ProblemInputGrabber
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;

    public class Program
    {
        private static readonly int Day = DateTime.UtcNow.Day;
        private static readonly string PathToInputFile = Path.Combine("AdventOfCode2019", "Input", $"{Day}.input");
        private static readonly string PathToProblemFile = Path.Combine("AdventOfCode2019", "Problems", $"Problem{Day}.cs");
        private static readonly string PathToProblemTestFile = Path.Combine("AdventOfCode2019.Tests", "Problems", $"Problem{Day}Tests.cs");

        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("You need to provide a session cookie.");
                Environment.Exit(1);
            }

            if (DateTime.UtcNow.Month != 12 || Day > 25)
            {
                Console.Error.WriteLine("It's not time yet.");
                Environment.Exit(2);
            }

            if (File.Exists(PathToInputFile))
            {
                Console.Out.WriteLine("Today's problem is already downloaded.");
                Environment.Exit(3);
            }

            if (!PullLatestMaster())
            {
                Environment.Exit(4);
            }

            if (!DownloadDailyInput(args[0]))
            {
                Environment.Exit(5);
            }

            if (!CreateDailyProblemCodeFiles())
            {
                Environment.Exit(6);
            }

            if (!CommitAndPushChanges())
            {
                Environment.Exit(7);
            }

            Console.Out.WriteLine("Done, exiting.");
        }

        private static bool DownloadDailyInput(string sessionCookie)
        {
            Console.Out.WriteLine("Downloading daily input.");

            Directory.CreateDirectory(Path.GetDirectoryName(PathToInputFile));

            try
            {
                var request = WebRequest.CreateHttp($"https://adventofcode.com/2019/day/{Day}/input");
                request.CookieContainer = new CookieContainer(1);
                request.CookieContainer.Add(new Cookie("session", sessionCookie, "/", "adventofcode.com"));

                var response = request.GetResponse();
                string documentContents;

                using (var receiveStream = response.GetResponseStream())
                {
                    using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }

                File.WriteAllText(PathToInputFile, documentContents);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private static bool CreateDailyProblemCodeFiles()
        {
            Console.Out.WriteLine("Create daily problem code files.");

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(PathToProblemFile));
                File.WriteAllText(Path.Combine(PathToProblemFile), string.Format("namespace AdventOfCode2019.Problems{1}{{{1}    public class Problem{0} : Problem{1}    {{{1}        public Problem{0}() : base({0}){1}        {{{1}        }}{1}{1}        public override string Answer(){1}        {{{1}            throw new System.NotImplementedException();{1}        }}{1}    }}{1}}}", Day, Environment.NewLine));

                Directory.CreateDirectory(Path.GetDirectoryName(PathToProblemTestFile));
                File.WriteAllText(PathToProblemTestFile, string.Format("namespace AdventOfCode2019.Tests.Problems{0}{{{0}    using AdventOfCode2019.Problems;{0}    using NUnit.Framework;{0}{0}    [TestFixture]{0}    public class Problem{1}Tests{0}    {{{0}{0}    }}{0}}}", Environment.NewLine, Day));

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private static bool PullLatestMaster()
        {
            Console.Out.WriteLine("Check out and pull latest master.");

            return ExecuteGitCommand("checkout -B master") &&
                   ExecuteGitCommand("pull");
        }

        private static bool CommitAndPushChanges()
        {
            Console.Out.WriteLine("Commit and push created files.");

            return ExecuteGitCommand($"add {PathToInputFile} {PathToProblemFile} {PathToProblemTestFile}") &&
                   ExecuteGitCommand($"commit -m \"PIG: Adding input and boilerplate for {Day}\"") &&
                   ExecuteGitCommand("push");
        }

        private static bool ExecuteGitCommand(string arguments)
        {
            var process = new Process {StartInfo = new ProcessStartInfo("git", arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            }};
            process.Start();
            
            process.WaitForExit(30 * 1000);

            if (process.ExitCode != 0)
            {
                Console.Error.WriteLine($"\"git {arguments}\" failed, exit code {process.ExitCode}.");
                return false;
            }

            return true;
        }
    }
}
