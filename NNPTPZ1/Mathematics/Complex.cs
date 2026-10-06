namespace NNPTPZ1.Mathematics
{
    public class Complex
    {
        public static readonly Complex Zero = new Complex()
        {
            Real = 0,
            Imaginary = 0
        };

        public double Real { get; set; }
        public float Imaginary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Complex)
            {
                Complex other = obj as Complex;
                return other.Real == Real && other.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Real.GetHashCode() * 397) ^ Imaginary.GetHashCode();
            }
        }

        public Complex Multiply(Complex b)
        {
            Complex a = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new Complex()
            {
                Real = a.Real * b.Real - a.Imaginary * b.Imaginary,
                Imaginary = (float)(a.Real * b.Imaginary + a.Imaginary * b.Real)
            };
        }

        public Complex Add(Complex b)
        {
            Complex a = this;
            return new Complex()
            {
                Real = a.Real + b.Real,
                Imaginary = a.Imaginary + b.Imaginary
            };
        }

        public Complex Subtract(Complex b)
        {
            Complex a = this;
            return new Complex()
            {
                Real = a.Real - b.Real,
                Imaginary = a.Imaginary - b.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        internal Complex Divide(Complex b)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var numerator = this.Multiply(new Complex() { Real = b.Real, Imaginary = -b.Imaginary });
            var denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new Complex()
            {
                Real = numerator.Real / denominator,
                Imaginary = (float)(numerator.Imaginary / denominator)
            };
        }
    }
}
