using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode.Day1
{
    public class Navigator_should_
    {
        [Test]
        public void begin_facing_north()
        {
            var navigator = new Navigator();

            Assert.AreEqual(Direction.North, navigator.Direction);
        }

        [Test]
        public void change_direction_to_east_when_given_right_command()
        {
            var navigator =  new Navigator();

            navigator.Move("R20");

            Assert.AreEqual(Direction.East, navigator.Direction);
        }

        [Test]
        public void change_direction_to_east_when_given_left_command()
        {
            var navigator = new Navigator();

            navigator.Move("L20");

            Assert.AreEqual(Direction.West, navigator.Direction);
        }

        [TestCase(Direction.East, Direction.South)]
        [TestCase(Direction.South, Direction.West)]
        [TestCase(Direction.West, Direction.North)]
        [TestCase(Direction.North, Direction.East)]
        public void change_direction_when_given_right_command(Direction existingDirection, Direction expectedNewDirection)
        {
            var navigator = new Navigator(existingDirection);

            navigator.Move("R10");

            Assert.AreEqual(expectedNewDirection, navigator.Direction);
        }

        [TestCase(Direction.East, Direction.North)]
        [TestCase(Direction.South, Direction.East)]
        [TestCase(Direction.West, Direction.South)]
        [TestCase(Direction.North, Direction.West)]
        public void change_direction_when_given_left_command(Direction existingDirection, Direction expectedNewDirection)
        {
            var navigator = new Navigator(existingDirection);

            navigator.Move("L10");

            Assert.AreEqual(expectedNewDirection, navigator.Direction);
        }

        [Test]
        public void have_starting_x_position_of_0()
        {
            var navigator = new Navigator();

            Assert.AreEqual(0, navigator.XPosition);
        }

        [Test]
        public void have_starting_y_position_of_0()
        {
            var navigator = new Navigator();

            Assert.AreEqual(0, navigator.YPosition);
        }

        [Test]
        public void increment_x_position_when_moving_east()
        {
            var navigator = new Navigator(Direction.East);

            navigator.UpdatePosition(10);

            Assert.AreEqual(10, navigator.XPosition);
        }

        [Test]
        public void decrement_x_position_when_moving_west()
        {
            var navigator = new Navigator(Direction.West);

            navigator.UpdatePosition(10);

            Assert.AreEqual(-10, navigator.XPosition);
        }

        [Test]
        public void increment_y_position_when_moving_north()
        {
            var navigator = new Navigator(Direction.North);

            navigator.UpdatePosition(10);

            Assert.AreEqual(10, navigator.YPosition);
        }

        [Test]
        public void decrement_y_position_when_moving_south()
        {
            var navigator = new Navigator(Direction.South);

            navigator.UpdatePosition(10);

            Assert.AreEqual(-10, navigator.YPosition);
        }

        [Test]
        public void calculate_shortest_path()
        {
            var navigator = new Navigator();

            navigator.Move("L3");
            navigator.Move("R5");
            navigator.Move("R2");

            var expectedDistance = 6;

            Assert.AreEqual(expectedDistance, navigator.ShortestPath);
        }

        [Test]
        public void return_current_position()
        {
            var navigator = new Navigator();

            var expectedPosition = "0,0";

            Assert.AreEqual(expectedPosition, navigator.CurrentPosition);
        }

        [Test]
        public void calculate_shortest_path_to_first_location_visited_twice()
        {
            var navigator = new Navigator();

            var expectedShortestPath = 1;

            var listOfCommands = new List<string>() { "L2", "R1", "R1", "R3" };

            var actualShortestPath = navigator.CalculateShortestPathToFirstLocationVisitedTwice(listOfCommands);

            Assert.AreEqual(expectedShortestPath, actualShortestPath);
        }
    }
}
