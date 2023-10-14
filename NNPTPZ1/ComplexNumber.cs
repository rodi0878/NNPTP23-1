using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public float ImaginaryPart { get; set; }

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
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart + addend.RealPart,
                ImaginaryPart = a.ImaginaryPart + addend.ImaginaryPart
            };
        }
        
        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart - subtrahend.RealPart,
                ImaginaryPart = a.ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber factor)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart * factor.RealPart - a.ImaginaryPart * factor.ImaginaryPart,
                ImaginaryPart = (float)(a.RealPart * factor.ImaginaryPart + a.ImaginaryPart * factor.RealPart)
            };
        }
        
        internal ComplexNumber Divide(ComplexNumber divisor)
        {
            var tmp = this.Multiply(new ComplexNumber() { RealPart = divisor.RealPart, ImaginaryPart = -divisor.ImaginaryPart });
            var tmp2 = divisor.RealPart * divisor.RealPart + divisor.ImaginaryPart * divisor.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = tmp.RealPart / tmp2,
                ImaginaryPart = (float)(tmp.ImaginaryPart / tmp2)
            };
        }
        
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.RealPart == RealPart && x.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
    }
}