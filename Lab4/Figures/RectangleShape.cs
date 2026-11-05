using System;
using Avalonia;
using Avalonia.Media;

namespace Lab4.Figures;

public class RectangleShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    private static readonly IBrush FillBrush = Brushes.Orange;

    public override void Draw(DrawingContext context)
    {
        var rect = GetBounds();

        if (Temporary)
        {
            context.DrawRectangle(null, TemporaryPen, rect);
            return;
        }

        context.DrawRectangle(FillBrush, FinalPen, rect);
    }

    private Rect GetBounds()
    {
        var corner = Coordinates.First;
        var opposite = Coordinates.Second;

        var left = Math.Min(corner.X, opposite.X);
        var top = Math.Min(corner.Y, opposite.Y);

        return new Rect(left, top, Math.Abs(corner.X - opposite.X), Math.Abs(corner.Y - opposite.Y));
    }
}
