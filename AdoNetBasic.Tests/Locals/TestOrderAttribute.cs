namespace AdoNetBasic.Tests.Locals;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestOrderAttribute : Attribute
{
    public int Priority { get; private set; }

    public TestOrderAttribute(int priority) => Priority = priority;
}
