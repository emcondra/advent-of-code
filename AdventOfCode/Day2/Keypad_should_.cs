using NUnit.Framework;

namespace AdventOfCode.Day2
{
    public class Keypad_should_
    {
        [Test]
        public void start_at_5()
        {
            var keypad = new Keypad();

            Assert.AreEqual('5', keypad.CurrentValue);
        }

        [TestCase("U")]
        [TestCase("UU")]
        [Test]
        public void move_to_2_if_given_up_command_from_starting_position(string move)
        {
            var keypad = new Keypad();

            keypad.Move(move);

            Assert.AreEqual('2', keypad.CurrentValue);
        }

        [TestCase("L")]
        [TestCase("LL")]
        public void move_to_4_if_given_left_command_from_starting_position(string move)
        {
            var keypad = new Keypad();

            keypad.Move(move);

            Assert.AreEqual('4', keypad.CurrentValue);
        }

        [TestCase("R")]
        [TestCase("RR")]
        public void move_to_6_if_given_right_command_from_starting_position(string move)
        {
            var keypad = new Keypad();

            keypad.Move(move);

            Assert.AreEqual('6', keypad.CurrentValue);
        }

        [TestCase("D")]
        [TestCase("DD")]
        public void move_to_8_if_given_down_command_from_starting_position(string move)
        {
            var keypad = new Keypad();

            keypad.Move(move);

            Assert.AreEqual('8', keypad.CurrentValue);
        }

        [Test]
        public void return_code_when_given_sequence_of_commands()
        {
            var commands = new [] { "ULL", "RRDDD", "LURDL", "UUUUD" };

            var keypad = new Keypad();

            var code = keypad.Decode(commands);

            Assert.AreEqual("1985", code);
        }
    }

}
