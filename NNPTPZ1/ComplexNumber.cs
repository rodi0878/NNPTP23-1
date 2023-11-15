using System;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class ComplexNumber
        {
            public double RealPart { get; set; }
            public float ImaginaryPart { get; set; }

            public override bool Equals(object comparedObject)
            {
                if (comparedObject is ComplexNumber)
                {
                    ComplexNumber comparedComplexNumber = comparedObject as ComplexNumber;
                    return comparedComplexNumber.RealPart == RealPart && comparedComplexNumber.ImaginaryPart == ImaginaryPart;
                }
                return base.Equals(comparedObject);
            }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                RealPart = 0,
                ImaginaryPart = 0
            };

            public ComplexNumber Multiply(ComplexNumber multiplier)
            {
                ComplexNumber multiplicand = this;
                return new ComplexNumber()
                {
                    RealPart = multiplicand.RealPart * multiplier.RealPart - multiplicand.ImaginaryPart * multiplier.ImaginaryPart,
                    ImaginaryPart = (float)(multiplicand.RealPart * multiplier.ImaginaryPart + multiplicand.ImaginaryPart * multiplier.RealPart)
                };
            }

            public ComplexNumber Add(ComplexNumber addend)
            {
                ComplexNumber augend = this;
                return new ComplexNumber()
                {
                    RealPart = augend.RealPart + addend.RealPart,
                    ImaginaryPart = augend.ImaginaryPart + addend.ImaginaryPart
                };
            }

            public ComplexNumber Divide(ComplexNumber Divisor)
            {
                var numerator = Multiply(new ComplexNumber() { RealPart = Divisor.RealPart, ImaginaryPart = -Divisor.ImaginaryPart });
                var denominator = Divisor.RealPart * Divisor.RealPart + Divisor.ImaginaryPart * Divisor.ImaginaryPart;

                return new ComplexNumber()
                {
                    RealPart = numerator.RealPart / denominator,
                    ImaginaryPart = (float)(numerator.ImaginaryPart / denominator)
                };
            }

            public double GetAngleInDegrees()
            {
                return Math.Atan(ImaginaryPart / RealPart) * 180 / Math.PI;
            }
            public ComplexNumber Subtract(ComplexNumber Subtrahend)
            {
                ComplexNumber Minuend = this;
                return new ComplexNumber()
                {
                    RealPart = Minuend.RealPart - Subtrahend.RealPart,
                    ImaginaryPart = Minuend.ImaginaryPart - Subtrahend.ImaginaryPart
                };
            }


            public double GetAbsoluteValue()
            {
                return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
            }

            public override string ToString()
            {
                return $"({RealPart} + {ImaginaryPart}i)";
            }

            public override int GetHashCode()
            {
                int hashCode = 1382181547;
                hashCode = hashCode * -1521134295 + RealPart.GetHashCode();
                hashCode = hashCode * -1521134295 + ImaginaryPart.GetHashCode();
                return hashCode;
            }
        }
    }
}
