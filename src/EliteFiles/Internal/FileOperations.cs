namespace EliteFiles.Internal
{
    internal static class FileOperations
    {
        public static T? RetryIfNull<T>(Func<T?> action, int retries)
            where T : class
        {
            return RetryIfFailed(action, x => x != null, retries);
        }

        public static T RetryIfFailed<T>(Func<T> action, Func<T, bool> test, int retries)
        {
            T res = action();

            // Since we may catch the file mid-update,
            // we wait a bit and try again.
            while (!test(res) && retries > 0)
            {
                Thread.Sleep(1);
                res = action();
                retries--;
            }

            return res;
        }
    }
}
