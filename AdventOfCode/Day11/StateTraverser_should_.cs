using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode.Day11
{
    public class StateTraverser_should_
    {
        [Test]
        public void indicate_current_state_is_goal_state()
        {
            var buildingState = new BuildingState(new List<char>() { 'H', 'L' });
            buildingState.SetChipLocation('H', 3);
            buildingState.SetGeneratorLocation('H', 3);
            buildingState.SetChipLocation('L', 3);
            buildingState.SetGeneratorLocation('L', 3);
            buildingState.ElevatorLocation = 3;

            var goalState = BuildingState.CreateGoalState(buildingState);

            var stateTraverser = new StateTraverser();

            var stepsToGoalState = stateTraverser.GetStepsToGoalState(buildingState, goalState);

            Assert.AreEqual(0, stepsToGoalState);
        }

        [Test]
        public void indicate_no_possible_path_from_current_state()
        {
            var buildingState = new BuildingState(new List<char>() { 'H', 'L', 'S' });
            buildingState.SetChipLocation('H', 0);
            buildingState.SetGeneratorLocation('H', 0);
            buildingState.SetChipLocation('L', 1);
            buildingState.SetGeneratorLocation('L', 3);
            buildingState.SetChipLocation('S', 2);
            buildingState.SetGeneratorLocation('S', 2);
            buildingState.ElevatorLocation = 1;

            var goalState = BuildingState.CreateGoalState(buildingState);

            var stateTraverser = new StateTraverser();

            var stepsToGoalState = stateTraverser.GetStepsToGoalState(buildingState, goalState);

            Assert.AreEqual(-1, stepsToGoalState);
        }

        [Test]
        public void indicate_current_state_is_one_step_away_from_goal_state()
        {
            var buildingState = new BuildingState(new List<char>() { 'H', 'L' });
            buildingState.SetChipLocation('H', 3);
            buildingState.SetGeneratorLocation('H', 3);
            buildingState.SetChipLocation('L', 2);
            buildingState.SetGeneratorLocation('L', 3);
            buildingState.ElevatorLocation = 2;

            var goalState = BuildingState.CreateGoalState(buildingState);

            var stateTraverser = new StateTraverser();

            var stepsToGoalState = stateTraverser.GetStepsToGoalState(buildingState, goalState);

            Assert.AreEqual(1, stepsToGoalState);
        }

        [Test]
        public void indicate_current_state_is_two_steps_away_from_goal_state()
        {
            var buildingState = new BuildingState(new List<char>() { 'H', 'L' });
            buildingState.SetChipLocation('H', 3);
            buildingState.SetGeneratorLocation('H', 3);
            buildingState.SetChipLocation('L', 1);
            buildingState.SetGeneratorLocation('L', 3);
            buildingState.ElevatorLocation = 1;

            var goalState = BuildingState.CreateGoalState(buildingState);

            var stateTraverser = new StateTraverser();

            var stepsToGoalState = stateTraverser.GetStepsToGoalState(buildingState, goalState);

            Assert.AreEqual(2, stepsToGoalState);
        }

        [Test]
        public void indicate_current_state_is_11_steps_away_from_goal_state()
        {
            var buildingState = new BuildingState(new List<char>() { 'H', 'L' });
            buildingState.SetChipLocation('H', 0);
            buildingState.SetGeneratorLocation('H', 1);
            buildingState.SetChipLocation('L', 0);
            buildingState.SetGeneratorLocation('L', 2);
            buildingState.ElevatorLocation = 0;

            var goalState = BuildingState.CreateGoalState(buildingState);

            var stateTraverser = new StateTraverser();

            var stepsToGoalState = stateTraverser.GetStepsToGoalState(buildingState, goalState);

            Assert.AreEqual(11, stepsToGoalState);
        }

        [Test]
        public void indicate_current_state_is_10_steps_away_from_goal_state()
        {
            var buildingState = new BuildingState(new List<char>() { 'H', 'L' });
            buildingState.SetChipLocation('H', 1);
            buildingState.SetGeneratorLocation('H', 1);
            buildingState.SetChipLocation('L', 0);
            buildingState.SetGeneratorLocation('L', 2);
            buildingState.ElevatorLocation = 1;

            var goalState = BuildingState.CreateGoalState(buildingState);

            var stateTraverser = new StateTraverser();

            var stepsToGoalState = stateTraverser.GetStepsToGoalState(buildingState, goalState);

            Assert.AreEqual(10, stepsToGoalState);
        }
    }
}
