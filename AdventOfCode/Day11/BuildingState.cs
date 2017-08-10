using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day11
{
    public class BuildingState
    {
        private readonly int[] _chipLocations;
        private readonly int[] _generatorLocations;
        internal const int TopFloor = 3;

        public IList<char> ElementNames;
        public int NumberOfElements => ElementNames.Count();

        private int? _hashCode = null;
        private int _elevatorLocation;

        public BuildingState(IList<char> elementNames)
        {
            ElementNames = elementNames;
            _chipLocations = new int[NumberOfElements];
            _generatorLocations = new int[NumberOfElements];
        }



        public override int GetHashCode()
        {
            if (_hashCode != null)
            {
                return _hashCode.Value;
            }

            var hash = 0;

            hash = hash | (ElevatorLocation << 30);

            var listOfElements = ElementNames.ToList();
            var generatorOffset = NumberOfElements * 2;

            for (var i = 0; i < NumberOfElements; i++)
            {
                var element = listOfElements[i];
                var elementOffset = i * 2;

                var chipFloor = GetChipLocation(element);
                hash = hash | (chipFloor << elementOffset);

                var generatorfloor = GetGeneratorLocation(element);
                hash = hash | (generatorfloor << (generatorOffset + elementOffset));
            }

            _hashCode = hash;

            return hash;
        }

        public static BuildingState CreateGoalState(BuildingState currentState)
        {
            var goalState = new BuildingState(currentState.ElementNames)
            {
                ElevatorLocation = TopFloor
            };

            foreach (var element in currentState.ElementNames)
            {
                goalState.SetChipLocation(element, TopFloor);
                goalState.SetGeneratorLocation(element, TopFloor);
            }

            return goalState;
        }

        public int ElevatorLocation
        {
            get { return _elevatorLocation; }
            set
            {
                _elevatorLocation = value;
                _hashCode = null;
            }
        }

        public void SetChipLocation(char chipName, int floor)
        {
            _chipLocations[ElementNames.IndexOf(chipName)] = floor;
            _hashCode = null;
        }

        public int GetChipLocation(char chipName)
        {
            return _chipLocations[ElementNames.IndexOf(chipName)];
        }

        public void SetGeneratorLocation(char generatorName, int floor)
        {
            _generatorLocations[ElementNames.IndexOf(generatorName)] = floor;
            _hashCode = null;
        }

        public int GetGeneratorLocation(char generatorName)
        {
            return _generatorLocations[ElementNames.IndexOf(generatorName)];
        }

        public BuildingState Clone()
        {
            var buildingState = new BuildingState(ElementNames);

            Array.Copy(_chipLocations, buildingState._chipLocations, NumberOfElements);
            Array.Copy(_generatorLocations, buildingState._generatorLocations, NumberOfElements);

            buildingState.ElevatorLocation = ElevatorLocation;

            return buildingState;
        }

        public bool IsValid()
        {
            foreach (var elementName in ElementNames)
            {
                var chipLocation = GetChipLocation(elementName);

                var generatorLocation = GetGeneratorLocation(elementName);

                if (IsChipProtected(chipLocation, generatorLocation))
                    continue;

                if (IsNonmatchingGeneratorOnSameFloorAsChip(elementName, chipLocation))
                {
                    return false;
                }
                    
            }

            return true;
        }

        private static bool IsChipProtected(int chipLocation, int generatorLocation)
        {
            return chipLocation == generatorLocation;
        }

        private bool IsNonmatchingGeneratorOnSameFloorAsChip(char elementName, int chipLocation)
        {
            var indexOfElementName = ElementNames.IndexOf(elementName);

            for (var index = 0; index < _generatorLocations.Length; index++)
            {
                if (index == indexOfElementName)
                {
                    continue;
                }

                var generatorLocation = _generatorLocations[index];

                if (generatorLocation == chipLocation)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsGoalState()
        {
            if (ElevatorLocation != TopFloor)
                return false;

            foreach (var elementName in ElementNames)
            {
                var chipLocation = GetChipLocation(elementName);
                var generatorLocation = GetGeneratorLocation(elementName);

                if (chipLocation != TopFloor || generatorLocation != TopFloor)
                    return false;
            }

            return true;
        }

        public IEnumerable<BuildingState> GenerateNextStates()
        {
            var chipsAtElevatorFloor = GetChipsAtElevatorFloor();
            var generatorsAtElevatorFloor = GetGeneratorsAtElevatorFloor();

            for (var i = 0; i < chipsAtElevatorFloor.Count; i++)
            {
                var chip = chipsAtElevatorFloor[i];

                if (ElevatorLocation != 0)
                {
                    var newState = Clone();

                    newState.SetChipLocation(chip, --newState.ElevatorLocation);

                    yield return newState;

                    for (var index = 0; index < generatorsAtElevatorFloor.Count; index++)
                    {
                        var generator = generatorsAtElevatorFloor[index];
                        var newStateWithMovedGenerator = newState.Clone();

                        newStateWithMovedGenerator.SetGeneratorLocation(generator, newStateWithMovedGenerator.ElevatorLocation);

                        yield return newStateWithMovedGenerator;
                    }

                    for (var index = 0; index < chipsAtElevatorFloor.Count; index++)
                    {
                        if (index == i)
                        {
                            continue;
                        }

                        var c = chipsAtElevatorFloor[index];
                        var newStateWithMovedChip = newState.Clone();

                        newStateWithMovedChip.SetChipLocation(c, newStateWithMovedChip.ElevatorLocation);

                        yield return newStateWithMovedChip;
                    }
                }

                if (ElevatorLocation != TopFloor)
                {
                    var newState = Clone();

                    newState.SetChipLocation(chip, ++newState.ElevatorLocation);

                    yield return newState;

                    foreach (var generator in generatorsAtElevatorFloor)
                    {
                        var newStateWithMovedGenerator = newState.Clone();

                        newStateWithMovedGenerator.SetGeneratorLocation(generator, newStateWithMovedGenerator.ElevatorLocation);

                        yield return newStateWithMovedGenerator;
                    }

                    for (var index = 0; index < chipsAtElevatorFloor.Count; index++)
                    {
                        if (index == i)
                        {
                            continue;
                        }

                        var c = chipsAtElevatorFloor[index];
                        var newStateWithMovedChip = newState.Clone();

                        newStateWithMovedChip.SetChipLocation(c, newStateWithMovedChip.ElevatorLocation);

                        yield return newStateWithMovedChip;
                    }
                }
            }

            for (var i = 0; i < generatorsAtElevatorFloor.Count; i++)
            {
                var generator = generatorsAtElevatorFloor[i];

                if (ElevatorLocation != 0)
                {
                    var newState = Clone();

                    newState.SetGeneratorLocation(generator, --newState.ElevatorLocation);

                    yield return newState;

                    for (var index = 0; index < generatorsAtElevatorFloor.Count; index++)
                    {
                        if (index == i)
                        {
                            continue;
                        }

                        var g = generatorsAtElevatorFloor[index];
                        var newStateWithMovedGenerator = newState.Clone();

                        newStateWithMovedGenerator.SetGeneratorLocation(g, newStateWithMovedGenerator.ElevatorLocation);

                        yield return newStateWithMovedGenerator;
                    }
                }

                if (ElevatorLocation != TopFloor)
                {
                    var newState = Clone();

                    newState.SetGeneratorLocation(generator, ++newState.ElevatorLocation);

                    yield return newState;

                    for (var index = 0; index < generatorsAtElevatorFloor.Count; index++)
                    {
                        if (index == i)
                        {
                            continue;
                        }

                        var g = generatorsAtElevatorFloor[index];
                        var newStateWithMovedGenerator = newState.Clone();

                        newStateWithMovedGenerator.SetGeneratorLocation(g, newStateWithMovedGenerator.ElevatorLocation);

                        yield return newStateWithMovedGenerator;
                    }
                }
            }
        }

        private List<char> GetChipsAtElevatorFloor()
        {
            var chipsAtElevatorFloor = new List<char>();

            for (var index = 0; index < _chipLocations.Length; index++)
            {
                var chipLocation = _chipLocations[index];

                if (chipLocation == ElevatorLocation)
                {
                    chipsAtElevatorFloor.Add(ElementNames[index]);
                }
            }

            return chipsAtElevatorFloor;
        }

        private List<char> GetGeneratorsAtElevatorFloor()
        {
            var generatorsAtElevatorFloor = new List<char>();

            for (var index = 0; index < _generatorLocations.Length; index++)
            {
                var generatorLocation = _generatorLocations[index];

                if (generatorLocation == ElevatorLocation)
                {
                    generatorsAtElevatorFloor.Add(ElementNames[index]);
                }
            }

            return generatorsAtElevatorFloor;
        }

        public override string ToString()
        {
            var buildingStateString = new StringBuilder();
            buildingStateString
                .Append("E")
                .Append(ElevatorLocation)
                .Append(" ");

            foreach (var elementName in ElementNames)
            {
                buildingStateString
                    .Append(elementName)
                    .Append("M")
                    .Append(GetChipLocation(elementName))
                    .Append(" ");

                buildingStateString
                    .Append(elementName)
                    .Append("G")
                    .Append(GetGeneratorLocation(elementName))
                    .Append(" ");
            }

            return buildingStateString.ToString().Trim();
        }

        public override bool Equals(object obj)
        {
            return obj != null && GetHashCode() == obj.GetHashCode();
        }

        public IEnumerable<BuildingState> GetValidNextStates()
        {
            var nextStates = GenerateNextStates();

            return nextStates.Where(state => state.IsValid());
        }
    }
}
