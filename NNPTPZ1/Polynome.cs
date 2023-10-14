using System.Collections.Generic;
using System.Linq;

namespace NNPTPZ1
{
    namespace Mathematics
    {
        public class Polynome
        {
            public List<ComplexNumber> Coefficients { get; set; }
            public Polynome()
            {
                Coefficients = new List<ComplexNumber>();
            }
            public void Add(ComplexNumber coefficient) => Coefficients.Add(coefficient);
            public Polynome Derive()
            {
                Polynome derivative = new Polynome();

                for (int i = 1; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i].Multiply(new ComplexNumber() { RealPart = i });
                    derivative.Coefficients.Add(coefficient);
                }
                return derivative;
            }
            public ComplexNumber Evaluate(double input)
            {
                return Evaluate(new ComplexNumber() { RealPart = input, ImaginaryPart = 0 });
            }
            public ComplexNumber Evaluate(ComplexNumber input)
            {
                ComplexNumber result = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber powerOfX = input;

                    if (i > 0)
                    {
                        for (int j = 0; j < i - 1; j++)
                        {
                            powerOfX = powerOfX.Multiply(input);
                        }
                        coefficient = coefficient.Multiply(powerOfX);
                    }
                    result = result.Add(coefficient);
                }
                return result;
            }
            public override string ToString()
            {
                return string.Join(" + ", Coefficients.Select((coefficient, index) => $"{coefficient}{new string('x', index)}"));
            }
        }
    }
}
