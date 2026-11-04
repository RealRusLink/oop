using Avalonia;
using Avalonia.Media;

namespace Lab2.Figures;

public abstract class Shape((Point first, Point second) coordinates, bool temporary = true)
{
    protected static readonly IPen FinalPen = new Pen(Brushes.Black, 2);
    protected static readonly IPen TemporaryPen = new Pen(Brushes.Black, 2, new DashStyle(new double[] { 4, 3 }, 0));
    protected static readonly IBrush FinalBrush = Brushes.Black;

    public (Point First, Point Second) Coordinates { get; set; } = coordinates;

    public bool Temporary { get; set; } = temporary;

    public abstract void Draw(DrawingContext context);
}
