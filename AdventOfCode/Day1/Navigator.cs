using System;
using System.Collections.Generic;

namespace AdventOfCode.Day1
{
    public class Navigator
    {
        public Direction Direction;
        private HashSet<string> _locationsVisited;
        private bool _anyLocationHasBeenVisitedTwice;
        public string CurrentPosition => XPosition + "," + YPosition;

        public int XPosition { get; private set; }
        public int YPosition { get; private set; }
        public int ShortestPath => Math.Abs(XPosition) + Math.Abs(YPosition);

        public Navigator(Direction direction = Direction.North)
        {
            Direction = direction;
            _locationsVisited = new HashSet<string>();
        }

        public void Move(string command)
        {
            var commandDirection = command[0];
            var points = int.Parse(command.Substring(1));

            if (commandDirection == 'R')
            {
                Direction = MoveRight(Direction);
                UpdatePosition(points);
            }
            else if (commandDirection == 'L')
            {
                Direction = MoveLeft(Direction);
                UpdatePosition(points);
            }
        }

        public void UpdatePosition(int points)
        {
            for (int i = 0; i < points; i++)
            {
                switch (Direction)
                {
                    case Direction.East:
                        XPosition++;
                        break;

                    case Direction.West:
                        XPosition--;
                        break;

                    case Direction.North:
                        YPosition++;
                        break;

                    case Direction.South:
                        YPosition--;
                        break;
                }

                if (_locationsVisited.Contains(CurrentPosition))
                {
                    _anyLocationHasBeenVisitedTwice = true;
                    break;
                }

                _locationsVisited.Add(CurrentPosition);
            }
        }

        protected Direction MoveRight(Direction startingDirection)
        {
            switch (startingDirection)
            {
                case Direction.East:
                    return Direction.South;

                case Direction.South:
                    return Direction.West;

                case Direction.West:
                    return Direction.North;

                case Direction.North:
                    return Direction.East;

                default:
                    return startingDirection;
            }
        }

        protected Direction MoveLeft(Direction startingDirection)
        {
            switch (startingDirection)
            {
                case Direction.East:
                    return Direction.North;

                case Direction.South:
                    return Direction.East;

                case Direction.West:
                    return Direction.South;

                case Direction.North:
                    return Direction.West;

                default:
                    return startingDirection;
            }
        }

        public int CalculateShortestPathToFirstLocationVisitedTwice(List<string> commands)
        {
            foreach (var command in commands)
            {
                Move(command);

                if (_anyLocationHasBeenVisitedTwice)
                {
                    return ShortestPath;
                }
            }

            return -1;
        }
    }
}
