namespace MicroERP.Business.DataAccessLayer.ESC.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAllButNotEmpty(this string haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle))
            {
                return false;
            }

            return haystack.Contains(needle);
        }
    }
}