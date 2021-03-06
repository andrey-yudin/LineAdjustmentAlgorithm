namespace LineAdjustment.Tests
{
    using NUnit.Framework;

    public class LineAdjustmentAlgorithmTests
    {
        [Test]
        [TestCase(null, 5, "")]
        [TestCase("", 5, "")]
        [TestCase("test", 5, "test ")]
        [TestCase("Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", 12,
            "Lorem  ipsum\ndolor    sit\namet        \nconsectetur \nadipiscing  \nelit  sed do\neiusmod     \ntempor      \nincididunt  \nut labore et\ndolore magna\naliqua      ")]
        public void Simple(string input, int lineWidth, string expected)
        {
            var algorithm = new LineAdjustmentAlgorithm();
            var output = algorithm.Transform(input, lineWidth);
            Assert.AreEqual(expected, output);
        }
    }
}