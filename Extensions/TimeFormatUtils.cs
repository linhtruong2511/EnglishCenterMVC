namespace EnglishCenterMVC.Extensions
{
    public static class TimeFormatUtils
    {
        public static int ToMinutes(int totalSeconds)
        {
            return (int)Math.Ceiling(totalSeconds / 60.0);
        }

        public static string ToMinuteSecond(int totalSeconds)
        {
            var minutes = totalSeconds / 60;
            var seconds = totalSeconds % 60;
            return $"{minutes}:{seconds:D2}";
        }

        public static string ToHourMinuteSecond(int totalSeconds)
        {
            var hours = totalSeconds / 3600;
            var minutes = (totalSeconds % 3600) / 60;
            var seconds = totalSeconds % 60;

            return hours > 0
                ? $"{hours}:{minutes:D2}:{seconds:D2}"
                : $"{minutes}:{seconds:D2}";
        }
    }
}
