namespace CalculationOfTheComplexImpedance
{
    enum Types
    {
        Resistor = 1,
        Capacitor,
        Inductor
    };

    public enum Connection
    {
        Series = 1,
        Parallel
    };
    enum Element
    {
         _resistorUnits,
         _capacitorUnits,
         _inductorUnits,
    };
    enum Preambula
    {
        piko = -12,
        nano = -9,
        mikro = -6,
        milly = -3, 
        normal = 0,
        kilo = 3,
        Mega = 6,
        Giga = 9,
        Tera = 12
    };

}

