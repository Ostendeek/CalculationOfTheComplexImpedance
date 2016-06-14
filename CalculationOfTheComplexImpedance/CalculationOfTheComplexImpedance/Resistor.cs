using System.Numerics;

namespace CalculationOfTheComplexImpedance
{
    
    class Resistor : IElement
    {
        public Connection Connection { get; private set; }
        private double Nominal { get; set; }
        public Resistor(double nominal, Connection connection)
        {
            Connection = connection;
            Nominal = nominal;
        }

        

        public Complex CalculateZ(double frequencies)
        {
          return new Complex(Nominal, 0);
        }
    }
}
