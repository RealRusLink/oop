using System;
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
        Checked = Objects.Items.OfType<MenuItem>().FirstOrDefault(i => i.Icon is RadioButton { IsChecked: true });
    }

    public event PropertyChangedEventHandler? ToolChanged;

    public MenuItem? Checked
    {
        get;
        private set
        {
            if (field == value)
            {
                return;
            }
            field = value;
            ToolChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Checked)));
        }
    }

    private void OnToolChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (Checked?.Tag is ShapeKind kind)
        {
            DrawingCanvas.ShapeKind = kind;
        }
    }

    private void OnObjectSelectClick(object? sender, RoutedEventArgs e)
    {
        if (sender is MenuItem { Icon: RadioButton radioButton } item)
        {
            radioButton.IsChecked = true;
            Checked = item;
        }
    }
}
