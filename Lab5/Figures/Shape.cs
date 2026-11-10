using Avalonia;
using Avalonia.Media;

namespace Lab5.Figures;

public abstract class Shape((Point first, Point second) initialCoordinates, ShapeState state = ShapeState.Temporary)
{
    protected static readonly IPen FinalPen = new Pen(Brushes.Black, 2);
    protected static readonly IPen TemporaryPen = new Pen(Brushes.Black, 2, DashStyle.DashDot);
    protected static readonly IPen HighlightPen = new Pen(Brushes.DodgerBlue, 3);
    protected static readonly IBrush FinalBrush = Brushes.Black;
    protected static readonly IBrush HighlightBrush = new SolidColorBrush(Color.FromArgb(80, 30, 144, 255));

    public (Point First, Point Second) Coordinates { get; set; } = initialCoordinates;

    public ShapeState State { get; set; } = state;

    protected virtual IBrush? FillBrush => null;

    public void Show(DrawingContext context)
    {
        switch (State)
        {
            case ShapeState.Final:
                Draw(context);
                break;
            case ShapeState.Temporary:
                Temporary(context);
                break;
            case ShapeState.Highlighted:
                Highlight(context);
                break;
        }
    }

    public void Draw(DrawingContext context) => Draw(context, CreateStyle(ShapeState.Final));

    public void Temporary(DrawingContext context) => Draw(context, CreateStyle(ShapeState.Temporary));

    public void Highlight(DrawingContext context) => Draw(context, CreateStyle(ShapeState.Highlighted));

    public void Draw(DrawingContext context, ShapeStyle style) => DrawWith(context, style);

    protected virtual ShapeStyle CreateStyle(ShapeState state) => state switch
    {
        ShapeState.Final => new ShapeStyle(state, FinalPen, FillBrush),
        ShapeState.Temporary => new ShapeStyle(state, TemporaryPen, null),
        ShapeState.Highlighted => new ShapeStyle(state, HighlightPen, HighlightBrush),
        _ => new ShapeStyle(state, FinalPen, FillBrush)
    };

    protected abstract void DrawWith(DrawingContext context, ShapeStyle style);
}
