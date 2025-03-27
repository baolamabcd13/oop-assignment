public class Triangle : TwoDimensionalShape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public Triangle(double baseLength, double height)
    {
        Base = baseLength;
        Height = height;
    }

    public override double GetArea()
    {
        return 0.5 * Base * Height;
    }
}