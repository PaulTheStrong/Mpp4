using System;
using System.IO;
using TestGeneratorLib;
using FileInfo = TestGeneratorLib.FileInfo;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = File.ReadAllText("C:/Users/Pavel_Saroka/RiderProjects/TestGenerator/TestGeneratorLib/ClassLoader.cs");
            FileInfo fileInfo = ClassLoader.GetFileInfo(code);
            var generatedTests = TestGenerator.GenerateTests(fileInfo);
            Console.WriteLine(generatedTests);
        }
    }
}
