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
        
        public double GetAbS()
        {
            return Math.Sqrt( RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }
        
        public double GetAngleInDegrees()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }
        
        public ComplexNumber Add(ComplexNumber addend)
        {
            ComplexNumber currentComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = currentComplexNumber.RealPart + addend.RealPart,
                ImaginaryPart = currentComplexNumber.ImaginaryPart + addend.ImaginaryPart
            };
        }
        
        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            ComplexNumber currentComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = currentComplexNumber.RealPart - subtrahend.RealPart,
                ImaginaryPart = currentComplexNumber.ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber factor)
        {
            ComplexNumber currentComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = currentComplexNumber.RealPart 
                    * factor.RealPart 
                    - currentComplexNumber.ImaginaryPart 
                    * factor.ImaginaryPart,
                
                ImaginaryPart = currentComplexNumber.RealPart 
                    * factor.ImaginaryPart 
                    + currentComplexNumber.ImaginaryPart 
                    * factor.RealPart
            };
        }
        
        internal ComplexNumber Divide(ComplexNumber divisor)
        {
            var multipliedWithDivisor = this.Multiply(new ComplexNumber() { RealPart = divisor.RealPart, ImaginaryPart = -divisor.ImaginaryPart });
            var divisorModulusSquared = divisor.RealPart * divisor.RealPart + divisor.ImaginaryPart * divisor.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = multipliedWithDivisor.RealPart / divisorModulusSquared,
                ImaginaryPart = multipliedWithDivisor.ImaginaryPart / divisorModulusSquared
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