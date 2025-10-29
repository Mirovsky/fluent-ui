namespace FluentUISourceGenerator.Tests.Mocks;

public class GenericA<T>
{
    public string label { get; set; }

    public T value { get; set; }

    public T genericValue { get; set; }

    public virtual T genericValue2 { get; set; }

    public virtual void Method() { }
}

public class GenericB : GenericA<int>
{
    public override int genericValue2 { get; set; }

    public override void Method() { }
}
