using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab1.Types;

namespace Lab1;

public partial class SecondFlowWindow : FlowWindow
{
    private TaskCompletionSource<DialogResult>? _tcs;

    public SecondFlowWindow(string name = "Dialog") : base(name, [DialogResult.Next]){}
}