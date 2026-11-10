using System;
using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

[ShapeText("Прямокутник", "Малює прямокутник", "icons/rectangle.png")]
public class RectangleShape((Point first, Point second) coordinates, ShapeState state = ShapeState.Temporary)
    : Shape(coordinates, state)
{
    private IBrush _fillBrush = Brushes.Orange;

    public RectangleShape((Point first, Point second) coordinates, ShapeState state, Color fillColor)
        : this(coordinates, state)
    {
        _fillBrush = new SolidColorBrush(fillColor);
    }

    protected override IBrush? FillBrush => _fillBrush;

    protected override void DrawWith(DrawingContext context, ShapeStyle style)
    {
        context.DrawRectangle(style.Fill, style.Pen, GetBounds());
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
