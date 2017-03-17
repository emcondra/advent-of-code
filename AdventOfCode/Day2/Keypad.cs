using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day2
{
    public class Keypad
    {
        public char CurrentValue => Grid[YPosition][XPosition];

        public virtual char[][] Grid { get; }

        public int XPosition { get; set; }

        public int YPosition { get; set; }

        public Keypad()
        {
            XPosition = 1;
            YPosition = 1;
            Grid = new []
            {
                new [] { '1', '2', '3' },
                new[] { '4', '5', '6' },
                new[] { '7', '8', '9' }
            };
        }

        public virtual void Move(string command)
        {
            var moves = command.ToCharArray();

            foreach (char move in moves)
            {
                switch (move)
                {
                    case 'U':
                        YPosition = Math.Max(0, YPosition - 1);
                        break;

                    case 'L':
                        XPosition = Math.Max(0, XPosition - 1);
                        break;

                    case 'R':
                        XPosition = Math.Min(2, XPosition + 1);
                        break;

                    case 'D':
                        YPosition = Math.Min(2, YPosition + 1);
                        break;
                }
            }
        }

        public string Decode(IEnumerable<string> commands)
        {
            var stringResult = new StringBuilder();

            foreach (var command in commands)
            {
                Move(command);

                stringResult = stringResult.Append(CurrentValue);
            }

            return stringResult.ToString();
        }
    }
}
