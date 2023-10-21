using System;

namespace NNPTPZ1.Mathematics
{
    public static class ComplexExtensions
    {
        public static Complex Exponentiate(this Complex @base, int exponent)
        {
            if (exponent == 0)
            {
                return Complex.One;
            }
            if (exponent < 0)
            {
                @base = @base.GetReciprocal();
                exponent = -exponent;
            }
            Complex result = @base;
            for (int i = 1; i < exponent; i++)
            {
                result *= @base;
            }
            return result;
        }
        public static Complex GetReciprocal(this Complex value)
        {
            if (value == Complex.Zero)
            {
                throw new DivideByZeroException();
            }
            double magnitudeSquared = value.GetSquaredMagnitude();
            return new Complex()
            {
                Real = value.Real / magnitudeSquared,
                Imaginary = -value.Imaginary / magnitudeSquared
            };
        }
        public static double GetSquaredMagnitude(this Complex value)
        {
            return (value.Real * value.Real) + (value.Imaginary * value.Imaginary);
        }
    }
}
