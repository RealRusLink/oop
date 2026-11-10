using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia;
using Lab5.Figures;

namespace Lab5;

public static class ShapeCatalog
{
    public static Dictionary<Type, ShapeTextAttribute> Attributes { get; } = CollectAttributes();

    public static ShapeTextAttribute Get(Type shapeType) =>
        Attributes.TryGetValue(shapeType, out var attribute)
            ? attribute
            : CreateFallback(shapeType);

    public static Shape? Create(Type shapeType, (Point First, Point Second) coordinates, ShapeState state)
    {
        var constructor = shapeType.GetConstructor([typeof((Point, Point)), typeof(ShapeState)]);

        return constructor?.Invoke([coordinates, state]) as Shape;
    }

    private static Dictionary<Type, ShapeTextAttribute> CollectAttributes()
    {
        var attributes = new Dictionary<Type, ShapeTextAttribute>();
        var shapeTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Shape)));

        foreach (var shapeType in shapeTypes)
        {
            attributes[shapeType] = CreateAttribute(shapeType);
        }

        return attributes;
    }

    private static ShapeTextAttribute CreateAttribute(Type shapeType) =>
        Attribute.GetCustomAttribute(shapeType, typeof(ShapeTextAttribute)) is ShapeTextAttribute attribute
            ? attribute
            : CreateFallback(shapeType);

    private static ShapeTextAttribute CreateFallback(Type shapeType) =>
        new(shapeType.Name, shapeType.FullName ?? shapeType.Name);
}
