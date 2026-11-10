using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Lab5.Figures;

namespace Lab5;

public class DrawingCanvas : Control
{

    private void CompletedShapesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        InvalidateVisual();
    }


    private const int MaxShapes = 116;

    private readonly IBrush _backgroundBrush = Brushes.White;
    private Shape? _pendingShape;

    public ObservableCollection<Shape>? CompletedShapes
    {
        get;
        set
        {
            field = value;
            value?.CollectionChanged += CompletedShapesOnCollectionChanged;
        }
    }

    public Type? ShapeType
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
            if (CompletedShapes is not null)
            {
                foreach (var shape in CompletedShapes)
                {
                    shape.Show(context);
                }
            }

            _pendingShape?.Show(context);
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        var position = e.GetPosition(this);
        var constructor = ShapeType?.GetConstructor([typeof((Point, Point)), typeof(ShapeState)]);
        if (constructor is null) return;

        var newShape = constructor.Invoke(new object[] { (position, position), ShapeState.Temporary });

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
        _pendingShape.State = ShapeState.Final;

        if (CompletedShapes is not null && CompletedShapes.Count < MaxShapes)
        {
            CompletedShapes.Add(_pendingShape);
        }

        _pendingShape = null;
        InvalidateVisual();
    }
}
