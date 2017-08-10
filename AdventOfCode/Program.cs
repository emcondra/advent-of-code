using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day1;
using AdventOfCode.Day11;
using AdventOfCode.Day12;
using AdventOfCode.Day2;
using AdventOfCode.Day3;

namespace AdventOfCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Day1();

            //Day2();

            //Day3();

            //Day12();

            Day11();

            Console.ReadLine();
        }

        private static void Day11()
        {
            var currentState = new BuildingState(new List<char> {'P', 'T', 'Z', 'R', 'C', 'E', 'D'});
            currentState.ElevatorLocation = 0;
            currentState.SetChipLocation('P', 1);
            currentState.SetGeneratorLocation('P', 0);
            currentState.SetChipLocation('T', 0);
            currentState.SetGeneratorLocation('T', 0);
            currentState.SetChipLocation('Z', 1);
            currentState.SetGeneratorLocation('Z', 0);
            currentState.SetChipLocation('R', 0);
            currentState.SetGeneratorLocation('R', 0);
            currentState.SetChipLocation('C', 0);
            currentState.SetGeneratorLocation('C', 0);
            currentState.SetChipLocation('E', 0);
            currentState.SetGeneratorLocation('E', 0);
            currentState.SetChipLocation('D', 0);
            currentState.SetGeneratorLocation('D', 0);

            var stateTraverser = new StateTraverser();

            var goalState = BuildingState.CreateGoalState(currentState);

            var stepsToGoalState = stateTraverser.GetStepsToGoalState(currentState, goalState);

            Console.WriteLine("Steps: " + stepsToGoalState);
        }

        private static void Day12()
        {
            var instructions = LineByLine.GetLines("day12/day12_input.txt");
            var instructionExecutor = new InstructionExecutor(instructions.ToArray());
            var valueOfRegisterA = instructionExecutor.Execute();
            Console.WriteLine("Value of register A: " + valueOfRegisterA);
        }

        private static void Day2()
        {
            var codeOutput = CalculateBathroomCode();
            var modifiedKeypadCodeOutput = CalculateBathroomCodeOnModifiedKeypad();
            Console.WriteLine("Bathroom code: " + codeOutput);
            Console.WriteLine("Bathroom code on modified keypad: " + modifiedKeypadCodeOutput);
        }

        private static void Day1()
        {
            var input = "R3, L5, R1, R2, L5, R2, R3, L2, L5, R5, L4, L3, R5, L1, R3, R4, R1, L3, R3, L2, L5, L2, R4, R5, R5, L4, L3, L3, R4, R4, R5, L5, L3, R2, R2, L3, L4, L5, R1, R3, L3, R2, L3, R5, L194, L2, L5, R2, R1, R1, L1, L5, L4, R4, R2, R2, L4, L1, R2, R53, R3, L5, R72, R2, L5, R3, L4, R187, L4, L5, L2, R1, R3, R5, L4, L4, R2, R5, L5, L4, L3, R5, L2, R1, R1, R4, L1, R2, L3, R5, L4, R2, L3, R1, L4, R4, L1, L2, R3, L1, L1, R4, R3, L4, R2, R5, L2, L3, L3, L1, R3, R5, R2, R3, R1, R2, L1, L4, L5, L2, R4, R5, L2, R4, R4, L3, R2, R1, L4, R3, L3, L4, L3, L1, R3, L2, R2, L4, L4, L5, R3, R5, R3, L2, R5, L2, L1, L5, L1, R2, R4, L5, R2, L4, L5, L4, L5, L2, L5, L4, R5, R3, R2, R2, L3, R3, L2, L5";

            var values = input.Split(',').Select(s => s.Trim()).ToList();

            var shortestPath = CalculateShortestPath(values);
            Console.WriteLine("Shortest path = " + shortestPath);

            var shortestPathToFirstLocationVisitedTwice = CalculateShortestPathToFirstLocationVisitedTwice(values);
            Console.WriteLine("Shortest path to first location visited twice = " + shortestPathToFirstLocationVisitedTwice);
        }

        private static void Day3()
        {
            var lines = LineByLine.GetLines("Day3/day3_input.txt").ToList();
            var count = lines.Count();

            //Part1(count, lines);

            var triangles = Part2(count, lines);

            Console.WriteLine("Valid triangles = " + triangles.Count(t => t.IsValid()));
        }

        private static Triangle[] Part1(int count, List<string> lines)
        {
            var triangles = new Triangle[count];

            for (var i = 0; i < count; i++)
            {
                var splitLine = lines[i].Split((char[]) null, StringSplitOptions.RemoveEmptyEntries);
                var coordinates = splitLine.Select(coord => int.Parse(coord)).ToArray();
                var triangle = new Triangle(coordinates);
                triangles[i] = triangle;
            }

            return triangles;
        }

        private static Triangle[] Part2(int count, List<string> lines)
        {
            var triangles = new Triangle[count];

            for (var i = 0; i < count; i+=3)
            {

                var line1 = lines[i];
                var line1Values = lines[i].Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                var splitLine = lines[i].Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                var coordinates = splitLine.Select(coord => int.Parse(coord)).ToArray();
                var triangle = new Triangle(coordinates);
                triangles[i] = triangle;
            }

            return triangles; 
        }

        public static int CalculateShortestPath(List<string> values)
        {
            var navigator = new Navigator();

            foreach (var value in values)
            {
                navigator.Move(value);
            }

            return navigator.ShortestPath;
        }

        public static int CalculateShortestPathToFirstLocationVisitedTwice(List<string> values)
        {
            var navigator = new Navigator();

            return navigator.CalculateShortestPathToFirstLocationVisitedTwice(values);
        }

        public static string CalculateBathroomCode()
        {
            var commands = LineByLine.GetLines("day2_input.txt");
            var keypad = new Keypad();
            return keypad.Decode(commands);
        }

        public static string CalculateBathroomCodeOnModifiedKeypad()
        {
            var commands = LineByLine.GetLines("day2_input.txt");
            var keypad = new ModifiedKeypad();
            return keypad.Decode(commands);
        }
    }
}
