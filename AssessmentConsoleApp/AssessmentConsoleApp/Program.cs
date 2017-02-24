using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentFileProcessor;

namespace AssessmentConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var rows = ReadFile();
           
            Console.WriteLine(WriteFile() ? "Txt File was created" : "Txt File was not created");
        }

        static List<Row> ReadFile()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return null;
            if (directoryInfo.Parent == null) return null;

            // read the csv data file
            string filename = Path.Combine(directoryInfo.Parent.FullName, "Data\\data.csv");
            return FileProcessor.ReadFile(filename);
            
        }

        static bool WriteFile()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return false;
            if (directoryInfo.Parent == null) return false;

            //Process the data before writing it to a text file
            var rowList = new List<Row>
            {
                new Row() {LineText = "Line 1"},
                new Row() {LineText = "Line 2"},
                new Row() {LineText = "Line 3"}
            };

            var writeFileName = Path.Combine(directoryInfo.Parent.FullName, "Data\\Test.txt");
            // check and delete file first before creating a new file
            if (File.Exists(writeFileName))
                File.Delete(writeFileName);
            FileProcessor.WriteFile(writeFileName, rowList);

            return File.Exists(writeFileName);
           
        }
    }
}
