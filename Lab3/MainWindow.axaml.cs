using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab3.Figures;

namespace Lab3;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ToolChanged += OnToolChanged;
        CurrentShape = ShapeKind.Point;
    }

    public ShapeKind? CurrentShape
    {
        get;
        private set
        {
            if (field == value)
            {
                return;
            }
            field = value;
            ToolChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentShape)));
        }
    } = null;

    public event PropertyChangedEventHandler? ToolChanged;


    private void OnToolChanged(object? sender, PropertyChangedEventArgs e)
    {
        DrawingCanvas.ShapeKind = CurrentShape ?? ShapeKind.Point;
        var toolbarButtons = ToolbarButtons.Children.OfType<RadioButton>().Where(rb => rb is { Tag: ShapeKind});
        var menuButtons = ObjectsMenu.Items.OfType<MenuItem>()
            .Select(item => item.Icon)
            .OfType<RadioButton>()
            .Where(button => button.Tag is ShapeKind);
        var allButtons = toolbarButtons.Concat(menuButtons);
        foreach (var button in allButtons)
        {
            if (button?.Tag is ShapeKind shapeKind)
            {
                button.IsChecked = shapeKind == CurrentShape;
            }
        }
    }

    private void OnObjectSelectClick(object? sender, RoutedEventArgs e)
    {
        if (sender is MenuItem { Icon: RadioButton { Tag: ShapeKind kind } })
        {
            CurrentShape = kind;
        }
    }

    private void OnToolbarButtonClick(object? sender, RoutedEventArgs e)
    {
        if (sender is RadioButton { Tag: ShapeKind kind })
        {
            CurrentShape = kind;
        }
    }
}
