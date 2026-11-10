using System;
using System.Collections.Generic;
using Lab5.Figures;

namespace Lab5;

public sealed class ShapeSelectionChangedEventArgs(IReadOnlyList<Shape> added, IReadOnlyList<Shape> removed)
    : EventArgs
{
    public IReadOnlyList<Shape> Added { get; } = added;
    public IReadOnlyList<Shape> Removed { get; } = removed;
}
