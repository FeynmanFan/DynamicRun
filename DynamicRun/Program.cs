namespace DynamicRun
{
    using DynamicRun.Builder;
    using System.IO;

    class Program
    {
        static void Main()
        {
            var compiler = new Compiler();

            var candidatesFile = @"C:\code\cset\globomantics.csv";
            var oppoFile = @"C:\code\cset\opportunities.csv";

            var args = new string[] { File.ReadAllText(candidatesFile), File.ReadAllText(oppoFile)};

            var filePath = @"C:\Code\DynamicRun\Sources\script.txt";

            Runner.Execute(compiler.Compile(filePath), args);
        }
    }
}
