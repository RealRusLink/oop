using Avalonia;

namespace Lab5.Figures;

public static class ShapeExtensions
{
    extension(Shape shape)
    {
        public string Name => ShapeCatalog.Get(shape.GetType()).ToolName;

        public string StartPoint => FormatPoint(shape.Coordinates.First);

        public string EndPoint => FormatPoint(shape.Coordinates.Second);
    }

    private static string FormatPoint(Point point) => $"({point.X:0.##}; {point.Y:0.##})";
}
