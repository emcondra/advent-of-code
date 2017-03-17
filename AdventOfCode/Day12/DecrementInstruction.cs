using System;

namespace AdventOfCode.Day12
{
    public class DecrementInstruction : Instruction
    {
        private readonly string _instruction;
        private readonly Registers _registers;

        public DecrementInstruction(string instruction, Registers registers)
        {
            _instruction = instruction;
            _registers = registers;
        }

        public int Execute(int currentInstructionIndex)
        {
            var register = Convert.ToChar(_instruction.Substring(4, 1));
            _registers[register]--;
            return currentInstructionIndex + 1;
        }
    }
}
