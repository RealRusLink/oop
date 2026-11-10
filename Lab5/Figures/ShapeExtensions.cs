using Avalonia;

namespace Lab5.Figures;

public static class ShapeExtensions
{
    extension(Shape shape)
    {
        public string Name => shape.GetType().Name;

        public string StartPoint => FormatPoint(shape.Coordinates.First);

        public string EndPoint => FormatPoint(shape.Coordinates.Second);

        public string Status => shape.State switch
        {
            ShapeState.Final => "Завершено",
            ShapeState.Temporary => "В процесі",
            ShapeState.Highlighted => "Виділено",
            _ => string.Empty
        };
    }

    private static string FormatPoint(Point point) => $"({point.X:0.##}; {point.Y:0.##})";
}
