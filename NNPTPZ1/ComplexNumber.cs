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

            public double GetAbsoluteValue()
            {
                return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
            }

            public double GetAngleInRadians()
            {
                return Math.Atan(ImaginaryPart / RealPart);
            }

            public ComplexNumber Add(ComplexNumber otherAddend)
            {
                ComplexNumber addend = this;
                return new ComplexNumber()
                {
                    RealPart = addend.RealPart + otherAddend.RealPart,
                    ImaginaryPart = addend.ImaginaryPart + otherAddend.ImaginaryPart
                };
            }

            public ComplexNumber Subtract(ComplexNumber subtrahend)
            {
                ComplexNumber minuend = this;
                return new ComplexNumber()
                {
                    RealPart = minuend.RealPart - subtrahend.RealPart,
                    ImaginaryPart = minuend.ImaginaryPart - subtrahend.ImaginaryPart
                };
            }

            public ComplexNumber Multiply(ComplexNumber multiplier)
            {
                ComplexNumber multiplicand = this;
                return new ComplexNumber()
                {
                    RealPart = multiplicand.RealPart * multiplier.RealPart - multiplicand.ImaginaryPart * multiplier.ImaginaryPart,
                    ImaginaryPart = (float)(multiplicand.RealPart * multiplier.ImaginaryPart + multiplicand.ImaginaryPart * multiplier.RealPart)
                };
            }

            public ComplexNumber Divide(ComplexNumber divisor)
            {
                ComplexNumber numerator = this.Multiply(new ComplexNumber() { RealPart = divisor.RealPart, ImaginaryPart = -divisor.ImaginaryPart });
                double denominator = divisor.RealPart * divisor.RealPart + divisor.ImaginaryPart * divisor.ImaginaryPart;

                return new ComplexNumber()
                {
                    RealPart = numerator.RealPart / denominator,
                    ImaginaryPart = numerator.ImaginaryPart / denominator
                };
            }

            public override bool Equals(object obj)
            {
                if (obj is ComplexNumber)
                {
                    ComplexNumber complexNumber = obj as ComplexNumber;
                    return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
                }
                return base.Equals(obj);
            }

            public override string ToString()
            {
                return $"({RealPart} + {ImaginaryPart}i)";
            }
          
        }
    }
}
