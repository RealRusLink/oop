using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab4.Figures;

namespace Lab4;

public partial class MainWindow : Window
{
    private List<(Type ShapeType, ShapeTextAttribute Text)> CollectTools()
    {
        var tools = new List<(Type ShapeType, ShapeTextAttribute Text)>();
        var shapeTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Shape)))
            .ToList();

        foreach (var shapeType in shapeTypes)
        {
            var text = Attribute.GetCustomAttribute(shapeType, typeof(ShapeTextAttribute)) is ShapeTextAttribute attribute
                ? attribute
                : new ShapeTextAttribute(shapeType.Name, shapeType.FullName ?? shapeType.Name);

            tools.Add((shapeType, text));
        }

        return tools;
    }

    private void Fill<T>(Func<(Type ShapeType, ShapeTextAttribute Text), T> createItem, Action<T> registerItem)
    {
        var tools = CollectTools();
        var items = tools.Select(createItem).ToList();

        foreach (var item in items)
        {
            registerItem(item);
        }
    }

    public MainWindow()
    {
        InitializeComponent();

        Fill((tool) => new MenuItem
            {
                Icon = new RadioButton
                {
                    Name = tool.Text.ToolName,
                    GroupName = "MenuButtons",
                    Tag = tool.ShapeType
                },
                Header = tool.Text.ToolName,
            },
            menuItem =>
            {
                menuItem.Click += OnObjectSelectClick;
                ObjectsMenu.Items.Add(menuItem);
            });

        Fill((tool) =>
            {
                var toolButton = new RadioButton
                {
                    Name = tool.ShapeType.Name,
                    Content = tool.Text.ToolName,
                    GroupName = "ToolbarGroup",
                    Tag = tool.ShapeType
                };
                ToolTip.SetTip(toolButton, tool.Text.ToolTip);
                return toolButton;
            },
            toolButton =>
            {
                toolButton.Click += OnToolbarButtonClick;
                ToolbarButtons.Children.Add(toolButton);
            });

        ToolChanged += OnToolChanged;
        CurrentShape = typeof(CubeShape);
    }

    public Type? CurrentShape
    {
        get;
        private set
        {
            if (value is null || !value.IsSubclassOf(typeof(Shape))) return;
            if (field == value) return;

            field = value;
            ToolChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentShape)));
        }
    } = null;

    public event PropertyChangedEventHandler? ToolChanged;

    private void OnToolChanged(object? sender, PropertyChangedEventArgs e)
    {
        DrawingCanvas.ShapeType = CurrentShape ?? typeof(PointShape);

        var toolbarItems = ToolbarButtons.Children
            .OfType<RadioButton>()
            .Where(button => button is { Tag: Type shapeType } && shapeType.IsSubclassOf(typeof(Shape)));

        var menuItems = ObjectsMenu.Items
            .OfType<MenuItem>()
            .Select(menuItem => menuItem.Icon)
            .OfType<RadioButton>()
            .Where(button => button.Tag is Type shapeType && shapeType.IsSubclassOf(typeof(Shape)));

        var buttons = toolbarItems.Concat(menuItems);

        foreach (var button in buttons)
        {
            if (button.Tag is Type shapeType && shapeType.IsSubclassOf(typeof(Shape)))
            {
                button.IsChecked = shapeType == CurrentShape;
            }
        }
    }

    private void OnObjectSelectClick(object? sender, RoutedEventArgs e)
    {
        if (sender is MenuItem { Icon: RadioButton { Tag: Type shapeType } } && shapeType.IsSubclassOf(typeof(Shape)))
        {
            CurrentShape = shapeType;
        }
    }

    private void OnToolbarButtonClick(object? sender, RoutedEventArgs e)
    {
        if (sender is RadioButton { Tag: Type shapeType } && shapeType.IsSubclassOf(typeof(Shape)))
        {
            CurrentShape = shapeType;
        }
    }
}
