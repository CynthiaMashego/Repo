using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentFileProcessor
{
   public class Address
    {
       public int StreetNumber { get; set; }

       public string StreetName { get; set; }

       public override string ToString()
       {
           return StreetNumber + " " + StreetName;
       }
    }
}
