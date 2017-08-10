namespace AdventOfCode.Day3
{
    public class Triangle
    {
        private readonly int[] _triangle;

        public Triangle(int[] triangle)
        { 
            _triangle = triangle;
        }

        public bool IsValid()
        {
            if (_triangle[0] + _triangle[1] <= _triangle[2])
                return false;

            if (_triangle[0] + _triangle[2] <= _triangle[1])
                return false;

            if (_triangle[1] + _triangle[2] <= _triangle[0])
                return false;

            return true;
        }
    }
}
