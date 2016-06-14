using System.Numerics;

namespace CalculationOfTheComplexImpedance
{
    public interface IElement
    {
        Connection Connection{get;}
        Complex CalculateZ(double frequencies);
    }

}
