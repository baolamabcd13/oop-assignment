using System;

public class Tetrahedron : ThreeDimensionalShape
{
    public double Side { get; set; }

    public Tetrahedron(double side)
    {
        Side = side;
    }

    public override double GetArea()
    {
        return Math.Sqrt(3) * Side * Side;
    }

    public override double GetVolume()
    {
        return Math.Pow(Side, 3) / (6 * Math.Sqrt(2));
    }
}