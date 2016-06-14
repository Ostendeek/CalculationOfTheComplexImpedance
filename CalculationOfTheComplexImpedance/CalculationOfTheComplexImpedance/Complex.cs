using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpo
{
    public class Complex
    {
        public Complex()
        {
            Real = 0;
            Img = 0;
        }
        public Complex(double real, double img)
        {
            Real = real;
            Img = img;
        }

        public double Real
        {
            get;
            set; 
        }
        public double Img
        {
            get;
            set; 
        }

        public override string ToString()
        {
            string result = Real.ToString();
            if (Img > 0) result += "+";
            result += Img.ToString();
            result += "j";
            return result;
        }

        static public Complex operator +(Complex One, Complex Two)
        {
            return new Complex(One.Real + Two.Real, One.Img + Two.Img);
        }

        static public Complex operator -(Complex One, Complex Two)
        {
            return new Complex(One.Real - Two.Real, One.Img - Two.Img);
        }
    }
}
