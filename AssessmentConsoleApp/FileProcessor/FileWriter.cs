using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentFileProcessor
{
    public class FileWriter : StreamWriter
    {
        public FileWriter(Stream stream) : base(stream)
        {
        }

        public FileWriter(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Writes a single row to a file.
        /// </summary>
        /// <param name="row">The row to be written</param>
        public void WriteRow(Row row)
        {
            WriteLine(row.LineText);
        }
    }
}
