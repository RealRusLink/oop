using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab1.Types;

namespace Lab1;

public partial class FlowWindow : Window
{
    private TaskCompletionSource<DialogResult>? _tcs;

    public FlowWindow(string name = "Dialog", params DialogResult[] disabledResults)
    {
        InitializeComponent();
        Title = name;
        DisableButtons(disabledResults);
    }
    

    public Task<DialogResult> ShowDialog()
    {
        _tcs = new TaskCompletionSource<DialogResult>();
        Show();
        return _tcs.Task;
    }

    protected void DisableButtons(IEnumerable<DialogResult> disabledResults)
    {
        foreach (var result in disabledResults)
        {
            if (this.FindControl<Button>(result.ToString()) is { } button)
            {
                button.IsVisible = false;
                button.IsEnabled = false;
            }
        }
    }

    protected void Finish(DialogResult result)
    {
        Hide();
        _tcs?.TrySetResult(result);
    }

    protected void Next_OnClick(object? sender, RoutedEventArgs e) => Finish(DialogResult.Next);
    protected void Previous_OnClick(object? sender, RoutedEventArgs e) => Finish(DialogResult.Previous);
    protected void Cancel_OnClick(object? sender, RoutedEventArgs e) => Finish(DialogResult.Cancel);
    protected void Action_OnClick(object? sender, RoutedEventArgs e) => Finish(DialogResult.Ok);
}