using System.Collections.Generic;

namespace NNPTPZ1
{

    namespace Mathematics
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
            public Polynomial Derive()
            {
                Polynomial p = new Polynomial();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    p.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
                }

                return p;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double x)
            {
                var y = Eval(new ComplexNumber() { Real = x, Imaginary = 0 });
                return y;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(ComplexNumber x)
            {
                ComplexNumber result = ComplexNumber.Zero;
                ComplexNumber xPower = new ComplexNumber() { Real = 1, Imaginary = 0 };
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    //ComplexNumber coef = Coefficients[i];
                    //ComplexNumber bx = x;
                    //int power = i;

                    //if (i > 0)
                    //{
                    //    for (int j = 0; j < power - 1; j++)
                    //        bx = bx.Multiply(x);

                    //    coef = coef.Multiply(bx);
                    //}

                    //s = s.Add(coef);
                    result = result.Add(Coefficients[i].Multiply(xPower));
                    xPower = xPower.Multiply(x); // Calculate x^(i+1) for the next iteration
                }
                return result;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string s = "";
                int i = 0;
                for (; i < Coefficients.Count; i++)
                {
                    s += Coefficients[i];
                    if (i > 0)
                    {
                        int j = 0;
                        for (; j < i; j++)
                        {
                            s += "x";
                        }
                    }
                    if (i+1<Coefficients.Count)
                    s += " + ";
                }
                return s;
            }
        }
    }
}
