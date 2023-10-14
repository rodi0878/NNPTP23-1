using System;

namespace NNPTPZ1
{
    namespace Mathematics
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
            public ComplexNumber Multiply(ComplexNumber otherComplexNumber)
            {
                ComplexNumber complexNumber = this;
                return new ComplexNumber()
                {
                    RealPart = complexNumber.RealPart * otherComplexNumber.RealPart - complexNumber.ImaginaryPart * otherComplexNumber.ImaginaryPart,
                    ImaginaryPart = (complexNumber.RealPart * otherComplexNumber.ImaginaryPart + complexNumber.ImaginaryPart * otherComplexNumber.RealPart)
                };
            }
            public ComplexNumber Divide(ComplexNumber other)
            {
                ComplexNumber product = Multiply(new ComplexNumber() { RealPart = other.RealPart, ImaginaryPart = -other.ImaginaryPart });
                double denominator = other.RealPart * other.RealPart + other.ImaginaryPart * other.ImaginaryPart;

                return new ComplexNumber()
                {
                    RealPart = product.RealPart / denominator,
                    ImaginaryPart = product.ImaginaryPart / denominator
                };
            }
            public ComplexNumber Add(ComplexNumber otherComplexNumber)
            {
                ComplexNumber complexNumber = this;
                return new ComplexNumber()
                {
                    RealPart = complexNumber.RealPart + otherComplexNumber.RealPart,
                    ImaginaryPart = complexNumber.ImaginaryPart + otherComplexNumber.ImaginaryPart
                };
            }
            public ComplexNumber Subtract(ComplexNumber otherComplexNumber)
            {
                ComplexNumber complexNumber = this;
                return new ComplexNumber()
                {
                    RealPart = complexNumber.RealPart - otherComplexNumber.RealPart,
                    ImaginaryPart = complexNumber.ImaginaryPart - otherComplexNumber.ImaginaryPart
                };
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
        }
    }
}