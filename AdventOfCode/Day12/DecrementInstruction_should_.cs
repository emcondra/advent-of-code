using NUnit.Framework;

namespace AdventOfCode.Day12
{
    public class DecrementInstruction_should_
    {
        [TestCase("dec a", 'a')]
        [TestCase("dec b", 'b')]
        [TestCase("dec c", 'c')]
        [TestCase("dec d", 'd')]
        [Test]
        public void decrement_value_of_register_by_one(string instructionString, char register)
        {
            var registers = new Registers();
            var currentValueOfRegister = registers[register];

            var instruction = new DecrementInstruction(instructionString, registers);

            var expectedValueOfRegister = currentValueOfRegister - 1;

            instruction.Execute(0);

            var actualValueOfRegister = registers[register];

            Assert.AreEqual(expectedValueOfRegister, actualValueOfRegister);
        }
    }
}
