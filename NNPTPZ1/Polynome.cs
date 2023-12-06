using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynome
        {
            public List<ComplexNumber> Coefficients { get; set; }

            public Polynome() => Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber coefficient) => Coefficients.Add(coefficient);

            public Polynome Derive()
            {
                Polynome derivate = new Polynome();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    derivate.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
                }

                return derivate;
            }

            public ComplexNumber Evaluate(double realPart)
            {
                return Evaluate(new ComplexNumber() { RealPart = realPart, ImaginaryPart = 0 });
            }

            public ComplexNumber Evaluate(ComplexNumber complexNumber)
            {
                ComplexNumber result = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber powerTerm = complexNumber;

                    if (i > 0)
                    {
                        for (int j = 0; j < i - 1; j++)
                        {
                            powerTerm = powerTerm.Multiply(complexNumber);
                        } 

                        coefficient = coefficient.Multiply(powerTerm);
                    }

                    result = result.Add(coefficient);
                }

                return result;
            }

            public override string ToString()
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    result.Append(Coefficients[i]);
                    for (int j = 0; j < i; j++)
                    {
                        result.Append("x");
                    }

                    if (i + 1 < Coefficients.Count)
                    {
                        result.Append(" + ");
                    }
                }
                return result.ToString();
            }
        }
    }
}
