using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AssessmentFileProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace AssessmentTest
{
    [TestClass]
    public class PersonProcessorTest
    {
       
        /// <summary>
        /// Test that the all names can be grouped and sorted
        /// </summary>
        [TestMethod]
        public void GetPersonNameFrequency_OrderbyName_ShouldSucceed()
        {
            var personList = new List<Person>
            {
                new Person() {FirstName = "Matt", LastName = "Brown"},
                new Person() {FirstName = "Heinrich", LastName = "Jones"},
                new Person() {FirstName = "Johnson", LastName = "Smith"},
                new Person() {FirstName = "Tim", LastName = "Johnson"},

            };
            var orderDictionary = PersonProcessor.PersonNameFrequency(personList);
            var topNameReturned = orderDictionary.FirstOrDefault();
            var lastNameReturned = orderDictionary.LastOrDefault();
            var johnsonFrequencyReturned = orderDictionary["Johnson"];
            var brownFrequencyReturned = orderDictionary["Brown"];
            var heinrichFrequencyReturned = orderDictionary["Heinrich"];

            var johnsonFrequencyExpected = 2;
            var brownFrequencyExpected = 1;
            var heinrichFrequencyExpected = 1;

            var topNameCountExpected = 2;
            var topNameExpected = "Johnson";

            var lastNameCountExpected = 1;
            var lastNameExpected = "Tim";

            Assert.IsNotNull(topNameReturned);
            Assert.AreEqual(topNameCountExpected, topNameReturned.Value);
            Assert.AreEqual(topNameExpected, topNameReturned.Key);

            Assert.IsNotNull(lastNameReturned);
            Assert.AreEqual(lastNameCountExpected, lastNameReturned.Value);
            Assert.AreEqual(lastNameExpected, lastNameReturned.Key);

            Assert.AreEqual(johnsonFrequencyExpected, johnsonFrequencyReturned);
            Assert.AreEqual(brownFrequencyExpected, brownFrequencyReturned);
            Assert.AreEqual(heinrichFrequencyExpected, heinrichFrequencyReturned);
        }

        /// <summary>
        /// Test that addresses are sorted alphabetically by streetname
        /// </summary>
        [TestMethod]
        public void GetPersonAddresses_OrderbyStreetName_ShouldSucceed()
        {
           //Process the data
            var personList = new List<Person>
            {
                new Person()
                {
                    FirstName = "Steven",
                    LastName = "Jones",
                    Address = new Address() {StreetName = "Alice Lane", StreetNumber = 15}
                },
                new Person()
                {
                    FirstName = "Steven",
                    LastName = "Brown",
                    Address = new Address() {StreetName = "Sunset Ave", StreetNumber = 30}
                },
                new Person()
                {
                    FirstName = "Jane",
                    LastName = "Brown",
                    Address = new Address() {StreetName = "Rockery Road", StreetNumber = 1}
                },
                new Person()
                {
                    FirstName = "Maria",
                    LastName = "Josephs",
                    Address = new Address() {StreetName = "Church Str", StreetNumber = 350}
                },
                new Person()
                {
                    FirstName = "Steven",
                    LastName = "Daniels",
                    Address = new Address() {StreetName = "Hamilton", StreetNumber = 5}
                },
            };

            List<string> orderDictionary = PersonProcessor.PersonsAddresses_OrderbyStreetName(personList);

            var expectedAddress = new Address() {StreetName = "Alice Lane", StreetNumber = 15};
            var result = orderDictionary[0];
           
            Assert.IsNotNull(expectedAddress);
            Assert.AreEqual(expectedAddress.ToString(), result);
           
        }

        //[TestMethod]
        //public void GetPersonFirstName_SortByFrequency_ShouldSucceed()
        //{
        //    //Process the data
        //    var personList = new List<Person>
        //    {
        //        new Person() {FirstName = "Steven", LastName = "Jones"},
        //        new Person() {FirstName = "Steven", LastName = "Martin"},
        //        new Person() {FirstName = "Jane", LastName = "Brown"},
        //        new Person() {FirstName = "Maria", LastName = "Josephs"},
        //        new Person() {FirstName = "Steven", LastName = "Daniels"}
        //    };
        //    var orderDictionary = PersonProcessor.PersonsFirstNameFrequency(personList);
        //    var topFirstNameReturned = orderDictionary.FirstOrDefault();
        //    var steveFrequencyReturned = orderDictionary["Steven"];
        //    var janeFrequencyReturned = orderDictionary["Jane"];
        //    var mariaFrequencyReturned = orderDictionary["Maria"];

        //    var topFirstNameCountExpected = 3;
        //    var topFirstNameExpected = "Steven";

        //    Assert.IsNotNull(topFirstNameReturned);
        //    Assert.AreEqual(topFirstNameCountExpected, topFirstNameReturned.Value);
        //    Assert.AreEqual(topFirstNameExpected, topFirstNameReturned.Key);

        //    var steveFrequencyExpected = 3;
        //    var janeFrequencyExpected = 1;
        //    var mariaFrequencyExpected = 1;

        //    Assert.AreEqual(steveFrequencyExpected, steveFrequencyReturned);
        //    Assert.AreEqual(janeFrequencyExpected, janeFrequencyReturned);
        //    Assert.AreEqual(mariaFrequencyExpected, mariaFrequencyReturned);
        //}

        //[TestMethod]
        //public void GetPersonLastName_SortByFrequency_ShouldSucceed()
        //{
        //   //Process the data
        //    var personList = new List<Person>
        //    {
        //        new Person() {FirstName = "Steven", LastName = "Jones"},
        //        new Person() {FirstName = "Steven", LastName = "Brown"},
        //        new Person() {FirstName = "Jane", LastName = "Brown"},
        //        new Person() {FirstName = "Maria", LastName = "Josephs"},
        //        new Person() {FirstName = "Steven", LastName = "Daniels"},
        //        new Person() {FirstName = "John", LastName = "Daniels"}
        //    };

        //    var orderDictionary = PersonProcessor.PersonsLastNameFrequency(personList);
        //    var topLastNameReturned = orderDictionary.FirstOrDefault();
        //    var jonesFrequencyReturned = orderDictionary["Jones"];
        //    var brownFrequencyReturned = orderDictionary["Brown"];
        //    var danielsFrequencyReturned = orderDictionary["Daniels"];

        //    var topLastNameCountExpected = 2;
        //    var topLastNameExpected = "Brown";


        //    Assert.IsNotNull(topLastNameReturned);
        //    Assert.AreEqual(topLastNameCountExpected, topLastNameReturned.Value);
        //    Assert.AreEqual(topLastNameExpected, topLastNameReturned.Key);

        //    var jonesFrequencyExpected = 1;
        //    var brownFrequencyExpected = 2;
        //    var danielsFrequencyExpected = 2;

        //    Assert.AreEqual(jonesFrequencyExpected, jonesFrequencyReturned);
        //    Assert.AreEqual(brownFrequencyExpected, brownFrequencyReturned);
        //    Assert.AreEqual(danielsFrequencyExpected, danielsFrequencyReturned);
        //}
    }
}
