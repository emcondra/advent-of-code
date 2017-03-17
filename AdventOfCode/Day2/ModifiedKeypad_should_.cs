using NUnit.Framework;

namespace AdventOfCode.Day2
{
    public class ModifiedKeypad_should_
    {
        [Test]
        public void start_at_5()
        {
            var keypad = new ModifiedKeypad();

            Assert.AreEqual('5', keypad.CurrentValue);
        }

        [TestCase("U")]
        [TestCase("UU")]
        [TestCase("D")]
        [TestCase("DD")]
        [TestCase("L")]
        [TestCase("LL")]
        [Test]
        public void remain_on_5_if_given_up_or_down_or_left_command_from_starting_position(string move)
        {
            var keypad = new ModifiedKeypad();

            keypad.Move(move);

            Assert.AreEqual('5', keypad.CurrentValue);
        }

        [TestCase("R")]
        public void move_to_6_if_given_right_command_from_starting_position(string move)
        {
            var keypad = new ModifiedKeypad();

            keypad.Move(move);

            Assert.AreEqual('6', keypad.CurrentValue);
        }

        [TestCase("R", '8')]
        [TestCase("RR", '9')]
        [TestCase("RRR", '9')]
        public void move_to_the_right_when_given_right_command_from_starting_position_of_7(string move, char expectedValue)
        {
            var keypad = new ModifiedKeypad
            {
                XPosition = 2,
                YPosition = 2
            };

            keypad.Move(move);

            Assert.AreEqual(expectedValue, keypad.CurrentValue);
        }

        [TestCase("L", '6')]
        [TestCase("LL", '5')]
        [TestCase("LLL", '5')]
        public void move_to_the_left_when_given_left_command_from_starting_position_of_7(string move, char expectedValue)
        {
            var keypad = new ModifiedKeypad
            {
                XPosition = 2,
                YPosition = 2
            };

            keypad.Move(move);

            Assert.AreEqual(expectedValue, keypad.CurrentValue);
        }

        [TestCase("U", '3')]
        [TestCase("UU", '1')]
        [TestCase("UUU", '1')]
        public void move_up_when_given_up_command_from_starting_position_of_7(string move, char expectedValue)
        {
            var keypad = new ModifiedKeypad
            {
                XPosition = 2,
                YPosition = 2
            };

            keypad.Move(move);

            Assert.AreEqual(expectedValue, keypad.CurrentValue);
        }

        [TestCase("D", 'B')]
        [TestCase("DD", 'D')]
        [TestCase("DDD", 'D')]
        public void move_down_when_given_down_command_from_starting_position_of_7(string move, int expectedValue)
        {
            var keypad = new ModifiedKeypad
            {
                XPosition = 2,
                YPosition = 2
            };

            keypad.Move(move);

            Assert.AreEqual(expectedValue, keypad.CurrentValue);
        }

        [Test]
        public void return_code_when_given_sequence_of_commands()
        {
            var commands = new[] { "ULL", "RRDDD", "LURDL", "UUUUD" };

            var keypad = new ModifiedKeypad();

            var expectedCode = "5DB3";

            var actualCode = keypad.Decode(commands);

            Assert.AreEqual(expectedCode, actualCode);
        }
    }
}
