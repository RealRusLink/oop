using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Lab5.Figures;

namespace Lab5;

public partial class ObjectsTable : Window
{
    public ObservableCollection<Shape> Shapes { get; }

    public ObjectsTable(ObservableCollection<Shape> shapes)
    {
        Shapes = shapes;
        InitializeComponent();
        DataContext = this;
    }

    private void DataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is DataGrid dg && e is { AddedItems: [Shape shape], RemovedItems: [] })
        {
            dg.SelectedItem = null;
            Shapes.Remove(shape);
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
