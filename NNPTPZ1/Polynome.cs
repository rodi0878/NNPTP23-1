using System.Collections.Generic;

namespace Mathematics
{
    public class Polynome
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynome() => Coefficients = new List<ComplexNumber>();

        public void AddCoefficient(ComplexNumber coefficient) => Coefficients.Add(coefficient);

        public Polynome DerivatePolynome()
        {
            Polynome derivedPoly = new Polynome();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivedPoly.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return derivedPoly;
        }

        public ComplexNumber EvaluatePolynome(double x) => EvaluatePolynome(new ComplexNumber() { RealPart = x, ImaginariPart = 0 });

        public ComplexNumber EvaluatePolynome(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];

                for (int j = 0; j < i; j++)
                    coefficient = coefficient.Multiply(x);

                result = result.Add(coefficient);
            }
            return result;
        }

        public override string ToString()
        {
            string polyString = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                polyString += Coefficients[i];
                if (i > 0)
                {
                    polyString += new string('x', i);
                }
                if (i + 1 < Coefficients.Count)
                    polyString += " + ";
            }
            return polyString;
        }
    }
}
