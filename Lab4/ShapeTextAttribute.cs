using System;

namespace Lab4;

public class ShapeTextAttribute(string name, string tip) : Attribute
{
    public string ToolTip { get; init; } = tip;
    public string ToolName { get; init; } = name;
}