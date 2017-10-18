﻿using System;
using System.Reflection;

namespace Logrila.Logging.Log4NetIntegration
{
    public class Log4NetLogger : Logrila.Logging.ILogger
    {
        private readonly Func<string, log4net.ILog> _loggerFactory;

        public Log4NetLogger()
        {
            _loggerFactory = (name) => log4net.LogManager.GetLogger(Assembly.GetCallingAssembly(), name);
        }

        public Log4NetLogger(Func<string, log4net.ILog> loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public ILog Get(string name)
        {
            return new Log4NetLog(_loggerFactory(name), name);
        }

        public static void Use()
        {
            Logrila.Logging.Logger.UseLogger(new Log4NetLogger());
        }

        public static void Use(log4net.ILog logger)
        {
            Logrila.Logging.Logger.UseLogger(new Log4NetLogger((name) => logger));
        }
    }
}
