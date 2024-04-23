namespace AdoNetBasic.Business.Exceptions;

internal class ItemNotFoundException : Exception
{
    private const string Template = "{0} with code '{1}' not found";

    public ItemNotFoundException(string itemName, string itemCode)
        : base(string.Format(Template, itemName, itemCode))
    { }
}
