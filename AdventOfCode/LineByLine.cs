using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class LineByLine
    {
        public static IEnumerable<string> GetLines(string fileName)
        {
            using (var fileStream = new StreamReader(fileName))
            {
                string currentLine;

                while (null != (currentLine = fileStream.ReadLine()))
                {
                    yield return currentLine;
                }
            }
        }
    }
}
