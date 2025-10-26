namespace FluentUISourceGenerator.Tests.Mocks;

public class BaseField<TValue>
{
    public virtual TValue value { get; set; }
}

public class TextField : BaseField<string>
{
    public override string value { get; set; }
}
