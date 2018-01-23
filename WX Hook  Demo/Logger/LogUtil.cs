using System;
using System.IO;

namespace LogHelper
{
    public static class LogUtil
    {
        public static ILogger DEFAULT       = new Logger("DEFAULT_LOG");
        public static ILogger WXHOOKUI      = new Logger("WXHOOKUI_LOG");
        public static ILogger WXHOOKSERVICE = new Logger("WXHOOKSERVICE_LOG");

        public static void InitHomePath(string homePath)
        {
            DEFAULT       = new Logger(homePath, "DEFAULT_LOG");
            WXHOOKUI      = new Logger(homePath, "WXHOOKUI_LOG");
            WXHOOKSERVICE = new Logger(homePath, "WXHOOKSERVICE_LOG");
        }
    }
}
