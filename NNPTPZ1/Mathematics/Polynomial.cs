using System.Collections.Generic;



namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<Complex> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<Complex>();

        public Polynomial(params Complex[] complexes) : this() => Coefficients.AddRange(complexes);

        public void Add(Complex coefficient) =>
            Coefficients.Add(coefficient);

        public Polynomial Derive()
        {
            Polynomial derivated = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivated.Coefficients.Add(Coefficients[i].Multiply(new Complex() { RealPart = i }));
            }

            return derivated;
        }

        public Complex Evaluate(double evaluateWith)
        {
            return Evaluate(new Complex() { RealPart = evaluateWith, ImaginaryPart = 0 });
        }

        public Complex Evaluate(Complex evaluated)
        {
            Complex result = Complex.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                Complex coefficient = Coefficients[i];
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        evaluated = evaluated.Multiply(evaluated);

                    coefficient = coefficient.Multiply(evaluated);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        public override string ToString()
        {
            string polynomial = "";
 
            for (int i = 0; i < Coefficients.Count; i++)
            {
                polynomial += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        polynomial += "x";
                    }
                }
                if (i + 1 < Coefficients.Count) polynomial += " + ";
            }
            return polynomial;
        }
    }
}
