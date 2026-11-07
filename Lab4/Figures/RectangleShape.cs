using System;
using Avalonia;
using Avalonia.Media;

namespace Lab4.Figures;

[ShapeText("Прямокутник", "Малює прямокутник", "icons/rectangle.png")]
public class RectangleShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    private IBrush _fillBrush = Brushes.Orange;

    public RectangleShape((Point first, Point second) coordinates, bool temporary, Color fillColor)
        : this(coordinates, temporary)
    {
        _fillBrush = new SolidColorBrush(fillColor);
    }

    protected virtual IBrush FillBrush => _fillBrush;

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
