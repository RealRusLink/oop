using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab1.Types;

namespace Lab1;

public partial class FirstFlowWindow : FlowWindow
{
    private TaskCompletionSource<DialogResult>? _tcs;

    public FirstFlowWindow(string name = "Dialog") : base(name, [DialogResult.Ok]){}
}