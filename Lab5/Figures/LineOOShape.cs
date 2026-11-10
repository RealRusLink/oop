using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

[ShapeText("О-О Лінія", "Малює О-О лінію", "icons/lineoo.png")]
public class LineOOShape((Point start, Point end) coordinates, ShapeState state = ShapeState.Temporary)
    : Shape(coordinates, state)
{
    private readonly LineShape _line = new(coordinates, state);
    private readonly EllipseShape _startEllipse = new(CreateEllipseCoordinates(coordinates.start), state);
    private readonly EllipseShape _endEllipse = new(CreateEllipseCoordinates(coordinates.end), state);

    private static (Point First, Point Second) CreateEllipseCoordinates(Point point) =>
        (point, new Point(point.X + 10, point.Y + 10));

    protected override void DrawWith(DrawingContext context, ShapeStyle style)
    {
        _line.Coordinates = Coordinates;
        _startEllipse.Coordinates = CreateEllipseCoordinates(Coordinates.First);
        _endEllipse.Coordinates = CreateEllipseCoordinates(Coordinates.Second);

        _line.State = style.State;
        _startEllipse.State = style.State;
        _endEllipse.State = style.State;

        _line.Show(context);
        _startEllipse.Show(context);
        _endEllipse.Show(context);
    }
}
