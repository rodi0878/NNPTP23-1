using NNPTPZ1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class Polynomial
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);


        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derivative()
        {
            Polynomial derivatedPolynomial = new Polynomial();
            for (int q = 1; q < Coefficients.Count; q++)
            {
                derivatedPolynomial.Coefficients.Add(Coefficients[q].Multiply(new ComplexNumber() { Real = q }));
            }

            return derivatedPolynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double x)
        {
            var y = Evaluate(new ComplexNumber() { Real = x, Imaginary = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber zero = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber currentTerm = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        currentTerm = currentTerm.Multiply(x);

                    coefficient = coefficient.Multiply(currentTerm);
                }

                zero = zero.Add(coefficient);
            }

            return zero;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string termStrings = "";
            int i = 0;
            for (; i < Coefficients.Count; i++)
            {
                termStrings += Coefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        termStrings += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    termStrings += " + ";
            }
            return termStrings;
        }
    }
}
