using System;

namespace AdventOfCode.Day12
{
    public static class InstructionExtensions
    {
        public static int GetIntValueOrValueOfRegister(string instructionValue, Registers registers)
        {
            int intValue;
            var isInt = int.TryParse(instructionValue, out intValue);

            if (!isInt)
            {
                char charValue;
                var isChar = char.TryParse(instructionValue, out charValue);

                if (isChar)
                {
                    return registers[charValue];
                }

                throw new Exception("Invalid instruction.");
            }

            return intValue;
        }
    }
}
