using System;



namespace NNPTPZ1.Mathematics
{
    public class Complex
    {
        public readonly static Complex Zero = new Complex()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public double RealPart { get; set; }
        public float ImaginaryPart { get; set; }

        public Complex Multiply(Complex multiplicand)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new Complex()
            {
                RealPart = this.RealPart * multiplicand.RealPart - this.ImaginaryPart * multiplicand.ImaginaryPart,
                ImaginaryPart = (float)(this.RealPart * multiplicand.ImaginaryPart + this.ImaginaryPart * multiplicand.RealPart)
            };
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt( RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public Complex Add(Complex addend)
        {
            return new Complex()
            {
                RealPart = this.RealPart + addend.RealPart,
                ImaginaryPart = this.ImaginaryPart + addend.ImaginaryPart
            };
        }
        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }
        public Complex Subtract(Complex subtrahend)
        {
            return new Complex()
            {
                RealPart = this.RealPart - subtrahend.RealPart,
                ImaginaryPart = this.ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        internal Complex Divide(Complex divideWith)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var divided = this.Multiply(new Complex() { RealPart = divideWith.RealPart, ImaginaryPart = -divideWith.ImaginaryPart });
            var divisor = divideWith.RealPart * divideWith.RealPart + divideWith.ImaginaryPart * divideWith.ImaginaryPart;

            return new Complex()
            {
                RealPart = divided.RealPart / divisor,
                ImaginaryPart = (float)(divided.ImaginaryPart / divisor)
            };
        }

        public override bool Equals(object comparedValue)
        {
            if (comparedValue is Complex)
            {
                Complex compared = comparedValue as Complex;
                return compared.RealPart == RealPart && compared.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(comparedValue);
        }

        public override int GetHashCode()
        {
            int hashCode = 1382181547;
            hashCode = hashCode * -1521134295 + RealPart.GetHashCode();
            hashCode = hashCode * -1521134295 + ImaginaryPart.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
    }
}
