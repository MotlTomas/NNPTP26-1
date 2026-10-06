using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coefficients, from the constant term up
        /// </summary>
        public List<Complex> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<Complex>();

        public static Polynomial FromRealCoefficients(IEnumerable<double> coefficients)
        {
            var polynomial = new Polynomial();
            foreach (double coefficient in coefficients)
            {
                polynomial.Add(new Complex() { Real = coefficient });
            }
            return polynomial;
        }

        public void Add(Complex coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial derivative = new Polynomial();
            for (int power = 1; power < Coefficients.Count; power++)
            {
                derivative.Coefficients.Add(Coefficients[power].Multiply(new Complex() { Real = power }));
            }

            return derivative;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Complex Evaluate(Complex x)
        {
            Complex result = Complex.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                Complex term = Coefficients[i];
                Complex xPower = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        xPower = xPower.Multiply(x);

                    term = term.Multiply(xPower);
                }

                result = result.Add(term);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string text = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                text += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        text += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    text += " + ";
            }
            return text;
        }
    }
}
