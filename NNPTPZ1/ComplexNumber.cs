using System;

namespace Mathematics
{
    public class ComplexNumber
    {
        public double RealNumber { get; set; }
        public double ImaginaryUnit { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealNumber = 0,
            ImaginaryUnit = 0
        };

        public ComplexNumber Add(ComplexNumber c)
        {
            return new ComplexNumber()
            {
                RealNumber = RealNumber + c.RealNumber,
                ImaginaryUnit = ImaginaryUnit + c.ImaginaryUnit
            };
        }
        public ComplexNumber Subtract(ComplexNumber c)
        {
            return new ComplexNumber()
            {
                RealNumber = RealNumber - c.RealNumber,
                ImaginaryUnit = ImaginaryUnit - c.ImaginaryUnit
            };
        }

        public ComplexNumber Multiply(ComplexNumber c)
        {
            return new ComplexNumber()
            {
                RealNumber = RealNumber * c.RealNumber - ImaginaryUnit * c.ImaginaryUnit,
                ImaginaryUnit = RealNumber * c.ImaginaryUnit + ImaginaryUnit * c.RealNumber
            };
        }

        internal ComplexNumber Divide(ComplexNumber c)
        {
            ComplexNumber dividend = Multiply(new ComplexNumber() { RealNumber = c.RealNumber, ImaginaryUnit = -c.ImaginaryUnit });
            double denominator = c.RealNumber * c.RealNumber + c.ImaginaryUnit * c.ImaginaryUnit;

            return new ComplexNumber()
            {
                RealNumber = dividend.RealNumber / denominator,
                ImaginaryUnit = dividend.ImaginaryUnit / denominator
            };
        }

        public double GetAbsolute()
        {
            return Math.Sqrt(RealNumber * RealNumber + ImaginaryUnit * ImaginaryUnit);
        }

        public double GetAngleInDegrees()
        {
            return Math.Atan(ImaginaryUnit / RealNumber);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 1024;
                hash = hash * 2048 + RealNumber.GetHashCode();
                hash = hash * 2048 + ImaginaryUnit.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber complexNumber)
            {
                return complexNumber.RealNumber == RealNumber && complexNumber.ImaginaryUnit == ImaginaryUnit;
            }
            return false;
        }

        public override string ToString()
        {
            return $"({RealNumber} + {ImaginaryUnit}i)";
        }
    }
}