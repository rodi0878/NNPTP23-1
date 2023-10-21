using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    [DebuggerDisplay("Polynomial: {ToString()}")]
    public class Polynomial
    {
        #region Properties
        protected List<Complex> Coefficients
        { get; set; }
        #endregion
        public Polynomial(params Complex[] coefficients)
        {
            Coefficients = new List<Complex>(coefficients);
        }
        public void AddCoefficient(Complex coefficient)
        {
            Coefficients.Add(coefficient);
        }
        public Polynomial GetDerivative()
        {
            var derivative = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                var derivedCoefficient = new Complex(i, 0) * Coefficients[i];
                derivative.AddCoefficient(derivedCoefficient);
            }
            return derivative;
        }
        public Complex Evaluate(double x)
        {
            return Evaluate(new Complex(x, 0));
        }
        public Complex Evaluate(Complex x)
        {
            Complex result = Complex.Zero;
            for (int exponent = 0; exponent < Coefficients.Count; exponent++)
            {
                Complex termValue = Coefficients[exponent] * x.Exponentiate(exponent);
                result += termValue;
            }
            return result;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Coefficients.Count; i++)
            {
                sb.Append(Coefficients[i].ToString());
                var power = i;
                if (power > 0)
                    sb.Append(new string('x', power));
                if (i < Coefficients.Count - 1)
                    sb.Append(" + ");
            }
            return sb.ToString();
        }
    }
}