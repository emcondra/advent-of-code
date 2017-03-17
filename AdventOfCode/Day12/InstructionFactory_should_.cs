using NUnit.Framework;

namespace AdventOfCode.Day12
{
    public class InstructionFactory_should_
    {
        [TestCase("inc a")]
        [TestCase("inc b")]
        [TestCase("inc c")]
        [TestCase("inc d")]
        public void return_a_increment_instruction_based_on_corresponding_string_input(string stringInstruction)
        {
            var registers = new Registers();

            var instructionFactory = new InstructionFactory(registers);

            var instruction = instructionFactory.Get(stringInstruction);

            Assert.AreEqual(typeof(IncrementInstruction), instruction.GetType());
        }

        [TestCase("dec a")]
        [TestCase("dec b")]
        [TestCase("dec c")]
        [TestCase("dec d")]
        public void return_a_decrement_instruction_based_on_corresponding_string_input(string stringInstruction)
        {
            var registers = new Registers();

            var instructionFactory = new InstructionFactory(registers);

            var instruction = instructionFactory.Get(stringInstruction);

            Assert.AreEqual(typeof(DecrementInstruction), instruction.GetType());
        }

        [TestCase("cpy 41 a")]
        [TestCase("cpy 2 b")]
        [TestCase("cpy 15 c")]
        [TestCase("cpy 8 d")]
        public void return_a_copy_instruction_based_on_corresponding_string_input(string stringInstruction)
        {
            var registers = new Registers();

            var instructionFactory = new InstructionFactory(registers);

            var instruction = instructionFactory.Get(stringInstruction);

            Assert.AreEqual(typeof(CopyInstruction), instruction.GetType());
        }

        [TestCase("jnz a 2")]
        [TestCase("jnz b 11")]
        [TestCase("jnz c 3")]
        [TestCase("jnz d 0")]
        public void return_a_jump_instruction_based_on_corresponding_string_input(string stringInstruction)
        {
            var registers = new Registers();

            var instructionFactory = new InstructionFactory(registers);

            var instruction = instructionFactory.Get(stringInstruction);

            Assert.AreEqual(typeof(JumpInstruction), instruction.GetType());
        }
    }
}
