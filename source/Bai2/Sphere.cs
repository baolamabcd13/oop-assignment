using System;

public class Sphere : ThreeDimensionalShape
{
    public double Radius { get; set; }

    public Sphere(double radius)
    {
        Radius = radius;
    }

    public override double GetArea()
    {
        return 4 * Math.PI * Radius * Radius;
    }

    public override double GetVolume()
    {
        return (4.0 / 3) * Math.PI * Math.Pow(Radius, 3);
    }
}