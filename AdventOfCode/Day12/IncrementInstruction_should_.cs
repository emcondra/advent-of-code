using NUnit.Framework;

namespace AdventOfCode.Day12
{
    public class IncrementInstruction_should_
    {
        [TestCase("inc a", 'a')]
        [TestCase("inc b", 'b')]
        [TestCase("inc c", 'c')]
        [TestCase("inc d", 'd')]
        [Test]
        public void increment_value_of_register_by_one(string instructionString, char register)
        {
            var registers = new Registers();
            var currentValueOfRegister = registers[register];

            var instruction = new IncrementInstruction(instructionString, registers);

            var expectedValueOfRegister = currentValueOfRegister + 1;

            instruction.Execute(0);

            var actualValueOfRegister = registers[register];

            Assert.AreEqual(expectedValueOfRegister, actualValueOfRegister);
        }
    }
}
