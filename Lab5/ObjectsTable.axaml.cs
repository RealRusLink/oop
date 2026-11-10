using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Lab5.Figures;

namespace Lab5;

public partial class ObjectsTable : Window
{
    public ObservableCollectionForce<Shape> Shapes { get; }

    public event EventHandler<ShapeSelectionChangedEventArgs>? SelectionChanged;

    public event EventHandler<IReadOnlyList<Shape>>? DeleteRequested;

    public ObjectsTable(ObservableCollectionForce<Shape> shapes)
    {
        Shapes = shapes;
        InitializeComponent();
        DataContext = this;
    }

    private void DataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var added = e.AddedItems.OfType<Shape>().ToList();
        var removed = e.RemovedItems.OfType<Shape>().ToList();

        if (added.Count == 0 && removed.Count == 0) return;

        SelectionChanged?.Invoke(this, new ShapeSelectionChangedEventArgs(added, removed));
    }

    private void DataGridTable_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key is not Key.Delete) return;

        var selected = DataGridTable.SelectedItems.OfType<Shape>().ToList();
        DataGridTable.SelectedItem = null;

        if (selected.Count > 0)
        {
            DeleteRequested?.Invoke(this, selected);
        }

        e.Handled = true;
    }
}

public sealed class ShapeTableValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is Shape shape
            ? parameter switch
            {
                "Name" => shape.Name,
                "StartPoint" => shape.StartPoint,
                "EndPoint" => shape.EndPoint,
                _ => null
            }
            : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
