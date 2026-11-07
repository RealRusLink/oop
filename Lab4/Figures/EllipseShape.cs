using System;
using Avalonia;
using Avalonia.Media;

namespace Lab4.Figures;

[ShapeText("Елліпс", "Малює елліпс", "icons/ellipse.png")]
public class EllipseShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    
    private static readonly IBrush FillBrush = Brushes.White;
    
    public override void Draw(DrawingContext context)
    {
        var bounds = GetBounds();

        context.DrawEllipse(Temporary ? null : FillBrush, Temporary ? TemporaryPen : FinalPen, bounds.Center, bounds.Width / 2, bounds.Height / 2);
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
