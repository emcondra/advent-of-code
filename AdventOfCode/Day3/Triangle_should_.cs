using NUnit.Framework;

namespace AdventOfCode.Day3
{
    public class Triangle_should_
    {

        [TestCase(false, 1, 2, 3)]
        [TestCase(false, 1, 7, 3)]
        [TestCase(false, 7, 1, 3)]
        [TestCase(true, 3, 3, 3)]
        [TestCase(false, 3, 3, 10)]
        [TestCase(false, 2, 3, 1)]
        [TestCase(false, 3, 1, 2)]
        public void return_true_if_triangle_is_valid(bool expectedResponse, params int[] triangle)
        {
            var triangleValidator = new Triangle(triangle);

            var isValid = triangleValidator.IsValid();

            Assert.AreEqual(expectedResponse, isValid);
        }
    }
}
