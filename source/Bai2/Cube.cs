public class Cube : ThreeDimensionalShape
{
    public double Side { get; set; }

    public Cube(double side)
    {
        Side = side;
    }

    public override double GetArea()
    {
        return 6 * Side * Side;
    }

    public override double GetVolume()
    {
        return Math.Pow(Side, 3);
    }
}