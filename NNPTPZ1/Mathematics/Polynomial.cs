using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        public Polynomial Derive()
        {
            Polynomial derivation = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivation.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return derivation;
        }
        public ComplexNumber Eval(double realPart)
        {
            return Eval(new ComplexNumber() { RealPart = realPart, ImaginaryPart = 0 });
        }

        public ComplexNumber Eval(ComplexNumber complexNumber)
        {
            ComplexNumber result = ComplexNumber.Zero;
            ComplexNumber complexNumberPower = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };

            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                result = result.Add(coefficient.Multiply(complexNumberPower));
                complexNumberPower = complexNumberPower.Multiply(complexNumber);
            }

            return result;
        }
        public override string ToString()
        {
            StringBuilder resultStringBuilder = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                resultStringBuilder.Append(Coefficients[i]);

                for (int j = 0; j < i; j++)
                {
                    resultStringBuilder.Append("x");
                }

                if (i + 1 < Coefficients.Count)
                    resultStringBuilder.Append(" + ");
            }
            return resultStringBuilder.ToString();
        }
    }
}