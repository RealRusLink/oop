using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia;
using Lab5.Figures;

namespace Lab5;

public static class ShapeCsv
{
    private const char Separator = ',';
    private const string Header = "Type,StartX,StartY,EndX,EndY";

    public static void Save(string path, IEnumerable<Shape> shapes)
    {
        var lines = new List<string> { Header };
        lines.AddRange(shapes.Select(FormatShape));

        File.WriteAllLines(path, lines);
    }

    public static List<Shape> Load(string path)
    {
        var shapes = new List<Shape>();

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line) || line == Header) continue;

            var shape = ParseShape(line);

            if (shape is not null)
            {
                shapes.Add(shape);
            }
        }

        return shapes;
    }

    private static string FormatShape(Shape shape) => string.Join(Separator,
        shape.GetType().Name,
        FormatNumber(shape.Coordinates.First.X),
        FormatNumber(shape.Coordinates.First.Y),
        FormatNumber(shape.Coordinates.Second.X),
        FormatNumber(shape.Coordinates.Second.Y));

    private static Shape? ParseShape(string line)
    {
        var columns = line.Split(Separator);

        if (columns.Length < 5) return null;

        var shapeType = ShapeCatalog.Attributes.Keys
            .FirstOrDefault(type => type.Name == columns[0].Trim());

        if (shapeType is null) return null;

        if (!TryParseNumber(columns[1], out var startX) ||
            !TryParseNumber(columns[2], out var startY) ||
            !TryParseNumber(columns[3], out var endX) ||
            !TryParseNumber(columns[4], out var endY))
        {
            return null;
        }

        var coordinates = (new Point(startX, startY), new Point(endX, endY));

        return ShapeCatalog.Create(shapeType, coordinates, ShapeState.Final);
    }

    private static string FormatNumber(double value) => value.ToString("R", CultureInfo.InvariantCulture);

    private static bool TryParseNumber(string value, out double result) =>
        double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
}
