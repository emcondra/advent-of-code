using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode.Day11
{
    public class BuildingState_should_
    {
        [Test]
        public void stores_elevator_location()
        {
            var buildingState = CreateBuildingState();

            buildingState.ElevatorLocation = 3;

            Assert.AreEqual(3, buildingState.ElevatorLocation);
        }

        private static BuildingState CreateBuildingState(int elevatorLocation = 0, params char[] elementNames)
        {
            BuildingState buildingState;

            if (elementNames.Length > 0)
            {
                buildingState = new BuildingState(elementNames);
            }
            else
            {
                buildingState = new BuildingState(new List<char> { 'H', 'L' });
            }

            buildingState.ElevatorLocation = elevatorLocation;
            return buildingState;
        }

        [Test]
        public void consider_elevator_location_in_state_equality()
        {
            var buildingState = CreateBuildingState(3);
            var buildingState2 = CreateBuildingState(3);

            Assert.AreEqual(buildingState.GetHashCode(), buildingState2.GetHashCode());
        }

        [Test]
        public void consider_elevator_location_in_state_inequality()
        {
            var buildingState = CreateBuildingState(3);
            var buildingState2 = CreateBuildingState(2);

            Assert.AreNotEqual(buildingState.GetHashCode(), buildingState2.GetHashCode());
        }

        [Test]
        public void stores_chip_locations()
        {
            var buildingState = CreateBuildingState();

            buildingState.SetChipLocation('H', 2);

            Assert.AreEqual(2, buildingState.GetChipLocation('H'));
        }

        [Test]
        public void stores_generator_locations()
        {
            var buildingState = CreateBuildingState();

            buildingState.SetGeneratorLocation('H', 2);

            Assert.AreEqual(2, buildingState.GetGeneratorLocation('H'));
        }

        [Test]
        public void clone_building_state()
        {
            var buildingState1 = Any.BuildingState(3);

            var buildingState2 = buildingState1.Clone();

            foreach (var element in buildingState1.ElementNames)
            {
                Assert.AreEqual(buildingState1.GetChipLocation(element), buildingState2.GetChipLocation(element));
                Assert.AreEqual(buildingState1.GetGeneratorLocation(element), buildingState2.GetGeneratorLocation(element));
            }

            Assert.AreEqual(buildingState1.ElevatorLocation, buildingState2.ElevatorLocation);
        }

        [Test]
        public void consider_location_of_chip_and_generator_and_elevator_in_state_equality()
        {
            var buildingState1 = Any.BuildingState(3);

            var buildingState2 = buildingState1.Clone();

            Assert.AreEqual(buildingState1.GetHashCode(), buildingState2.GetHashCode());
        }

        [Test]
        public void consider_chip_location_in_state_inequality()
        {
            var buildingState1 = Any.BuildingState(3);
            var buildingState2 = buildingState1.Clone();

            var elementName = buildingState2.ElementNames.First();
            var chipLocation = buildingState2.GetChipLocation(elementName);
            buildingState2.SetChipLocation(elementName, chipLocation+1);

            Assert.AreNotEqual(buildingState1.GetHashCode(), buildingState2.GetHashCode());
        }

        [Test]
        public void consider_generator_location_in_state_inequality()
        {
            var buildingState1 = Any.BuildingState(3);
            var buildingState2 = buildingState1.Clone();

            var elementName = buildingState2.ElementNames.First();
            var generatorLocation = buildingState2.GetGeneratorLocation(elementName);
            buildingState2.SetGeneratorLocation(elementName, generatorLocation + 1);

            Assert.AreNotEqual(buildingState1.GetHashCode(), buildingState2.GetHashCode());
        }

        [Test]
        public void determine_state_is_invalid_when_unprotected_chip_is_on_same_floor_as_nonmatching_generator()
        {
            var sharedFloor = Any.Floor();
            var differentFloor = Any.DifferentFloor(sharedFloor);

            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', sharedFloor);
            buildingState.SetGeneratorLocation('H', differentFloor);
            buildingState.SetChipLocation('L', Any.Floor());
            buildingState.SetGeneratorLocation('L', sharedFloor);

            Assert.IsFalse(buildingState.IsValid());
        }

        [Test]
        public void determine_state_is_valid_when_protected_chip_is_on_same_floor_as_nonmatching_generator()
        {
            var sharedFloor = Any.Floor();

            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', sharedFloor);
            buildingState.SetGeneratorLocation('H', sharedFloor);
            buildingState.SetChipLocation('L', Any.Floor());
            buildingState.SetGeneratorLocation('L', sharedFloor);

            Assert.IsTrue(buildingState.IsValid());
        }

        [Test]
        public void determine_state_is_not_goal_state_if_elevator_is_not_on_top_floor()
        {
            var currentState = Any.GoalBuildingState(3);
            currentState.ElevatorLocation = 2;

            Assert.IsFalse(currentState.IsGoalState());
        }

        [Test]
        public void determine_state_is_not_goal_state_when_chip_is_not_on_top_floor()
        {
            var currentState = Any.GoalBuildingState(3);
            var element = Any.ElementOnState(currentState);
            currentState.SetChipLocation(element, Any.FloorExceptTop());

            Assert.IsFalse(currentState.IsGoalState());
        }

        [Test]
        public void determine_state_is_not_goal_state_when_generator_is_not_on_top_floor()
        {
            var currentState = Any.GoalBuildingState(3);
            var element = Any.ElementOnState(currentState);
            currentState.SetGeneratorLocation(element, Any.FloorExceptTop());

            Assert.IsFalse(currentState.IsGoalState());
        }

        [Test]
        public void determine_if_current_state_meets_goal()
        {
            var goalState = Any.GoalBuildingState(3);

            Assert.IsTrue(goalState.IsGoalState());
        }

        [Test]
        public void generate_next_state_when_only_one_chip_can_move_up()
        {
            var buildingState = CreateBuildingState(0, 'H');
            buildingState.SetChipLocation('H', 0);
            buildingState.SetGeneratorLocation('H', 1);
            buildingState.ElevatorLocation = 0;

            var expectedState = CreateBuildingState(0, 'H');
            expectedState.SetChipLocation('H', 1);
            expectedState.SetGeneratorLocation('H', 1);
            expectedState.ElevatorLocation = 1;

            var nextStates = new HashSet<BuildingState>(buildingState.GenerateNextStates());

            Assert.AreEqual(1, nextStates.Count);
            CollectionAssert.Contains(nextStates, expectedState);
        }

        [Test]
        public void generate_next_state_when_only_one_generator_can_move_up()
        {
            var buildingState = CreateBuildingState(0, 'H');
            buildingState.SetChipLocation('H', 1);
            buildingState.SetGeneratorLocation('H', 0);
            buildingState.ElevatorLocation = 0;

            var expectedState = CreateBuildingState(1, 'H');
            expectedState.SetChipLocation('H', 1);
            expectedState.SetGeneratorLocation('H', 1);
            expectedState.ElevatorLocation = 1;

            var nextStates = new HashSet<BuildingState>(buildingState.GenerateNextStates());

            CollectionAssert.Contains(nextStates, expectedState);
            Assert.AreEqual(1, nextStates.Count);
        }

        [Test]
        public void generate_next_state_when_only_one_chip_can_move_down()
        {
            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', 3);
            buildingState.SetGeneratorLocation('H', 2);
            buildingState.ElevatorLocation = 3;

            var expectedState = CreateBuildingState();
            expectedState.SetChipLocation('H', 2);
            expectedState.SetGeneratorLocation('H', 2);
            expectedState.ElevatorLocation = 2;

            var nextStates = new HashSet<BuildingState>(buildingState.GenerateNextStates());

            CollectionAssert.Contains(nextStates, expectedState);
            Assert.AreEqual(1, nextStates.Count);
        }

        [Test]
        public void generate_next_state_when_only_one_generator_can_move_down()
        {
            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', 2);
            buildingState.SetGeneratorLocation('H', 3);
            buildingState.ElevatorLocation = 3;

            var expectedState = CreateBuildingState();
            expectedState.SetChipLocation('H', 2);
            expectedState.SetGeneratorLocation('H', 2);
            expectedState.ElevatorLocation = 2;

            var nextStates = new HashSet<BuildingState>(buildingState.GenerateNextStates());

            CollectionAssert.Contains(nextStates, expectedState);
            Assert.AreEqual(1, nextStates.Count);
        }

        [Test]
        public void generate_next_states_when_only_one_chip_can_move_up_or_down()
        {
            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', 2);
            buildingState.SetGeneratorLocation('H', 0);
            buildingState.ElevatorLocation = 2;

            var expectedState1 = CreateBuildingState();
            expectedState1.SetChipLocation('H', 3);
            expectedState1.SetGeneratorLocation('H', 0);
            expectedState1.ElevatorLocation = 3;

            var expectedState2 = CreateBuildingState();
            expectedState2.SetChipLocation('H', 1);
            expectedState2.SetGeneratorLocation('H', 0);
            expectedState2.ElevatorLocation = 1;

            var nextStates = new HashSet<BuildingState>(buildingState.GenerateNextStates());

            CollectionAssert.Contains(nextStates, expectedState1);
            CollectionAssert.Contains(nextStates, expectedState2);
        }

        [Test]
        public void generate_next_states_when_only_one_generator_can_move_up_or_down()
        {
            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', 0);
            buildingState.SetGeneratorLocation('H', 2);
            buildingState.ElevatorLocation = 2;

            var expectedState1 = CreateBuildingState();
            expectedState1.SetChipLocation('H', 0);
            expectedState1.SetGeneratorLocation('H', 3);
            expectedState1.ElevatorLocation = 3;

            var expectedState2 = CreateBuildingState();
            expectedState2.SetChipLocation('H', 0);
            expectedState2.SetGeneratorLocation('H', 1);
            expectedState2.ElevatorLocation = 1;

            var nextStates = new HashSet<BuildingState>(buildingState.GenerateNextStates());

            CollectionAssert.Contains(nextStates, expectedState1);
            CollectionAssert.Contains(nextStates, expectedState2);
        }

        [Test]
        public void generate_next_state_when_multiple_components_are_on_top_floor()
        {
            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', 3);
            buildingState.SetGeneratorLocation('H', 3);
            buildingState.ElevatorLocation = 3;

            var expectedState = CreateBuildingState();
            expectedState.SetChipLocation('H', 2);
            expectedState.SetGeneratorLocation('H', 2);
            expectedState.ElevatorLocation = 2;

            var nextStates = new HashSet<BuildingState>(buildingState.GenerateNextStates());

            CollectionAssert.Contains(nextStates, expectedState);
        }

        [Test]
        public void generate_next_state_when_multiple_components_are_on_bottom_floor()
        {
            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', 0);
            buildingState.SetGeneratorLocation('H', 0);
            buildingState.ElevatorLocation = 0;

            var expectedState = CreateBuildingState();
            expectedState.SetChipLocation('H', 1);
            expectedState.SetGeneratorLocation('H', 1);
            expectedState.ElevatorLocation = 1;

            var nextStates = new HashSet<BuildingState>(buildingState.GenerateNextStates());

            CollectionAssert.Contains(nextStates, expectedState);
        }



        [Test]
        public void generate_valid_next_states()
        {
            var buildingState = CreateBuildingState();
            buildingState.SetChipLocation('H', 1);
            buildingState.SetGeneratorLocation('H', 1);
            buildingState.SetChipLocation('L', 0);
            buildingState.SetGeneratorLocation('L', 2);

            var validNextStates = buildingState.GetValidNextStates();

            foreach (var validState in validNextStates)
            {
                Assert.IsTrue(validState.IsValid());
            }
        }

        [Test]
        public void create_goal_state()
        {
            var currentBuildingState = CreateBuildingState();
            currentBuildingState.SetChipLocation('H', 1);
            currentBuildingState.SetGeneratorLocation('H', 1);
            currentBuildingState.SetChipLocation('L', 0);
            currentBuildingState.SetGeneratorLocation('L', 2);

            var goalState = BuildingState.CreateGoalState(currentBuildingState);

            Assert.IsTrue(goalState.IsGoalState());
        }

        [Test]
        public void generate_next_state_when_multiple_chips_are_on_same_floor()
        {
            var currentState = CreateBuildingState();
            currentState.SetChipLocation('H', 0);
            currentState.SetGeneratorLocation('H', 2);
            currentState.SetChipLocation('L', 0);
            currentState.SetGeneratorLocation('L', 2);
            currentState.ElevatorLocation = 0;

            var expectedState = CreateBuildingState();
            expectedState.SetChipLocation('H', 1);
            expectedState.SetGeneratorLocation('H', 2);
            expectedState.SetChipLocation('L', 1);
            expectedState.SetGeneratorLocation('L', 2);
            expectedState.ElevatorLocation = 1;

            var validNextStates = currentState.GetValidNextStates();

            CollectionAssert.Contains(validNextStates, expectedState);
        }

        [Test]
        public void generate_next_state_when_multiple_generators_are_on_same_floor()
        {
            var currentState = CreateBuildingState();
            currentState.SetChipLocation('H', 0);
            currentState.SetGeneratorLocation('H', 3);
            currentState.SetChipLocation('L', 0);
            currentState.SetGeneratorLocation('L', 3);
            currentState.ElevatorLocation = 3;

            var expectedState = CreateBuildingState();
            expectedState.SetChipLocation('H', 0);
            expectedState.SetGeneratorLocation('H', 2);
            expectedState.SetChipLocation('L', 0);
            expectedState.SetGeneratorLocation('L', 2);
            expectedState.ElevatorLocation = 2;

            var validNextStates = currentState.GetValidNextStates();

            CollectionAssert.Contains(validNextStates, expectedState);
        }
    }
}
