using System;

namespace NNPTPZ1.Mathematics
{
    public struct Complex
    {
        public static readonly Complex Zero = new Complex()
        {
            Real = 0,
            Imaginary = 0
        };
        public static readonly Complex One = new Complex()
        {
            Real = 1,
            Imaginary = 0
        };
        #region Properties
        public double Real { get; set; }
        public double Imaginary { get; set; }
        public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);
        public double AngleRadians => Math.Atan(Imaginary / Real);
        #endregion
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }
        #region Operations
        public Complex Add(Complex addend)
        {
            return new Complex()
            {
                Real = Real + addend.Real,
                Imaginary = Imaginary + addend.Imaginary
            };
        }
        public Complex Subtract(Complex subtrahend)
        {
            return new Complex()
            {
                Real = Real - subtrahend.Real,
                Imaginary = Imaginary - subtrahend.Imaginary
            };
        }
        public Complex MultiplyBy(Complex multiplier)
        {
            return new Complex()
            {
                Real = (Real * multiplier.Real) - (Imaginary * multiplier.Imaginary),
                Imaginary = (Real * multiplier.Imaginary) + (Imaginary * multiplier.Real)
            };
        }
        public Complex DivideBy(Complex divisor)
        {
            if (divisor == Complex.Zero)
                throw new DivideByZeroException($"Invalid expression: `{this}/{divisor}`.");
            Complex divisorConjugate = new Complex(divisor.Real, -divisor.Imaginary);
            Complex numerator = this * divisorConjugate;
            double denominator = (divisor.Real * divisor.Real) + (divisor.Imaginary * divisor.Imaginary);
            return new Complex()
            {
                Real = numerator.Real / denominator,
                Imaginary = numerator.Imaginary / denominator
            };
        }
        public bool IsEqualTo(Complex other)
        {
            return Real == other.Real && Imaginary == other.Imaginary;
        }
        #endregion
        #region Operators
        public static Complex operator +(Complex firstAddend, Complex secondAddend)
        {
            return firstAddend.Add(secondAddend);
        }
        public static Complex operator -(Complex minuend, Complex subtrahend)
        {
            return minuend.Subtract(subtrahend);
        }
        public static Complex operator *(Complex multiplicand, Complex multiplier)
        {
            return multiplicand.MultiplyBy(multiplier);
        }
        public static Complex operator/(Complex dividend, Complex divisor)
        {
            return dividend.DivideBy(divisor);
        }
        public static bool operator ==(Complex left, Complex right)
        {
            return left.IsEqualTo(right);
        }
        public static bool operator !=(Complex left, Complex right)
        {
            return !left.IsEqualTo(right);
        }
        #endregion
        #region Object overrides
        public override bool Equals(object obj)
        {
            if (obj is Complex complex)
            {
                return IsEqualTo(complex);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return (Real, Imaginary).GetHashCode();
        }
        public override string ToString()
        {
            var sign = Imaginary < 0 
                ? "-" 
                : "+";
            var imaginaryAbs = Math.Abs(Imaginary);

            return $"({Real} {sign} {imaginaryAbs}i)";
        }
        #endregion
    }
}