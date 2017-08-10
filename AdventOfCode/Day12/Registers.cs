using System.Collections.Generic;

namespace AdventOfCode.Day12
{
    public class Registers 
    {
        private readonly Dictionary<char, int> _registerValues = new Dictionary<char, int>();

        public int this[char index]    
        {
            get
            {
                return _registerValues.ContainsKey(index) ? _registerValues[index] : 0;
            }
            set
            {
                _registerValues[index] = value;
            }
        }
    }
}
