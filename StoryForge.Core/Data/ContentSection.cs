using System.Text;

namespace StoryForge.Core.Data;

public interface IContentSection
{
    IContentSection AddSection(IContentSection newSection);
    string ToText();
}

public class EmptySection : IContentSection
{
    public IContentSection AddSection(IContentSection newSection) => newSection;
    public string ToText() => string.Empty;
}

public class ContentSection(string Text) : IContentSection
{
    public string Text { get; set; } = Text;
    public IContentSection AddSection(IContentSection newSection) =>
        new MultipleSection { Sections = [this, newSection] };
    public string ToText() => Text;
}

public class MultipleSection : IContentSection
{
    public List<IContentSection> Sections { get; init; } = [];

    public IContentSection AddSection(IContentSection newSection)
    {
        Sections.Add(newSection);
        return this;
    }

    public string ToText() => Sections
        .Where(section => section is not EmptySection)
        .Aggregate(new StringBuilder(),
            (builder, next) => builder
                .Append(Environment.NewLine)
                .Append(Environment.NewLine)
                .Append(next.ToText()),
            builder => builder.ToString());
}
