using System;
using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

[ShapeText("Елліпс", "Малює елліпс", "icons/ellipse.png")]
public class EllipseShape((Point first, Point second) coordinates, ShapeState state = ShapeState.Temporary)
    : Shape(coordinates, state)
{
    private static readonly IBrush EllipseFillBrush = Brushes.White;

    protected override IBrush? FillBrush => EllipseFillBrush;

    protected override void DrawWith(DrawingContext context, ShapeStyle style)
    {
        var bounds = GetBounds();

        context.DrawEllipse(style.Fill, style.Pen, bounds.Center, bounds.Width / 2, bounds.Height / 2);
    }

    private Rect GetBounds()
    {
        var centerPoint = Coordinates.First;
        var secondPoint = Coordinates.Second;
        var oppositePoint = new Point(2 * centerPoint.X - secondPoint.X, 2 * centerPoint.Y - secondPoint.Y);

        var leftEdge = Math.Min(secondPoint.X, oppositePoint.X);
        var topEdge = Math.Min(secondPoint.Y, oppositePoint.Y);

        return new Rect(leftEdge, topEdge, Math.Abs(secondPoint.X - oppositePoint.X), Math.Abs(secondPoint.Y - oppositePoint.Y));
    }
}
