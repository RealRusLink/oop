using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

[ShapeText("О-О Лінія", "Малює О-О лінію")]
public class LineOOShape((Point start, Point end) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    private readonly LineShape _line = new(coordinates, temporary);
    private readonly EllipseShape _startEllipse = new(CreateEllipseCoordinates(coordinates.start), temporary);
    private readonly EllipseShape _endEllipse = new(CreateEllipseCoordinates(coordinates.end), temporary);

    private static (Point First, Point Second) CreateEllipseCoordinates(Point point) =>
        (point, new Point(point.X + 10, point.Y + 10));

    public override bool Temporary
    {
        get;
        set
        {
            field = value;
            _line.Temporary = value;
            _startEllipse.Temporary = value;
            _endEllipse.Temporary = value;
        }
    }

    public override void Draw(DrawingContext context)
    {
        _line.Coordinates = Coordinates;
        _startEllipse.Coordinates = CreateEllipseCoordinates(Coordinates.First);
        _endEllipse.Coordinates = CreateEllipseCoordinates(Coordinates.Second);

        _line.Draw(context);
        _startEllipse.Draw(context);
        _endEllipse.Draw(context);
    }
}
