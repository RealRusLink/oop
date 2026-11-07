using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Lab5.Figures;

namespace Lab5;

public class DrawingCanvas : Control
{
    private const int MaxShapes = 116;

    private readonly IBrush _backgroundBrush = Brushes.White;
    private readonly Shape?[] _completedShapes = new Shape?[MaxShapes];

    private int _completedShapeCount;
    private Shape? _pendingShape;

    public Type ShapeType
    {
        get;
        set
        {
            if (value is null || !value.IsSubclassOf(typeof(Shape))) return;
            field = value;
        }
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var canvasBounds = new Rect(Bounds.Size);
        if (canvasBounds.Width <= 0 || canvasBounds.Height <= 0) return;

        context.FillRectangle(_backgroundBrush, canvasBounds);

        using (context.PushClip(canvasBounds))
        {
            for (var index = 0; index < _completedShapeCount; index++)
            {
                _completedShapes[index]?.Draw(context);
            }

            _pendingShape?.Draw(context);
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        var position = e.GetPosition(this);
        var newShape = ShapeType
            .GetConstructor([typeof((Point, Point)), typeof(bool)])
            .Invoke(new object[] { (position, position), true });

        if (newShape is Shape shape)
        {
            _pendingShape = shape;
        }

        InvalidateVisual();
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        var position = e.GetPosition(this);

        if (_pendingShape is not null)
        {
            _pendingShape.Coordinates = (_pendingShape.Coordinates.First, position);
        }

        InvalidateVisual();
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        if (_pendingShape is null) return;

        var position = e.GetPosition(this);
        _pendingShape.Coordinates = (_pendingShape.Coordinates.First, position);
        _pendingShape.Temporary = false;

        if (_completedShapeCount < MaxShapes)
        {
            _completedShapes[_completedShapeCount++] = _pendingShape;
        }

        _pendingShape = null;
        InvalidateVisual();
    }
}
