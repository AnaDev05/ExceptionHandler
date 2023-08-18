using System;
using System.IO;
using Serilog;
using Serilog.Templates;

namespace ExceptionHandler
{
    public sealed class Logger
    {
        public static void LogExceptionToJson(ToLog error)
        {
            string baseDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\ExceptionHandler\ExceptionHandler\Exceptions\";

            string monthDirectoryName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
            string monthDirectoryPath = Path.Combine(baseDirectoryPath, monthDirectoryName);

            if (!File.Exists(monthDirectoryPath))
            {
                Directory.CreateDirectory(monthDirectoryPath);
            }

            string filePath = Path.Combine(monthDirectoryPath, "DailyExceptions.json");
            var log = new LoggerConfiguration()
                            .WriteTo.File(new ExpressionTemplate("{ {Timestamp: @t, Message: @m, Properties: @p} },\n"), filePath, rollingInterval: RollingInterval.Day, shared: true)
                            .CreateLogger();
            log.Error("Error Logged: {Message}, with properties: {@Properties}", error.ErrorDetails.Message, error);

        }
    }
}
