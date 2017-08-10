using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day11;

namespace AdventOfCode
{
    public static class Any
    {
        private static Random _random = new Random();
        internal const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        internal const string Numbers = "0123456789";
        internal const int MaxNumberOfFloors = 4;
        internal const int TopFloor = MaxNumberOfFloors - 1;

        public static BuildingState BuildingState(int numberOfElements)
        {
            var elementNames = ElementNames(numberOfElements);
            var buildingState = new BuildingState(elementNames);

            for (var i = 0; i < numberOfElements; i++)
            {
                var elementName = elementNames[i];
                buildingState.SetChipLocation(elementName, Floor());
                buildingState.SetGeneratorLocation(elementName, Floor());
            }

            buildingState.ElevatorLocation = Floor();

            return buildingState;
        }

        public static IList<char> ElementNames(int numberOfElements)
        {
            var elementNames = new List<char>();

            for (var i = 0; i < numberOfElements; i++)
            {
                elementNames.Add(ElementName());
            }

            return elementNames;
        }

        public static BuildingState GoalBuildingState(int numberOfElements)
        {
            return Day11.BuildingState.CreateGoalState(BuildingState(numberOfElements));
        }

        public static char ElementName()
        {
            return RandomString(1, Alphabet)[0];
        }

        private static string RandomString(int length, string allowedCharacters = Alphabet + Numbers)
        {
            return new string(Enumerable.Repeat(allowedCharacters, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static int Floor()
        {
            return _random.Next(0, MaxNumberOfFloors);
        }

        public static int DifferentFloor(int sharedFloor)
        {
            return (sharedFloor + 1) % MaxNumberOfFloors;
        }

        public static int FloorExceptTop()
        {
            return _random.Next(0, MaxNumberOfFloors - 1);
        }

        public static char ElementOnState(BuildingState currentState)
        {
            var elementNames = currentState.ElementNames.ToList();

            var index = _random.Next(0, elementNames.Count);

            return elementNames[index];
        }
    }
}
