using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

[ShapeText("Лінія", "Малює лінію", "icons/line.png")]
public class LineShape((Point first, Point second) coordinates, ShapeState state = ShapeState.Temporary)
    : Shape(coordinates, state)
{
    protected override void DrawWith(DrawingContext context, ShapeStyle style)
    {
        context.DrawLine(style.Pen, Coordinates.First, Coordinates.Second);
    }
}
