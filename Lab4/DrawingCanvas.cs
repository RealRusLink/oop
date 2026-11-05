using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Lab4.Figures;

namespace Lab4;

public class DrawingCanvas : Control
{   
    private const int ShapeCapacity = 116;

    private readonly IBrush _backgroundBrush = Brushes.White;
    private readonly Shape?[] _shapes = new Shape?[ShapeCapacity];

    private int _shapeCount;
    private Shape? _currentShape;

    public ShapeKind ShapeKind { get; set; }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        var bounds = new Rect(Bounds.Size);
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }
        context.FillRectangle(_backgroundBrush, bounds);

        using (context.PushClip(bounds))
        {
            for (var i = 0; i < _shapeCount; i++)
            {
                _shapes[i]?.Draw(context);
            }

            _currentShape?.Draw(context);
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        var pointerPosition = e.GetPosition(this);
        _currentShape = ShapeKind switch
        {
            ShapeKind.Point => new PointShape((pointerPosition, pointerPosition)),
            ShapeKind.Line => new LineShape((pointerPosition, pointerPosition)),
            ShapeKind.Rectangle => new RectangleShape((pointerPosition, pointerPosition)),
            ShapeKind.Ellipse => new EllipseShape((pointerPosition, pointerPosition)),
            _ => null,
        };

        InvalidateVisual();
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        var pointerPosition = e.GetPosition(this);

        if (_currentShape is not null)
        {
            _currentShape.Coordinates = (_currentShape.Coordinates.First, pointerPosition);
        }

        InvalidateVisual();
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        if (_currentShape is null)
        {
            return;
        }

        var pointerPosition = e.GetPosition(this);
        _currentShape.Coordinates = (_currentShape.Coordinates.First, pointerPosition);
        _currentShape.Temporary = false;

        if (_shapeCount < ShapeCapacity)
        {
            _shapes[_shapeCount++] = _currentShape;
        }

        _currentShape = null;

        InvalidateVisual();
    }
}
