namespace StoryForge.Core.Data;

public class WorldSetting
{
    public WorldSettingId Id { get; set; } = WorldSettingId.Empty;
    public string Name { get; set; } = string.Empty;
}

public readonly record struct WorldSettingId(string Value) : ITypedId
{
    public static WorldSettingId Empty => new(string.Empty);
    public static WorldSettingId New() => new(TypedId.New());
    public static WorldSettingId From(string value) => new(TypedId.From(value));
}

public record WorldEntry(WorldSettingId Id, string Name) : IIdentifiable
{
    public string IdString => Id.Value;
    public override string ToString() => Name;
}
