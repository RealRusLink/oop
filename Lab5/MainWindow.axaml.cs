using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab5.Figures;

namespace Lab5;

public partial class MainWindow : Window
{
    private readonly ShapeFileManager _files;

    public ObservableCollectionForce<Shape> CompletedShapes { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        DrawingCanvas.CompletedShapes = CompletedShapes;

        _files = new ShapeFileManager(this);
        _files.CurrentPathChanged += OnCurrentPathChanged;

        foreach (var (shapeType, text) in ShapeTools.All)
        {
            ObjectsMenu.Items.Add(ShapeTools.CreateMenuItem(shapeType, text, OnShapeSelectClick));
            ToolbarButtons.Children.Add(ShapeTools.CreateToolbarButton(shapeType, text, OnShapeSelectClick));
        }

        UpdateTitle();
        SelectShape(typeof(CubeShape));
    }

    public Type? CurrentShape { get; private set; }

    private void SelectShape(Type shapeType)
    {
        if (!shapeType.IsSubclassOf(typeof(Shape)) || CurrentShape == shapeType) return;

        CurrentShape = shapeType;
        DrawingCanvas.ShapeType = shapeType;
        ShapeTools.MarkSelected(shapeType, ToolbarButtons, ObjectsMenu);
    }

    private void OnShapeSelectClick(object? sender, RoutedEventArgs e)
    {
        switch (sender)
        {
            case MenuItem { Icon: RadioButton { Tag: Type shapeType } }:
                SelectShape(shapeType);
                break;
            case RadioButton { Tag: Type shapeType }:
                SelectShape(shapeType);
                break;
        }
    }

    private void OnCurrentPathChanged(object? sender, EventArgs e) => UpdateTitle();

    private void UpdateTitle()
    {
        var path = _files.CurrentPath;
        Title = path is null ? "Lab 5" : $"Lab 5 - {path}";
    }

    private void OnOpenObjectsTableClick(object? sender, RoutedEventArgs e)
    {
        var table = new ObjectsTable(CompletedShapes);
        table.SelectionChanged += OnTableSelectionChanged;
        table.DeleteRequested += OnTableDeleteRequested;
        table.Show();
    }

    private void OnTableSelectionChanged(object? sender, ShapeSelectionChangedEventArgs e)
    {
        foreach (var shape in e.Added)
        {
            shape.State = ShapeState.Highlighted;
        }

        foreach (var shape in e.Removed)
        {
            shape.State = ShapeState.Final;
        }

        CompletedShapes.ForceEvent();
    }

    private void OnTableDeleteRequested(object? sender, IReadOnlyList<Shape> shapes) =>
        CompletedShapes.RemoveRange(shapes);

    private async void OnOpenFileClick(object? sender, RoutedEventArgs e) =>
        await _files.OpenAsync(CompletedShapes);

    private async void OnSaveFileClick(object? sender, RoutedEventArgs e) =>
        await _files.SaveAsync(CompletedShapes);

    private async void OnSaveFileAsClick(object? sender, RoutedEventArgs e) =>
        await _files.SaveAsAsync(CompletedShapes);
}
