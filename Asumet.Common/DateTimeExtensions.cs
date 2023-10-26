namespace Asumet.Common
{
    /// <summary>
    /// Exxtension methods for <see cref="DateTime"/>
    /// </summary>
    public static class DateTimeExtensions
    {
        public static DateTime? SetKindUtc(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
            {
                return null;
            }

            return dateTime.Value.SetKindUtc();
        }

        public static DateTime SetKindUtc(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime;
            }
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }
    }
}
