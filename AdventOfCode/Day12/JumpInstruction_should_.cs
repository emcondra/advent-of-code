using NUnit.Framework;

namespace AdventOfCode.Day12
{
    public class JumpInstruction_should_
    {

        [TestCase("jnz a 2", 'a', 4, 6)]
        [TestCase("jnz b 12", 'b', 3, 15)]
        [TestCase("jnz c 8", 'c', 2, 10)]
        [TestCase("jnz d -1", 'd', 3, 2)]
        [Test]
        public void return_index_of_instruction_to_execute_next_if_register_is_not_0(string instructionString, char register, int currentIndex, int expectedIndex)
        {
            var registers = new Registers { [register] = 3 }; //anything but 0

            var instruction = new JumpInstruction(instructionString, registers);

            var actualIndex = instruction.Execute(currentIndex);

            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase("jnz 2 2", 4, 6)]
        [TestCase("jnz 4 12", 3, 15)]
        [TestCase("jnz 2 8", 2, 10)]
        [TestCase("jnz 1 -1", 3, 2)]
        public void return_index_of_instruction_to_execute_next_if_int_value_is_not_0(string instructionString, int currentIndex, int expectedIndex)
        {
            var instruction = new JumpInstruction(instructionString, new Registers());

            var actualIndex = instruction.Execute(currentIndex);

            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase("jnz a 2", 4, 5)]
        [TestCase("jnz b 12", 3, 4)]
        [TestCase("jnz c 8", 2, 3)]
        [TestCase("jnz d -1", 10, 11)]
        [Test]
        public void return_incremented_index_if_register_is_0(string instructionString, int currentIndex, int expectedIndex)
        {
            var instruction = new JumpInstruction(instructionString, new Registers());

            var actualIndex = instruction.Execute(currentIndex);

            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase("jnz 0 2", 4, 5)]
        [TestCase("jnz 0 12", 3, 4)]
        [TestCase("jnz 0 8", 2, 3)]
        [TestCase("jnz 0 -1", 3, 4)]
        public void return_incremented_index_if_int_value_is_0(string instructionString, int currentIndex, int expectedIndex)
        {
            var instruction = new JumpInstruction(instructionString, new Registers());

            var actualIndex = instruction.Execute(currentIndex);

            Assert.AreEqual(expectedIndex, actualIndex);
        }
    }
}
