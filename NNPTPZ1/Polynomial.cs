using System.Collections.Generic;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynomial
        {
            /// <summary>
            /// List of complex numbers in given polynomial
            /// </summary>
            public List<ComplexNumber> ComplexNumbers { get; set; }

            public Polynomial() => ComplexNumbers = new List<ComplexNumber>();
            public Polynomial(IEnumerable<ComplexNumber> polynomials) => ComplexNumbers = new List<ComplexNumber>(polynomials);


            public void Add(ComplexNumber complexNumber) =>
                ComplexNumbers.Add(complexNumber);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynomial Derive()
            {
                Polynomial polynomToDerivate = new Polynomial();
                for (int i = 1; i < ComplexNumbers.Count; i++)
                {
                    polynomToDerivate.ComplexNumbers.Add(ComplexNumbers[i].Multiply(new ComplexNumber() { RealPart = i }));
                }

                return polynomToDerivate;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns >y value of function for given x</returns>
            public ComplexNumber Evaluate(double x)
            {
                var y = Evaluate(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
                return y;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y value of function for given x</returns>
            public ComplexNumber Evaluate(ComplexNumber x)
            {
                ComplexNumber y = ComplexNumber.Zero;
                for (int i = 0; i < ComplexNumbers.Count; i++)
                {
                    ComplexNumber coeficient = ComplexNumbers[i];
                    ComplexNumber polynomialSum = x;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            polynomialSum = polynomialSum.Multiply(x);

                        coeficient = coeficient.Multiply(polynomialSum);
                    }

                    y = y.Add(coeficient);
                }

                return y;
            }

            public override string ToString()
            {
                string resultedString = "";
                for (int i = 0; i < ComplexNumbers.Count; i++)
                {
                    resultedString += ComplexNumbers[i];
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            resultedString += "x";
                        }
                    }
                    if (i + 1 < ComplexNumbers.Count)
                        resultedString += " + ";
                }
                return resultedString;
            }
        }
    }
}
