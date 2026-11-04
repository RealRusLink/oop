using Avalonia;
using Avalonia.Media;

namespace Lab3.Figures;

public class LineShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    public override void Draw(DrawingContext context)
    {
        context.DrawLine(Temporary ? TemporaryPen : FinalPen, Coordinates.First, Coordinates.Second);
    }
}
