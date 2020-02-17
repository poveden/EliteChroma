using System;
using System.Threading;

namespace EliteFiles.Internal
{
    internal static class FileOperations
    {
        public static T RetryIfNull<T>(Func<T> action, int retries)
            where T : class
        {
            T res = action();

            // Since we may catch the file mid-update,
            // we wait a bit and try again.
            while (res == null && retries > 0)
            {
                Thread.Sleep(1);
                res = action();
                retries--;
            }

            return res;
        }
    }
}
