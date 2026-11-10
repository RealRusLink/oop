using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

[ShapeText("Куб", "Малює куб", "icons/cube.png")]
public class CubeShape((Point first, Point second) coordinates, ShapeState state = ShapeState.Temporary)
    : Shape(coordinates, state)
{
    private readonly RectangleShape _backFace = new(coordinates, state, Colors.Transparent);
    private readonly RectangleShape _frontFace = new(coordinates, state, Colors.Transparent);

    private readonly LineShape _frontTopLeftEdge = new(coordinates, state);
    private readonly LineShape _frontTopRightEdge = new(coordinates, state);
    private readonly LineShape _frontBottomRightEdge = new(coordinates, state);
    private readonly LineShape _frontBottomLeftEdge = new(coordinates, state);

    protected override void DrawWith(DrawingContext context, ShapeStyle style)
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

        foreach (var part in Parts())
        {
            part.State = style.State;
            part.Show(context);
        }
    }

    private IEnumerable<Shape> Parts()
    {
        yield return _backFace;

        yield return _frontTopLeftEdge;
        yield return _frontTopRightEdge;
        yield return _frontBottomRightEdge;
        yield return _frontBottomLeftEdge;

        yield return _frontFace;
    }
}
