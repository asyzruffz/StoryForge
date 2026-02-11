namespace StoryForge.Core.Data;

public class Plot
{
    public PlotId Id { get; set; } = PlotId.Empty;
    public string Name { get; set; } = string.Empty;
    public Importance Importance { get; set; }
    public string Characters { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;

    public static Plot New(string name) =>
        new() { Id = PlotId.New(), Name = name };

    public PlotEntry ToEntry() => new PlotEntry(Id, Name, Importance);
}

public readonly record struct PlotId(string Value) : ITypedId
{
    public static PlotId Empty => new(string.Empty);
    public static PlotId New() => new(TypedId.New());
    public static PlotId From(string value) => new(TypedId.From(value));
}

public record PlotEntry(PlotId Id, string Name, Importance Importance) : IIdentifiable
{
    public string IdString => Id.Value;
    public override string ToString() => Name;
}
