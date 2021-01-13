namespace LK.Lib.Helper
{
    public interface IUrlHelper
    {
        string CreateSlug(string title);
        string RemoveDiacritics(string text);
        string RemoveReservedUrlCharacters(string text);
    }
}