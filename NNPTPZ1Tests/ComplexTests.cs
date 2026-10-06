using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexTests
    {
        [TestMethod()]
        public void Add_ReturnsSumOfParts()
        {
            Complex a = new Complex()
            {
                Real = 10,
                Imaginary = 20
            };
            Complex b = new Complex()
            {
                Real = 1,
                Imaginary = 2
            };

            Complex actual = a.Add(b);
            Complex expected = new Complex()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(expected, actual);

            a = new Complex()
            {
                Real = 1,
                Imaginary = -1
            };
            b = new Complex() { Real = 0, Imaginary = 0 };
            expected = new Complex() { Real = 1, Imaginary = -1 };
            actual = a.Add(b);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToString_FormatsRealAndImaginaryPart()
        {
            Complex a = new Complex()
            {
                Real = 10,
                Imaginary = 20
            };
            Complex b = new Complex()
            {
                Real = 1,
                Imaginary = 2
            };

            var expectedText = "(10 + 20i)";
            var actualText = a.ToString();
            Assert.AreEqual(expectedText, actualText);
            expectedText = "(1 + 2i)";
            actualText = b.ToString();
            Assert.AreEqual(expectedText, actualText);

            a = new Complex()
            {
                Real = 1,
                Imaginary = -1
            };
            b = new Complex() { Real = 0, Imaginary = 0 };

            expectedText = "(1 + -1i)";
            actualText = a.ToString();
            Assert.AreEqual(expectedText, actualText);

            expectedText = "(0 + 0i)";
            actualText = b.ToString();
            Assert.AreEqual(expectedText, actualText);
        }

        [TestMethod()]
        public void Evaluate_ReturnsValueAtPoint()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });
            polynomial.Coefficients.Add(new Complex() { Real = 0, Imaginary = 0 });
            polynomial.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });
            Complex result = polynomial.Evaluate(new Complex() { Real = 0, Imaginary = 0 });
            var expected = new Complex() { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Evaluate(new Complex() { Real = 1, Imaginary = 0 });
            expected = new Complex() { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Evaluate(new Complex() { Real = 2, Imaginary = 0 });
            expected = new Complex() { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ToString_ListsCoefficientsWithPowers()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });
            polynomial.Coefficients.Add(new Complex() { Real = 0, Imaginary = 0 });
            polynomial.Coefficients.Add(new Complex() { Real = 1, Imaginary = 0 });

            var actualText = polynomial.ToString();
            var expectedText = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedText, actualText);
        }
    }
}
