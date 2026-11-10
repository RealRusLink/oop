using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Lab5.Figures;

namespace Lab5;

public static class ShapeTools
{
    private const string UnknownIconPath = "icons/unknown.png";

    public static IEnumerable<(Type ShapeType, ShapeTextAttribute Text)> All =>
        ShapeCatalog.Attributes.Select(pair => (pair.Key, pair.Value));

    public static MenuItem CreateMenuItem(Type shapeType, ShapeTextAttribute text, EventHandler<RoutedEventArgs> onClick)
    {
        var menuItem = new MenuItem
        {
            Icon = new RadioButton
            {
                Name = text.ToolName,
                GroupName = "MenuButtons",
                Tag = shapeType
            },
            Header = text.ToolName
        };

        menuItem.Click += onClick;
        return menuItem;
    }

    public static RadioButton CreateToolbarButton(Type shapeType, ShapeTextAttribute text, EventHandler<RoutedEventArgs> onClick)
    {
        var button = new RadioButton
        {
            Name = shapeType.Name,
            Content = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 6,
                Children =
                {
                    new Image
                    {
                        Width = 20,
                        Height = 20,
                        Source = LoadIcon(text.IconPath)
                    },
                    new TextBlock
                    {
                        Text = text.ToolName,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            },
            GroupName = "ToolbarGroup",
            Tag = shapeType
        };

        button.Click += onClick;
        ToolTip.SetTip(button, text.ToolTip);
        return button;
    }

    public static void MarkSelected(Type? shapeType, StackPanel toolbar, MenuItem objectsMenu)
    {
        var toolbarButtons = toolbar.Children.OfType<RadioButton>();
        var menuButtons = objectsMenu.Items
            .OfType<MenuItem>()
            .Select(menuItem => menuItem.Icon)
            .OfType<RadioButton>();

        foreach (var button in toolbarButtons.Concat(menuButtons))
        {
            button.IsChecked = button.Tag is Type type && type == shapeType;
        }
    }

    private static Bitmap LoadIcon(string? iconPath)
    {
        var relativePath = string.IsNullOrWhiteSpace(iconPath)
            ? UnknownIconPath
            : iconPath.TrimStart('/');

        try
        {
            using var stream = AssetLoader.Open(new Uri($"avares://Lab5/{relativePath}"));
            return new Bitmap(stream);
        }
        catch
        {
            using var stream = AssetLoader.Open(new Uri($"avares://Lab5/{UnknownIconPath}"));
            return new Bitmap(stream);
        }
    }
}
