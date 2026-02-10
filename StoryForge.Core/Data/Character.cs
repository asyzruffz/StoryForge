namespace StoryForge.Core.Data;

public class Character
{
    public CharacterId Id { get; set; } = CharacterId.Empty;
    public string Name { get; set; } = string.Empty;
    public string Motivation { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public string Conflict { get; set; } = string.Empty;
    public string Epiphany { get; set; } = string.Empty;
    public Summary Summary { get; set; } = default!;
    public string? Notes { get; set; }
    public string? Details { get; set; }

    public static Character New(string name) =>
        new() { Id = CharacterId.New(), Name = name, Summary = Summary.New() };

    public CharacterEntry ToEntry() => new CharacterEntry(Id, Name);
}

public readonly record struct CharacterId(string Value) : ITypedId
{
    public static CharacterId Empty => new(string.Empty);
    public static CharacterId New() => new(TypedId.New());
    public static CharacterId From(string value) => new(TypedId.From(value));

    //public static CharacterId From(string name) =>
    //    new(Uri.EscapeDataString(name.Replace(" ", "_")));
}

public record CharacterEntry(CharacterId Id, string Name) : IIdentifiable
{
    public string IdString => Id.Value;
    public override string ToString() => Name;
}
