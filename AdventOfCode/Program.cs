using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day1;
using AdventOfCode.Day12;
using AdventOfCode.Day2;

namespace AdventOfCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Day1();

            //Day2();

            Day12();

            Console.ReadLine();
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
