using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Lab2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CheckedItemChanged += OnCheckedItemChanged;
        Checked = Objects.Items.OfType<MenuItem>().FirstOrDefault();
    }

    private void OnCheckedItemChanged(object? sender, PropertyChangedEventArgs e)
    {
        Title = $"Lab2 - {Checked.Header}";
    }

    public event PropertyChangedEventHandler? CheckedItemChanged;

    public MenuItem Checked
    {
        get;
        private set
        {
            if (field != value)
            {
                field = value;
                CheckedItemChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Checked)));
            }
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