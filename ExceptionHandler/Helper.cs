using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public class Helper
    {
        public static StringBuilder RetryMessage;
        public static async Task<T> TryAndRetry<T>(Func<Task<T>> action, TimeSpan retryInterval, int retryCount)
        {
            RetryMessage = new StringBuilder();
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    return await action();

                }
                catch (Exception ex)
                {
                    RetryMessage.Append($"Retry {i + 1}. " );
                    await Task.Delay(retryInterval);
                }
            }
            return await action();
        }
    }
}
