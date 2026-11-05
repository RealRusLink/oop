using System;
using Avalonia;
using Avalonia.Media;

namespace Lab4.Figures;

[ShapeText("Куб", "Малює куб")]
public class CubeShape((Point first, Point second) coordinates, bool temporary = true)
    : Shape(coordinates, temporary)
{
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

        var linePen = Temporary ? TemporaryPen : FinalPen;

        context.DrawLine(linePen, frontTopLeft, frontTopRight);
        context.DrawLine(linePen, frontTopRight, frontBottomRight);
        context.DrawLine(linePen, frontBottomRight, frontBottomLeft);
        context.DrawLine(linePen, frontBottomLeft, frontTopLeft);

        context.DrawLine(linePen, backTopLeft, backTopRight);
        context.DrawLine(linePen, backTopRight, backBottomRight);
        context.DrawLine(linePen, backBottomRight, backBottomLeft);
        context.DrawLine(linePen, backBottomLeft, backTopLeft);

        context.DrawLine(linePen, frontTopLeft, backTopLeft);
        context.DrawLine(linePen, frontTopRight, backTopRight);
        context.DrawLine(linePen, frontBottomRight, backBottomRight);
        context.DrawLine(linePen, frontBottomLeft, backBottomLeft);
    }
}
