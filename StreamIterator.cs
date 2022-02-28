using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FileStreamEnumerator
{
    public class StreamIterator : IEnumerable<int>
    {
        private List<int> _list;
        
        private StreamReader _streamReader;

        public StreamIterator(Stream stream)
        {
            _list = new List<int>();
            _streamReader = new StreamReader(stream);
            string line = string.Empty;
            while((line = _streamReader.ReadLine()) != null)
            {
                var trimmed = line.Trim();
                var removedZeros = Regex.Replace(trimmed ,@"^0+(?=\d)", "");
                bool noLeadingZeros = trimmed == removedZeros;
                var stripped = Regex.Replace(removedZeros, "[^\\w- .+,]+", "");
                bool noSpecChars = removedZeros == stripped;
                var chArr = stripped.ToCharArray();
                if (chArr[0] == '-' && chArr[1] == '0')
                {
                    if (chArr.Length <= 2)
                    {
                        _list.Add(Convert.ToInt32(new string(chArr)));
                    }
                }
                else
                {
                    if(noLeadingZeros && noSpecChars)
                    {
                        if (chArr.Contains('.'))
                        {
                            double db = double.Parse(stripped, CultureInfo.InvariantCulture);
                            // bool isInt = unchecked(db == (int)db);
                            // Console.WriteLine($"db {db}, {isInt}");
                            if (unchecked(db == (int)db))
                            {
                                _list.Add((int)db);
                            }
                        }
                        // check if valid number
                        if(int.TryParse(stripped, out int value))
                        {
                            if (-100000000 < value && value < 1000000000)
                            {
                                _list.Add(value);
                            }
                        }
                    }
                }
            }
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

    }
}
