using Avalonia;
using Avalonia.Media;

namespace Lab2.Figures;

public class PointShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    private const double Radius = 3;

    public override void Draw(DrawingContext context)
    {
        context.DrawEllipse(FinalBrush, null, Coordinates.First, Radius, Radius);
    }
}
