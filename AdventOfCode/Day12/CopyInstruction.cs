using System;

namespace AdventOfCode.Day12
{
    public class CopyInstruction : Instruction
    {
        private readonly string _instruction;
        private readonly Registers _registers;

        public CopyInstruction(string instruction, Registers registers)
        {
            _instruction = instruction;
            _registers = registers;
        }

        public int Execute(int currentInstructionIndex)
        {
            var instructionParts = _instruction.Split(' ');
            var valueToCopy = InstructionExtensions.GetIntValueOrValueOfRegister(instructionParts[1], _registers);
            var register = Convert.ToChar(instructionParts[2]);

            _registers[register] = valueToCopy;
            return currentInstructionIndex + 1;
        }
    }
}
