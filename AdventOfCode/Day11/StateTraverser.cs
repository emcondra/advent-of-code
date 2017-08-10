using System.Collections.Generic;

namespace AdventOfCode.Day11
{
    public class StateTraverser
    {

        public int GetStepsToGoalState(BuildingState currentState, BuildingState goalState)
        {
            var currentStates = new Queue<BuildingState>();
            currentStates.Enqueue(currentState);

            var nextStates = new Queue<BuildingState>();
            var depth = 0;

            var visitedStates = new HashSet<int>();
            visitedStates.Add(currentState.GetHashCode());

            while (currentStates.Count > 0)
            {
                var current = currentStates.Dequeue();

                if (current.Equals(goalState))
                {
                    return depth;
                }

                var next = current.GetValidNextStates();

                foreach (var nextState in next)
                {
                    if (!visitedStates.Contains(nextState.GetHashCode()))
                    {
                        nextStates.Enqueue(nextState);
                        visitedStates.Add(nextState.GetHashCode());
                    }
                }

                if (currentStates.Count == 0)
                {
                    currentStates = nextStates;
                    nextStates = new Queue<BuildingState>();
                    depth++;
                }
            }

            return -1;
        }
    }
}
