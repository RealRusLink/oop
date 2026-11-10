using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Lab5.Figures;

namespace Lab5;

public sealed class ShapeFileManager(IStorageProvider storageProvider)
{
    private static readonly FilePickerFileType CsvFileType = new("CSV")
    {
        Patterns = ["*.csv"]
    };

    public string? CurrentPath
    {
        get;
        private set
        {
            if (field == value) return;

            field = value;
            CurrentPathChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? CurrentPathChanged;

    public async Task OpenAsync(ObservableCollection<Shape> shapes)
    {
        var path = await PickOpenFileAsync();

        if (path is null) return;

        CurrentPath = path;

        try
        {
            var loaded = ShapeCsv.Load(path);

            shapes.Clear();

            foreach (var shape in loaded)
            {
                shapes.Add(shape);
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public async Task SaveAsync(IReadOnlyCollection<Shape> shapes)
    {
        if (CurrentPath is null)
        {
            var path = await PickSaveFileAsync();

            if (path is null) return;

            CurrentPath = path;
        }

        Save(CurrentPath, shapes);
    }

    public async Task SaveAsAsync(IReadOnlyCollection<Shape> shapes)
    {
        var path = await PickSaveFileAsync();

        if (path is null) return;

        CurrentPath = path;
        Save(path, shapes);
    }

    private static void Save(string path, IReadOnlyCollection<Shape> shapes)
    {
        try
        {
            ShapeCsv.Save(path, shapes);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private async Task<string?> PickOpenFileAsync()
    {
        var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Відкрити файл",
            AllowMultiple = false,
            FileTypeFilter = [CsvFileType]
        });

        return files.Count > 0 ? files[0].TryGetLocalPath() : null;
    }

    private async Task<string?> PickSaveFileAsync()
    {
        var file = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Зберегти файл",
            DefaultExtension = "csv",
            FileTypeChoices = [CsvFileType]
        });

        return file?.TryGetLocalPath();
    }
}
