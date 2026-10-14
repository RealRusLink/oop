using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab1.Types;

namespace Lab1;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private async void Flow_OnClick(object? sender, RoutedEventArgs e)
    {
        var windows = new LinkedList<FlowWindow>([
            FlowWindow.First(),
            new(disabledResults: DialogResult.Next)
        ]);

        for (var node = windows.First; node is not null; )
        {
            var result = await node.Value.ShowDialog();

            node = result switch
            {
                DialogResult.Previous => node.Previous ?? node,
                DialogResult.Next or DialogResult.Ok => node.Next,
                DialogResult.Cancel => null,
            };
        }

        foreach (var window in windows)
        {
            window.Close();
        }
    }

    private async void Group_OnClick(object? sender, RoutedEventArgs e)
    {
        var g = await (new GroupListWindow()).ShowDialog<GroupSelectionResult>(this);
        if (g is GroupSelectionResult.Ok ok) SelectedGroup.Text = ok.Selection;
    }
}