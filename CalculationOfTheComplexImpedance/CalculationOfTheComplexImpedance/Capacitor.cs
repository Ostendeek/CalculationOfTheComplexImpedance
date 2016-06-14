using System;
using System.Numerics;

namespace CalculationOfTheComplexImpedance
{
    class Capacitor : IElement
    {
        public Connection Connection { get; private set; } 
        private double Nominal
        {
            get;
            set;
        }
        public Capacitor(double nominal, Connection connection)
        {
            Nominal = nominal;
            Connection = Connection;
        }

       
      
        public Complex CalculateZ(double frequencies)
        {           
            return new Complex(0, -1 / (2 * Math.PI * frequencies * Nominal));
        }
    }
}
