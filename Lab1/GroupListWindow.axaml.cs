using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Linq;
using Lab1.Types;

namespace Lab1;

public partial class GroupListWindow : Window
{
    public GroupListWindow()
    {
        InitializeComponent();
        GroupList.ItemsSource =  new[] {"ІМ-51", "ІМ-52", "ІМ-53", "ІМ-54"};
    }


    private void OkButton_OnClick(object? sender, RoutedEventArgs e)
    {       
        Close(new GroupSelectionResult.Ok(GroupList?.SelectedItems?.Cast<string>().FirstOrDefault() ?? "Група не обрана"));
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close(GroupSelectionResult.Cancel.Instance);
    }
}