namespace MachineReporting.Api.Utilities
{
    public static class UniqueNumberGenerator
    {
        private static readonly object _lock = new object();
        private static long _lastTimestamp = 0;
        private static int _counter = 0;

        public static long GenerateUnique17DigitNumber()
        {
        lock (_lock)
        {
            // گرفتن timestamp به میلی‌ثانیه از تاریخ 2020/01/01
            long timestamp = (long)(DateTime.UtcNow - new DateTime(2020, 1, 1)).TotalMilliseconds;

            if (timestamp == _lastTimestamp)
            {
                _counter++;
            }
            else
            {
                _counter = 0;
                _lastTimestamp = timestamp;
            }

            var random = new Random();
            int randomPart = random.Next(0, 9999);

            // ساخت رشته عددی 17 رقمی
            string numberStr = $"{timestamp:D11}{_counter:D2}{randomPart:D4}";

            // تبدیل رشته به long و بازگرداندن
            return long.Parse(numberStr);
        }
        }
    }
}