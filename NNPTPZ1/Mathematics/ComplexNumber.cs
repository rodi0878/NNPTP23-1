using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber complexNumber = obj as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public ComplexNumber Multiply(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart * b.RealPart - a.ImaginaryPart * b.ImaginaryPart,
                ImaginaryPart = a.RealPart * b.ImaginaryPart + a.ImaginaryPart * b.RealPart
            };
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart + b.RealPart,
                ImaginaryPart = a.ImaginaryPart + b.ImaginaryPart
            };
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public ComplexNumber Subtract(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart - b.RealPart,
                ImaginaryPart = a.ImaginaryPart - b.ImaginaryPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            ComplexNumber temporaryNumeratorsProduct = this.Multiply(new ComplexNumber() { RealPart = b.RealPart, ImaginaryPart = -b.ImaginaryPart });
            var temporaryDenominator = b.RealPart * b.RealPart + b.ImaginaryPart * b.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = temporaryNumeratorsProduct.RealPart / temporaryDenominator,
                ImaginaryPart = temporaryNumeratorsProduct.ImaginaryPart / temporaryDenominator
            };
        }
    }
}
