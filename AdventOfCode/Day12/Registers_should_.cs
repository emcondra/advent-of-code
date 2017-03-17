using NUnit.Framework;

namespace AdventOfCode.Day12
{
    public class Registers_should_
    {
        [TestCase('a')]
        [TestCase('b')]
        [TestCase('c')]
        [TestCase('d')]
        [Test]
        public void return_default_value_of_register(char register)
        {
            var registers = new Registers();

            Assert.AreEqual(0, registers[register]);
        }

        [TestCase('a', 1)]
        [TestCase('b', 1)]
        [TestCase('c', 1)]
        [TestCase('d', 1)]
        [Test]
        public void set_value_of_register(char register, int newValue)
        {
            var registers = new Registers();

            registers[register] = newValue;

            Assert.AreEqual(newValue, registers[register]);
        }
    }
}
