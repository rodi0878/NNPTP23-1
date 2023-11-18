using System.Collections.Generic;

namespace Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber complexNumber) => Coefficients.Add(complexNumber);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial derivatedPolynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivatedPolynomial.Add(Coefficients[i].Multiply(new ComplexNumber() { RealNumber = i }));
            }
            return derivatedPolynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber EvaluateAt(double x)
        {
            ComplexNumber y = EvaluateAt(
                new ComplexNumber()
                {
                    RealNumber = x,
                    ImaginaryUnit = 0
                });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="complexNumber">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber EvaluateAt(ComplexNumber complexNumber)
        {
            ComplexNumber evaluated = ComplexNumber.Zero;
            for (int power = 0; power < Coefficients.Count; power++)
            {
                ComplexNumber coefficient = Coefficients[power];
                ComplexNumber poweredX = complexNumber;
                if (power > 0)
                {
                    for (int i = 0; i < power - 1; i++)
                    {
                        poweredX = poweredX.Multiply(complexNumber);
                    }
                    coefficient = coefficient.Multiply(poweredX);
                }
                evaluated = evaluated.Add(coefficient);
            }
            return evaluated;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    result += " + ";
            }
            return result;
        }
    }
}