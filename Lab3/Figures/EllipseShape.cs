using System;
using Avalonia;
using Avalonia.Media;

namespace Lab3.Figures;

public class EllipseShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    public override void Draw(DrawingContext context)
    {
        var rect = GetBounds();

        context.DrawEllipse(null, Temporary ? TemporaryPen : FinalPen, rect.Center, rect.Width / 2, rect.Height / 2);
    }

    private Rect GetBounds()
    {
        var first = Coordinates.First;
        var second = Coordinates.Second;

        var left = Math.Min(first.X, second.X);
        var top = Math.Min(first.Y, second.Y);

        return new Rect(left, top, Math.Abs(first.X - second.X), Math.Abs(first.Y - second.Y));
    }
}
