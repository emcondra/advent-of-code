namespace AdventOfCode.Day12
{
    public class InstructionExecutor
    {
        private readonly string[] _instructions;
        private Registers _registers;

        public InstructionExecutor(string[] instructions)
        {
            _instructions = instructions;
            _registers = new Registers();
        }

        public int Execute()
        {
            var instructionFactory = new InstructionFactory(_registers);
            
            var currentInstructionIndex = 0;

            _registers['c'] = 1;

            while (currentInstructionIndex < _instructions.Length && currentInstructionIndex >= 0)
            {
                var instructionType = instructionFactory.Get(_instructions[currentInstructionIndex]);
                currentInstructionIndex = instructionType.Execute(currentInstructionIndex);
            }

            return _registers['a'];
        }
    }
}
