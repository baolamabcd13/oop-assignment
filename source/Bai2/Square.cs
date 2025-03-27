public class Square : TwoDimensionalShape
{
    public double Side { get; set; }

    public Square(double side)
    {
        Side = side;
    }

    public override double GetArea()
    {
        return Side * Side;
    }
}