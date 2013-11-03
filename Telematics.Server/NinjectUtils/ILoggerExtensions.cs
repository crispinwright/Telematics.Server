using System;
using log4net.Core;
using Telematics.Server.Const;

namespace Ninject.Extensions.Logging.Log4net
{
    /// <summary>
    /// This class provides some lambda function extensions to the ILogger interface. These methods allow
    /// you to pass in a lambda that will only be executed if the relevant logger level is active. This means
    /// that you can liberally sprinkle lots of Debug-level logging throughout your code, with lots of nice juicy
    /// error-exposing detail, but not pay the evaluation/generation cost of all the messages when not actually logging debug
    /// level messages.
    /// 
    /// We don't provide Error or Fatal level support, as you should ALWAYS be logging Error and Fatal messages everywhere, anyway (IMO).
    /// 
    /// The namespace is as it is so that you automatically get these extensions when you reference the correct logger.
    /// </summary>
    public static class ILoggerExtensions
    {

        static ILoggerExtensions()
        {
            // Set up the event Id to a default value on initialisation.
            ClearEventID();
        }
        private const string WindowsEventIDPropertyKey = "EventID";

        #region Debug

        public static void Debug(this ILogger logger, Func<string> message)
        {
            if (logger.IsDebugEnabled)
                logger.Debug(message());
        }

        public static void Debug(this ILogger logger, Exception exception, Func<string> message)
        {
            if (logger.IsDebugEnabled)
                logger.Debug(exception, message());
        }

        #endregion

        #region Info

        public static void Info(this ILogger logger, Func<string> message, WindowsEventID eventID)
        {
            if (logger.IsInfoEnabled)
            {
                SetEventID(eventID);
                logger.Info(message.Invoke());
                ClearEventID();
            }
        }

        public static void Info(this ILogger logger, Func<string> message)
        {
            if (logger.IsInfoEnabled)
                logger.Info(message());
        }

        public static void Info(this ILogger logger, Exception exception, Func<string> message)
        {
            if (logger.IsInfoEnabled)
                logger.Info(exception, message());
        }
        #endregion

        #region Warn

        public static void Warn(this ILogger logger, Func<string> message, WindowsEventID eventID)
        {
            if (logger.IsWarnEnabled)
            {
                SetEventID(eventID);
                logger.Warn(message.Invoke());
                ClearEventID();
            }
        }
        public static void Warn(this ILogger logger, Func<string> message)
        {
            if (logger.IsWarnEnabled)
                logger.Warn(message());
        }

        public static void Warn(this ILogger logger, Exception exception, Func<string> message)
        {
            if (logger.IsWarnEnabled)
                logger.Warn(exception, message());
        }
        #endregion

        #region Error

        public static void Error(this ILogger logger, Func<string> message, WindowsEventID eventID)
        {
            if (logger.IsErrorEnabled)
            {
                SetEventID(eventID);
                logger.Error(message.Invoke());
                ClearEventID();
            }
        }

        #endregion

        private static void SetEventID(WindowsEventID eventID)
        {
            log4net.ThreadContext.Properties[WindowsEventIDPropertyKey] = (int)eventID;
        }

        private static void ClearEventID()
        {
            log4net.ThreadContext.Properties.Remove(WindowsEventIDPropertyKey); // clear the property key
            log4net.ThreadContext.Properties[WindowsEventIDPropertyKey] = (int)WindowsEventID.Unspecified; // reset the property to a default
        }
    }
}