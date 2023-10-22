using System.Collections.Generic;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynomial
        {
            public List<ComplexNumber> ListOfComplexNumbers { get; set; }

            public Polynomial() => ListOfComplexNumbers = new List<ComplexNumber>();

            public void Add(ComplexNumber newComplexNumber) =>
                ListOfComplexNumbers.Add(newComplexNumber);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynomial Derive()
            {
                Polynomial derivedPolynomial = new Polynomial();
                for (int i = 1; i < ListOfComplexNumbers.Count; i++)
                {
                    derivedPolynomial.ListOfComplexNumbers.Add(ListOfComplexNumbers[i].Multiply(new ComplexNumber() { RealElement = i }));
                }

                return derivedPolynomial;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="pointAsDecimalNumber">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double pointAsDecimalNumber)
            {
                return Eval(new ComplexNumber() { RealElement = pointAsDecimalNumber, ImaginaryElement = 0 });
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="pointAsComplexNumber">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(ComplexNumber pointAsComplexNumber)
            {
                ComplexNumber evaluatedComplexNumber = ComplexNumber.Zero;
                for (int i = 0; i < ListOfComplexNumbers.Count; i++)
                {
                    ComplexNumber complexNumberFromList = ListOfComplexNumbers[i];
                    ComplexNumber multipliedComplexNumber = pointAsComplexNumber;

                    if (i > 0)
                    {
                        for (int j = 0; j < i - 1; j++)
                            multipliedComplexNumber = multipliedComplexNumber.Multiply(pointAsComplexNumber);

                        complexNumberFromList = complexNumberFromList.Multiply(multipliedComplexNumber);
                    }

                    evaluatedComplexNumber = evaluatedComplexNumber.Add(complexNumberFromList);
                }

                return evaluatedComplexNumber;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string StringVersionOfPolynomial = "";
                
                for (int i = 0; i < ListOfComplexNumbers.Count; i++)
                {
                    StringVersionOfPolynomial += ListOfComplexNumbers[i];
                    if (i > 0)
                    {
                        StringVersionOfPolynomial += new string('x', i);
                    }
                    if (i + 1 < ListOfComplexNumbers.Count)
                        StringVersionOfPolynomial += " + ";
                }
                return StringVersionOfPolynomial;
            }
        }
    }
}
