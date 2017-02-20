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
        public static List<Person> GetPersonList(List<Row> rows)
        {
            List<Person> persons = new List<Person>();
            foreach (var row in rows)
            {
              Person person = new Person();
                for (int i =0 ; i < row.Count; i++)
                {
                    if (i.Equals(0))
                        person.FirstName = row[i];
                    else if (i.Equals(1))
                        person.LastName = row[i];
                    else if (i.Equals(2))
                    {
                        string originalAddress = row[i];
                        Address address = new Address();
                        int startPos = originalAddress.IndexOf(" ", StringComparison.CurrentCultureIgnoreCase);
                        int length = originalAddress.Length;
                        string streetname = originalAddress.Substring(startPos);
                        originalAddress = originalAddress.Remove(startPos);
                        string streetnumber = originalAddress.Substring(0);
                        address.StreetName = streetname;
                        address.StreetNumber = Int32.Parse(streetnumber);
                        person.Address = address;
                    }
                    else 
                    {
                        person.PhoneNumber = Int32.Parse(row[i]);
                    }

                }
                persons.Add(person);
            }

            return persons;
        }

        public static Dictionary<string, int> PersonsFirstNameFrequency(List<Person> persons)
        {
            var newList =  persons.GroupBy(p => p.FirstName)
                .OrderByDescending(p => p.Count())
                .ThenBy(p => p.Key)
                .Select(p => new {FirstName = p.Key, Count = p.Count()});
            return newList.ToDictionary(item => item.FirstName, item => item.Count);
        }

        public static Dictionary<string, int> PersonsLastNameFrequency(List<Person> persons)
        {
            var newList = persons.GroupBy(p => p.LastName)
                  .OrderByDescending(p => p.Count())
                  .ThenBy(p => p.Key)
                  .Select(p => new { LastName = p.Key, Count = p.Count() }).ToList();
            return newList.ToDictionary(item => item.LastName, item => item.Count);
        }

        public static List<string> PersonsAddresses(List<Person> persons)
        {
          var newList =
                persons.OrderBy(p => p.Address.StreetName)
                    .Select(p => new {p.Address.StreetNumber, p.Address.StreetName});
           return newList.Select(item => item.StreetNumber + " " + item.StreetName).ToList();
        }

    }
}
