using System;
using System.Numerics;

namespace CalculationOfTheComplexImpedance
{   
    class Inductor : IElement
    {
        public Connection Connection
        {
            get;
            private set;
        }
        private double Nominal { get; set; }
        public Inductor(double nominal, Connection connection)
        {
            Connection = connection;
            Nominal = nominal;
        }
            
        

        public Complex CalculateZ(double frequencies)
        {   
           return new Complex(0, 2 * Math.PI * frequencies * Nominal);
        }
    }
}
