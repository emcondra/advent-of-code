namespace AdventOfCode.Day12
{
    class InstructionFactory
    {
        private readonly Registers _registers;

        public InstructionFactory(Registers registers)
        {
            _registers = registers;
        }

        public Instruction Get(string instruction)
        {
            var instructionName = instruction.Substring(0, 3);

            switch (instructionName)
            {
                case "inc":
                    return new IncrementInstruction(instruction, _registers);

                case "dec":
                    return new DecrementInstruction(instruction, _registers);

                case "cpy":
                    return new CopyInstruction(instruction, _registers);

                case "jnz":
                    return new JumpInstruction(instruction, _registers);

                default:
                    return null;
            }
        }
    }
}
