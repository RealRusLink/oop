using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

public abstract class Shape((Point first, Point second) initialCoordinates, bool isTemporary = true)
{
    protected static readonly IPen FinalPen = new Pen(Brushes.Black, 2);
    protected static readonly IPen TemporaryPen = new Pen(Brushes.Black, 2, DashStyle.DashDot);
    protected static readonly IBrush FinalBrush  = Brushes.Black;

    public (Point First, Point Second) Coordinates { get; set; } = initialCoordinates;

    public virtual bool Temporary { get; set; } = isTemporary;

    public abstract void Draw(DrawingContext context);
}
