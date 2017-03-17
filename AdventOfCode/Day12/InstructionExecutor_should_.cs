using NUnit.Framework;

namespace AdventOfCode.Day12
{
    public class InstructionExecutor_should_
    {

        [Test]
        public void execute_instructions_and_return_value_of_register_a()
        {
            var instructions = new[] { "cpy 41 a", "inc a", "inc a", "dec a", "jnz a 2", "dec a" };
            var executor = new InstructionExecutor(instructions);

            var expectValueOfRegisterA = 42;

            var actualValueOfRegisterA = executor.Execute();

            Assert.AreEqual(expectValueOfRegisterA, actualValueOfRegisterA);
        }
    }
}
