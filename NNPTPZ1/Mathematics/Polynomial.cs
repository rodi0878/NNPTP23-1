using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        private List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void AddCoefficient(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.AddCoefficient(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>result</returns>
        public ComplexNumber Evaluate(double x)
        {
            ComplexNumber result = Evaluate(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
            return result;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>result of evaluation</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coefficient = coefficient.Multiply(bx);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string polynomAsString = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                polynomAsString += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        polynomAsString += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    polynomAsString += " + ";
            }
            return polynomAsString;
        }
    }
}
