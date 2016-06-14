using System.Collections.Generic;
using System.Numerics;

namespace CalculationOfTheComplexImpedance
{
    class Circuit
    {
        public Circuit()
        {
            elements = new List<IElement>();
        }

        private List<IElement> elements;

        private List<double> frequencies = new List<double>();
        
        public List<IElement> Elements
        {
            get { return elements; }
            set { elements = value; }
        }

        public List<double> Frequencies
        {
            get { return frequencies; }
            set { frequencies = value; }
        }
        public List<Complex> CalculateZ()
        {
            List<Complex> result = new List<Complex>();
            for (int i = 0; i < frequencies.Count; i++ )
            {
                Complex complex = CalculateZ(frequencies[i]);
                result.Add(complex);
            }
            return result;
        }
        
        public Complex CalculateZ(double frequency)
        {
            Complex result = new Complex();
            Complex tmpresult = new Complex();
            Complex tmpelement = new Complex();
            //foreach (IElement element in Elements)
            for (int i = elements.Count-1; i >= 0; i--)
            {
                if (elements[i].Connection == Connection.Series)
                {
                    tmpresult = elements[i].CalculateZ(frequency);
                    result += tmpresult;
             //       result += elements[i].CalculateZ(frequency);
                }
                else
                {

                    tmpelement = elements[i].CalculateZ(frequency);              
                    tmpresult = ((tmpelement * elements[i-1].CalculateZ(frequency)) /
                        (elements[i - 1].CalculateZ(frequency) + tmpelement));
                    
                    result += tmpresult;
                   // tmpelement = tmpresult;
                }
               // tmpelement = tmpelement - 1;
                //result += element.CalculateZ(frequency);
            }
            return result;
        }
    }
}
