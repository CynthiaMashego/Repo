
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentFileProcessor
{
    public static class FileProcessor
    {
        public static List<Row> ReadFile(string filename)
        {
            List<Row> rows = new List<Row>();
            using (FileReader reader = new FileReader(filename))
            {
                Row row = new Row();
                while (reader.ReadRow(row))
                {
                    foreach (string s in row)
                    {
                        Console.Write(s);
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                    rows.Add(row);
                    row = new Row();
                }
            }
            rows.RemoveAt(0);
            return rows;
        }

        public static void WriteFile(string filename, List<Row> rows)
        {
            using (FileWriter writer = new FileWriter(filename))
            {
                foreach (var row in rows)
                {
                    writer.WriteRow(row);
                }
            }
        }

    }
}
