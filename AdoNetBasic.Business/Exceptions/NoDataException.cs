namespace AdoNetBasic.Business.Exceptions;

internal class NoDataException : Exception
{
    private const string Template = "No {0} has been found in database";

    public NoDataException(string itemName)
        : base(string.Format(Template, itemName))
    { }
}
