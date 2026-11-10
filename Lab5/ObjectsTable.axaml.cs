using System;
using System.Collections.ObjectModel;
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

    public ObjectsTable(ObservableCollectionForce<Shape> shapes)
    {
        Shapes = shapes;
        InitializeComponent();
        DataContext = this;
    }

    private void DataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is DataGrid dg)
        {
            foreach (var item in e.AddedItems)
            {
                if (item is Shape shape) shape.State = ShapeState.Highlighted;
            }
            foreach (var item in e.RemovedItems)
            {
                if (item is Shape shape) shape.State = ShapeState.Final;
            }
            Shapes.ForceEvent();
        }
    }

    private void DataGridTable_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (sender is DataGrid dg && e.Key is Key.Delete)
        {
            Shapes.RemoveRange(DataGridTable.SelectedItems.OfType<Shape>());
            DataGridTable.SelectedItem = null;
        }
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
                "Status" => shape.Status,
                _ => null
            }
            : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
