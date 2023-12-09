using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber complexNumber = obj as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }
        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }
        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
        public ComplexNumber Add(ComplexNumber secondComplexNumber)
        {
            ComplexNumber firstComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = firstComplexNumber.RealPart + secondComplexNumber.RealPart,
                ImaginaryPart = firstComplexNumber.ImaginaryPart + secondComplexNumber.ImaginaryPart
            };
        }
        public ComplexNumber Subtract(ComplexNumber secondComplexNumber)
        {
            ComplexNumber firstComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = firstComplexNumber.RealPart - secondComplexNumber.RealPart,
                ImaginaryPart = firstComplexNumber.ImaginaryPart - secondComplexNumber.ImaginaryPart
            };
        }
        public ComplexNumber Multiply(ComplexNumber secondComplexNumber)
        {
            ComplexNumber firstComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = firstComplexNumber.RealPart * secondComplexNumber.RealPart - firstComplexNumber.ImaginaryPart * secondComplexNumber.ImaginaryPart,
                ImaginaryPart = firstComplexNumber.RealPart * secondComplexNumber.ImaginaryPart + firstComplexNumber.ImaginaryPart * secondComplexNumber.RealPart
            };
        }
        public ComplexNumber Divide(ComplexNumber secondComplexNumber)
        {
            ComplexNumber temporaryNumerator = this.Multiply(new ComplexNumber() { RealPart = secondComplexNumber.RealPart, ImaginaryPart = -secondComplexNumber.ImaginaryPart });
            double temporaryDenominator = secondComplexNumber.RealPart * secondComplexNumber.RealPart + secondComplexNumber.ImaginaryPart * secondComplexNumber.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = temporaryNumerator.RealPart / temporaryDenominator,
                ImaginaryPart = temporaryNumerator.ImaginaryPart / temporaryDenominator
            };
        }
    }
}