using System;
using Avalonia;
using Avalonia.Media;

namespace Lab4.Figures;

[ShapeText("Куб", "Малює куб", "icons/cube.png")]
public class CubeShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
    private readonly RectangleShape _backFace = new(coordinates, temporary, Colors.Transparent);
    private readonly RectangleShape _frontFace = new(coordinates, temporary, Colors.Transparent);

    private readonly LineShape _frontTopLeftEdge = new(coordinates, temporary);
    private readonly LineShape _frontTopRightEdge = new(coordinates, temporary);
    private readonly LineShape _frontBottomRightEdge = new(coordinates, temporary);
    private readonly LineShape _frontBottomLeftEdge = new(coordinates, temporary);

    public override bool Temporary
    {
        get => base.Temporary;
        set
        {
            base.Temporary = value;
            _backFace.Temporary = value;
            _frontFace.Temporary = value;
            _frontTopLeftEdge.Temporary = value;
            _frontTopRightEdge.Temporary = value;
            _frontBottomRightEdge.Temporary = value;
            _frontBottomLeftEdge.Temporary = value;
        }
    }

    public override void Draw(DrawingContext context)
    {
        var (start, end) = Coordinates;

        var leftEdge = Math.Min(start.X, end.X);
        var topEdge = Math.Min(start.Y, end.Y);
        var boxWidth = Math.Abs(start.X - end.X);
        var boxHeight = Math.Abs(start.Y - end.Y);
        var depthOffset = Math.Min(boxWidth, boxHeight) * 0.35;

        var frontTopLeft = new Point(leftEdge, topEdge);
        var frontTopRight = new Point(leftEdge + boxWidth, topEdge);
        var frontBottomRight = new Point(leftEdge + boxWidth, topEdge + boxHeight);
        var frontBottomLeft = new Point(leftEdge, topEdge + boxHeight);

        var backTopLeft = new Point(leftEdge + depthOffset, topEdge - depthOffset);
        var backTopRight = new Point(leftEdge + boxWidth + depthOffset, topEdge - depthOffset);
        var backBottomRight = new Point(leftEdge + boxWidth + depthOffset, topEdge + boxHeight - depthOffset);
        var backBottomLeft = new Point(leftEdge + depthOffset, topEdge + boxHeight - depthOffset);

        _backFace.Coordinates = (backTopLeft, backBottomRight);
        _frontFace.Coordinates = (frontTopLeft, frontBottomRight);

        _frontTopLeftEdge.Coordinates = (frontTopLeft, backTopLeft);
        _frontTopRightEdge.Coordinates = (frontTopRight, backTopRight);
        _frontBottomRightEdge.Coordinates = (frontBottomRight, backBottomRight);
        _frontBottomLeftEdge.Coordinates = (frontBottomLeft, backBottomLeft);

        _backFace.Draw(context);

        _frontTopLeftEdge.Draw(context);
        _frontTopRightEdge.Draw(context);
        _frontBottomRightEdge.Draw(context);
        _frontBottomLeftEdge.Draw(context);

        _frontFace.Draw(context);
    }
}
