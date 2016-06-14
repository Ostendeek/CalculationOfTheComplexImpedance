using System.Collections.Generic;
using System.Linq;

namespace CalculationOfTheComplexImpedance
{
    static class UnitTools
    {
        private static Dictionary<Types, string> _units = new Dictionary<Types, string>()
        {
            {Types.Resistor, "Ohm"},
            {Types.Capacitor, "F"},
            {Types.Inductor, "H"}
        };
        private static Dictionary<Preambula, string> _preambula = new Dictionary<Preambula, string>()
        {
            {Preambula.piko, "p"},
            {Preambula.nano, "n"},
            {Preambula.normal, ""},
            {Preambula.mikro, "mk"},
            {Preambula.milly, "m"},    
            {Preambula.kilo, "k"},
            {Preambula.Mega, "M"},
            {Preambula.Giga, "G"},
            {Preambula.Tera, "T"}
        };

       

        public static string GetUnitsString(Types type, Preambula preambula)
        {
            string str = _preambula[preambula] + _units[type];
            return str;
        }

        public static Preambula GetPreambula(string str, Types type)
        {
           string preambula = str.Replace(_units[type], string.Empty);
           var myValue = _preambula.FirstOrDefault(x => x.Value == preambula).Key;
           return myValue;
        }
    }
}
