using Avalonia.Media;

namespace Lab5.Figures;

public readonly record struct ShapeStyle(ShapeState State, IPen Pen, IBrush? Fill);
