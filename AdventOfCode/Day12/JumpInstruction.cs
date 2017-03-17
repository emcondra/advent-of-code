using System;

namespace AdventOfCode.Day12
{
    public class JumpInstruction : Instruction
    {
        private readonly string _instruction;
        private readonly Registers _registers;

        public JumpInstruction(string instruction, Registers registers)
        {
            _instruction = instruction;
            _registers = registers;
        }

        public int Execute(int currentInstructionIndex)
        {
            var values = _instruction.Split(' ');
            var intValueToCompare = InstructionExtensions.GetIntValueOrValueOfRegister(values[1], _registers);
            var indexesToMove = int.Parse(values[2]);

            if (intValueToCompare != 0)
            {
                return currentInstructionIndex + indexesToMove;
            }

            return currentInstructionIndex + 1;
        }
    }
}
