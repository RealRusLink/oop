using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Lab5.Figures;

namespace Lab5;

public sealed class ShapeFileManager(TopLevel owner)
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

    public async Task OpenAsync(ObservableCollectionForce<Shape> shapes)
    {
        var path = await PickOpenFileAsync();

        if (path is null)
        {
            Console.WriteLine("Відкриття файлу скасовано");
            return;
        }

        CurrentPath = path;

        try
        {
            var loaded = ShapeCsv.Load(path);

            shapes.Clear();

            foreach (var shape in loaded)
            {
                shapes.Add(shape);
            }

            Console.WriteLine($"Завантажено {loaded.Count} об'єктів з файлу: {path}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Не вдалося відкрити файл: {exception.Message}");
        }
    }

    public async Task SaveAsync(IReadOnlyCollection<Shape> shapes)
    {
        if (CurrentPath is null)
        {
            var path = await PickSaveFileAsync();

            if (path is null)
            {
                Console.WriteLine("Збереження файлу скасовано");
                return;
            }

            CurrentPath = path;
        }

        Save(CurrentPath, shapes);
    }

    public async Task SaveAsAsync(IReadOnlyCollection<Shape> shapes)
    {
        var path = await PickSaveFileAsync();

        if (path is null)
        {
            Console.WriteLine("Збереження файлу скасовано");
            return;
        }

        CurrentPath = path;
        Save(path, shapes);
    }

    private static void Save(string path, IReadOnlyCollection<Shape> shapes)
    {
        try
        {
            ShapeCsv.Save(path, shapes);
            Console.WriteLine($"Збережено {shapes.Count} об'єктів у файл: {path}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Не вдалося зберегти файл: {exception.Message}");
        }
    }

    private async Task<string?> PickOpenFileAsync()
    {
        var files = await owner.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Відкрити файл",
            AllowMultiple = false,
            FileTypeFilter = [CsvFileType]
        });

        return files.Count > 0 ? files[0].TryGetLocalPath() : null;
    }

    private async Task<string?> PickSaveFileAsync()
    {
        var file = await owner.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Зберегти файл",
            DefaultExtension = "csv",
            FileTypeChoices = [CsvFileType]
        });

        return file?.TryGetLocalPath();
    }
}
