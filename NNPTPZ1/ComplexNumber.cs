using System;

namespace NNPTPZ1
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public float Imaginary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.Real == Real && x.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public ComplexNumber Multiply(ComplexNumber complexNumber)
        {
            ComplexNumber dividedNumber = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                Real = dividedNumber.Real * complexNumber.Real - dividedNumber.Imaginary * complexNumber.Imaginary,
                Imaginary = (float)(dividedNumber.Real * complexNumber.Imaginary + dividedNumber.Imaginary * complexNumber.Real)
            };
        }
        public double GetAbS()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public ComplexNumber Add(ComplexNumber complexNumber)
        {
            ComplexNumber dividedNumber = this;
            return new ComplexNumber()
            {
                Real = dividedNumber.Real + complexNumber.Real,
                Imaginary = dividedNumber.Imaginary + complexNumber.Imaginary
            };
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(Imaginary / Real);
        }
        public ComplexNumber Subtract(ComplexNumber complexNumber)
        {
            ComplexNumber dividedNumber = this;
            return new ComplexNumber()
            {
                Real = dividedNumber.Real - complexNumber.Real,
                Imaginary = dividedNumber.Imaginary - complexNumber.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        internal ComplexNumber Divide(ComplexNumber complexNumber)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var dividend = this.Multiply(new ComplexNumber() { Real = complexNumber.Real, Imaginary = -complexNumber.Imaginary });
            var devider = complexNumber.Real * complexNumber.Real + complexNumber.Imaginary * complexNumber.Imaginary;

            return new ComplexNumber()
            {
                Real = dividend.Real / devider,
                Imaginary = (float)(dividend.Imaginary / devider)
            };
        }
    }
}
