using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();

        Console.Write("Nhập số lượng hình: ");
        int shapeCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < shapeCount; i++)
        {
            Console.WriteLine("\nChọn loại hình (1: Circle, 2: Square, 3: Triangle, 4: Sphere, 5: Cube, 6: Tetrahedron): ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Nhập bán kính: ");
                    double radius = double.Parse(Console.ReadLine());
                    shapes.Add(new Circle(radius));
                    break;
                case 2:
                    Console.Write("Nhập cạnh: ");
                    double sideSquare = double.Parse(Console.ReadLine());
                    shapes.Add(new Square(sideSquare));
                    break;
                case 3:
                    Console.Write("Nhập đáy: ");
                    double baseLength = double.Parse(Console.ReadLine());
                    Console.Write("Nhập chiều cao: ");
                    double height = double.Parse(Console.ReadLine());
                    shapes.Add(new Triangle(baseLength, height));
                    break;
                case 4:
                    Console.Write("Nhập bán kính: ");
                    double radiusSphere = double.Parse(Console.ReadLine());
                    shapes.Add(new Sphere(radiusSphere));
                    break;
                case 5:
                    Console.Write("Nhập cạnh: ");
                    double sideCube = double.Parse(Console.ReadLine());
                    shapes.Add(new Cube(sideCube));
                    break;
                case 6:
                    Console.Write("Nhập cạnh: ");
                    double sideTetrahedron = double.Parse(Console.ReadLine());
                    shapes.Add(new Tetrahedron(sideTetrahedron));
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    i--;
                    break;
            }
        }

        foreach (var shape in shapes)
        {
            Console.WriteLine($"\nLoại hình: {shape.GetType().Name}");
            Console.WriteLine($"Diện tích: {shape.GetArea()}");

            if (shape is ThreeDimensionalShape)
            {
                Console.WriteLine($"Thể tích: {shape.GetVolume()}");
            }
        }

        var shapeCounts = new Dictionary<string, int>();
        foreach (var shape in shapes)
        {
            string typeName = shape.GetType().Name;
            if (shapeCounts.ContainsKey(typeName))
                shapeCounts[typeName]++;
            else
                shapeCounts[typeName] = 1;
        }

        Console.WriteLine("\nThống kê số lượng từng loại hình:");
        foreach (var entry in shapeCounts)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
}