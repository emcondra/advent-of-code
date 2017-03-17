using System;

namespace AdventOfCode.Day2
{
    public class ModifiedKeypad : Keypad
    {
        public override char[][] Grid { get; }

        public ModifiedKeypad()
        {
            XPosition = 0;
            YPosition = 2;
            Grid = new[]
            {
                new[] { '0', '0', '1', '0', '0' },
                new[] { '0', '2', '3', '4', '0' },
                new[] { '5', '6', '7', '8', '9' },
                new[] { '0', 'A', 'B', 'C', '0' },
                new[] { '0', '0', 'D', '0', '0' }
            };
        }

        public override void Move(string command)
        {
            var moves = command.ToCharArray();

            foreach (var move in moves)
            {
                switch (move)
                {
                    case 'U':
                    {
                        var newYPosition = Math.Max(0, YPosition - 1);
                        var valueOfAbove = Grid[XPosition][newYPosition];
                        if (valueOfAbove != '0')
                            YPosition = newYPosition;
                        break;
                    }

                    case 'D':
                    {
                        var newYPosition = Math.Min(4, YPosition + 1);
                        var valueOfBelow = Grid[XPosition][newYPosition];
                        if (valueOfBelow != '0')
                            YPosition = newYPosition;
                        break;
                    }

                    case 'L':
                    {
                        var newXPosition = Math.Max(0, XPosition - 1);
                        var valueOfLeft = Grid[newXPosition][YPosition];
                        if (valueOfLeft != '0')
                            XPosition = newXPosition;
                        break;
                    }

                    case 'R':
                    {
                        var newXPosition = Math.Min(4, XPosition + 1);
                        var valueOfLeft = Grid[newXPosition][YPosition];
                        if (valueOfLeft != '0')
                            XPosition = newXPosition;
                        break;
                    }
                }
            }
        }
    }
}
