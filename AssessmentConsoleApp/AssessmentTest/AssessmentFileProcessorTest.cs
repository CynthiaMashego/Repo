using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssessmentFileProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AssessmentTest
{
    [TestClass]
    public class AssessmentFileProcessorTest
    {
        [TestMethod]
        public void GetPersonFromFile_ShouldSucceed()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return;
            if (directoryInfo.Parent == null) return;

            // read the csv data file
            string filename = Path.Combine(directoryInfo.Parent.FullName, "Data\\data.csv");
            List<Row> rows = FileProcessor.ReadFile(filename);
            Assert.AreEqual(8,rows.Count);
        }

        [TestMethod]
        public void ProcessPersonFromFile_ShouldSucceed()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return;
            if (directoryInfo.Parent == null) return;

            // read the csv data file
            string filename = Path.Combine(directoryInfo.Parent.FullName, "Data\\data.csv");
            List<Row> rows = FileProcessor.ReadFile(filename);

            //Process the data
            List<Person> person = PersonProcessor.GetPersonList(rows);
            Assert.AreEqual(8,person.Count);
        }

        [TestMethod]
        public void GetPersonFirstNameFrequency_ShouldSucceed()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return;
            if (directoryInfo.Parent == null) return;

            // read the csv data file
            string filename = Path.Combine(directoryInfo.Parent.FullName, "Data\\data.csv");
            List<Row> rows = FileProcessor.ReadFile(filename);

            //Process the data
            List<Person> person = PersonProcessor.GetPersonList(rows);
            Dictionary<string, int> orderDictionary = PersonProcessor.PersonsFirstNameFrequency(person);
            Assert.AreEqual(5, orderDictionary.Count);
        }

        [TestMethod]
        public void GetPersonLastNameFrequency_ShouldSucceed()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return;
            if (directoryInfo.Parent == null) return;

            // read the csv data file
            string filename = Path.Combine(directoryInfo.Parent.FullName, "Data\\data.csv");
            List<Row> rows = FileProcessor.ReadFile(filename);

            //Process the data
            List<Person> person = PersonProcessor.GetPersonList(rows);
            Dictionary<string, int> orderDictionary = PersonProcessor.PersonsLastNameFrequency(person);
            Assert.AreEqual(4, orderDictionary.Count);
        }

         [TestMethod]
        public void GetPersonAddresses_ShouldSucceed()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return;
            if (directoryInfo.Parent == null) return;

            // read the csv data file
            string filename = Path.Combine(directoryInfo.Parent.FullName, "Data\\data.csv");
            List<Row> rows = FileProcessor.ReadFile(filename);

            //Process the data
            List<Person> person = PersonProcessor.GetPersonList(rows);
            List<string> orderDictionary = PersonProcessor.PersonsAddresses(person);
            Assert.AreEqual(8, orderDictionary.Count);
        }

        [TestMethod]
        public void WritetoFile_FirstAndLastName_Ordered_ShouldSucceed()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return;
            if (directoryInfo.Parent == null) return;

            // read the csv data file
            string filename = Path.Combine(directoryInfo.Parent.FullName, "Data\\data.csv");
            List<Row> rows = FileProcessor.ReadFile(filename);
            List<Person> person = PersonProcessor.GetPersonList(rows);
            Dictionary<string, int> orderFirstname = PersonProcessor.PersonsFirstNameFrequency(person);
            Dictionary<string, int> orderLastName = PersonProcessor.PersonsLastNameFrequency(person);

            //Process the data before writing it to a text file
            List<Row> rowList = new List<Row>();
            rowList.Add(new Row(){LineText = "Ordered FirstName"});
            rowList.AddRange(orderFirstname.Select(first => new Row() {LineText = first.Key + ", " + first.Value}));
            rowList.Add(new Row() { LineText ="Ordered LastName" });
            rowList.AddRange(orderLastName.Select(last => new Row() {LineText = last.Key + ", " + last.Value}));
           
            string writeFileName = Path.Combine(directoryInfo.Parent.FullName, "Data\\Names.txt");
            // check and delete file first before creating a new file
            if (File.Exists(writeFileName))
                File.Delete(writeFileName);
            FileProcessor.WriteFile(writeFileName, rowList);

            bool fileExist = File.Exists(writeFileName);
            Assert.IsTrue(fileExist);

        }

        [TestMethod]
        public void WritetoFile_PersonAddresses_ShouldSucceed()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) return;
            if (directoryInfo.Parent == null) return;

            // read the csv file
            string filename = Path.Combine(directoryInfo.Parent.FullName, "Data\\data.csv");
            List<Row> rows = FileProcessor.ReadFile(filename);
            List<Person> person = PersonProcessor.GetPersonList(rows);

            //Process the data before writing it to a text file
            List<string> orderDictionary = PersonProcessor.PersonsAddresses(person);
            List<Row> rowList = orderDictionary.Select(address => new Row() {LineText = address}).ToList();

            string writeFileName = Path.Combine(directoryInfo.Parent.FullName, "Data\\Address.txt");
            // check and delete file first before creating a new file
            if (File.Exists(writeFileName))
                File.Delete(writeFileName);
            FileProcessor.WriteFile(writeFileName, rowList);

            bool fileExist = File.Exists(writeFileName);
            Assert.IsTrue(fileExist);

        }
    }
}
