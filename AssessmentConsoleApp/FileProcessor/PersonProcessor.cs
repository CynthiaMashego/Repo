using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentFileProcessor
{
    public static class PersonProcessor
    {
        /// <summary>
        /// Orders person by address - street name
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        public static List<string> PersonsAddresses_OrderbyStreetName(List<Person> persons)
        {
            var newList =
                  persons.OrderBy(p => p.Address.StreetName)
                      .Select(p => new { p.Address.StreetNumber, p.Address.StreetName });
            return newList.Select(item => item.StreetNumber + " " + item.StreetName).ToList();
        }
        /// <summary>
        /// Order person frequency(desc) of names( first and last) then by alphabetically (asc)
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>

        public static Dictionary<string, int> PersonNameFrequency(List<Person> persons)
        {
            var names = new List<Row>();
            foreach (var person in persons)
            {
                names.Add(new Row() { LineText = person.FirstName });
                names.Add(new Row() { LineText = person.LastName });
            }
            var newList = names.GroupBy(n => n.LineText)
                .OrderByDescending(n => n.Count())
                .ThenBy(n => n.Key)
                .Select(n => new { LineText = n.Key, Count = n.Count() }).ToList();

            return newList.ToDictionary(item => item.LineText, item => item.Count);
        }

        /// <summary>
        /// create person object
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static List<Person> GetPersonList(List<Row> rows)
        {
            var persons = new List<Person>();
            foreach (var row in rows)
            {
                var person = new Person();
                for (var i = 0; i < row.Count; i++)
                {
                    if (i.Equals(0))
                        person.FirstName = row[i];
                    else if (i.Equals(1))
                        person.LastName = row[i];
                    else if (i.Equals(2))
                    {
                        var originalAddress = row[i];
                        var address = new Address();
                        var startPos = originalAddress.IndexOf(" ", StringComparison.CurrentCultureIgnoreCase);
                        var streetname = originalAddress.Substring(startPos);
                        originalAddress = originalAddress.Remove(startPos);
                        var streetnumber = originalAddress.Substring(0);
                        address.StreetName = streetname;
                        address.StreetNumber = int.Parse(streetnumber);
                        person.Address = address;
                    }
                    else
                    {
                        person.PhoneNumber = int.Parse(row[i]);
                    }

                }
                persons.Add(person);
            }

            return persons;
        }

        /// <summary>
        /// Order by frequency(desc) then first name
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        public static Dictionary<string, int> PersonsFirstNameFrequency(List<Person> persons)
        {
            var newList = persons.GroupBy(p => p.FirstName)
                .OrderByDescending(p => p.Count())
                .ThenBy(p => p.Key)
                .Select(p => new { FirstName = p.Key, Count = p.Count() });
            return newList.ToDictionary(item => item.FirstName, item => item.Count);
        }

        /// <summary>
        /// Order by frequency(desc) then last name
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        public static Dictionary<string, int> PersonsLastNameFrequency(List<Person> persons)
        {
            var newList = persons.GroupBy(p => p.LastName)
                  .OrderByDescending(p => p.Count())
                  .ThenBy(p => p.Key)
                  .Select(p => new { LastName = p.Key, Count = p.Count() }).ToList();
            return newList.ToDictionary(item => item.LastName, item => item.Count);
        }
        

    }
}
