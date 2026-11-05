using System;
using Avalonia;
using Avalonia.Media;

namespace Lab4.Figures;

[ShapeText("Прямокутник", "Малює прямокутник")]
public class RectangleShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    private static readonly IBrush FillBrush = Brushes.Orange;

    public override void Draw(DrawingContext context)
    {
        var bounds = GetBounds();

        if (Temporary)
        {
            context.DrawRectangle(null, TemporaryPen, bounds);
            return;
        }

        context.DrawRectangle(FillBrush, FinalPen, bounds);
    }

    private Rect GetBounds()
    {
        var firstPoint = Coordinates.First;
        var secondPoint = Coordinates.Second;

        var leftEdge = Math.Min(firstPoint.X, secondPoint.X);
        var topEdge = Math.Min(firstPoint.Y, secondPoint.Y);

        return new Rect(leftEdge, topEdge, Math.Abs(firstPoint.X - secondPoint.X), Math.Abs(firstPoint.Y - secondPoint.Y));
    }
}
