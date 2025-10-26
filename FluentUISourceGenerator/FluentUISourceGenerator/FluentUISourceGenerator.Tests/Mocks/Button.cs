namespace FluentUISourceGenerator.Tests.Mocks;

public class TextElement
{
    public virtual string text { get; set; }
}

public class Button : TextElement
{
    public override string text { get; set; }
}
