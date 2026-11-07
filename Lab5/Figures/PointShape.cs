using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

[ShapeText("Крапка", "Малює крапку")]
public class PointShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    private const double DotRadius = 3;

    public override void Draw(DrawingContext context)
    {
        context.DrawEllipse(FinalBrush, null, Coordinates.First, DotRadius, DotRadius);
    }
}
