namespace StoryForge.Core.Data;

public class StorySetting
{
    public StorySettingId Id { get; set; } = StorySettingId.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;

    public static StorySetting New(string name) =>
        new() { Id = StorySettingId.New(), Name = name };

    public StorySettingEntry ToEntry() => new StorySettingEntry(Id, Name);
}

public readonly record struct StorySettingId(string Value) : ITypedId
{
    public static StorySettingId Empty => new(string.Empty);
    public static StorySettingId New() => new(TypedId.New());
    public static StorySettingId From(string value) => new(TypedId.From(value));
}

public record StorySettingEntry(StorySettingId Id, string Name) : IIdentifiable
{
    public string IdString => Id.Value;
    public override string ToString() => Name;
}
