using NUnit.Framework;

namespace AdventOfCode.Day12
{
    public class CopyInstruction_should_
    {
        [TestCase("cpy 4 a", 4, 'a')]
        [TestCase("cpy 16 b", 16, 'b')]
        [TestCase("cpy 8 c", 8, 'c')]
        [TestCase("cpy 7 d", 7, 'd')]
        [Test]
        public void copy_value_into_register(string stringInstruction, int expectedValueOfRegister, char register)
        {
            var registers = new Registers();

            var instruction = new CopyInstruction(stringInstruction, registers);

            instruction.Execute(0);

            var actualValueOfRegister = registers[register];

            Assert.AreEqual(expectedValueOfRegister, actualValueOfRegister);
        }
    }
}
