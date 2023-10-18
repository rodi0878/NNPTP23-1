using System;

namespace Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public float ImaginariPart { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ComplexNumber x && x.RealPart == RealPart && x.ImaginariPart == ImaginariPart;
        }

        public readonly static ComplexNumber Zero = new ComplexNumber { RealPart = 0, ImaginariPart = 0 };

        public ComplexNumber Multiply(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart * b.RealPart - ImaginariPart * b.ImaginariPart,
                ImaginariPart = (float)(RealPart * b.ImaginariPart + ImaginariPart * b.RealPart)
            };
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginariPart * ImaginariPart);
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart + b.RealPart,
                ImaginariPart = ImaginariPart + b.ImaginariPart
            };
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(ImaginariPart / RealPart);
        }
        public ComplexNumber Subtract(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart - b.RealPart,
                ImaginariPart = ImaginariPart - b.ImaginariPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginariPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                RealPart = (RealPart * b.RealPart + ImaginariPart * b.ImaginariPart) / (b.RealPart * b.RealPart + b.ImaginariPart * b.ImaginariPart),
                ImaginariPart = (float)((ImaginariPart * b.RealPart - RealPart * b.ImaginariPart) / (b.RealPart * b.RealPart + b.ImaginariPart * b.ImaginariPart))
            };
        }
    }
}
