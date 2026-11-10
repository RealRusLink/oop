using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

[ShapeText("Крапка", "Малює крапку", "icons/point.png")]
public class PointShape((Point first, Point second) coordinates, ShapeState state = ShapeState.Temporary)
    : Shape(coordinates, state)
{
    private const double DotRadius = 3;
    private const double HighlightedDotRadius = 6;

    protected override IBrush? FillBrush => FinalBrush;

    protected override void DrawWith(DrawingContext context, ShapeStyle style)
    {
        var radius = style.State == ShapeState.Highlighted ? HighlightedDotRadius : DotRadius;
        var fill = style.Fill ?? style.Pen.Brush;

        context.DrawEllipse(fill, null, Coordinates.First, radius, radius);
    }
}
