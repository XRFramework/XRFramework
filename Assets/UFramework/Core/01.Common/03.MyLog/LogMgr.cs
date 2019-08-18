/****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2019 xiaojingli
 * 
 * https://github.com/JinLiGame/UnityFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ****************************************************************************/

using UnityEngine;

namespace UFramework
{
    class LogMgr : Singleton<LogMgr>
    {
        #region Fields
        /// <summary>
        /// Directory of log file.
        /// </summary>
        private readonly string LogFilePath = $"{Application.persistentDataPath}/Log/";
        /// <summary>
        /// Log file name.
        /// </summary>
        private const string LogFileName = "MyLog.log";
        /// <summary>
        /// Log level output to file.
        /// Any logs greater than or equal to this level will be output to log file.
        /// </summary>
        private const LogLevel FileOutputLogLevel = LogLevel.All;
        /// <summary>
        /// Log level send to server.
        /// Any logs greater than or equal to this level will be send to server.
        /// </summary>
        private const LogLevel ServerLogLevel = LogLevel.Error;
        /// <summary>
        /// Log level output to console.
        /// Any logs greater than or equal to this level will be output to console.
        /// </summary>
        private const LogLevel ConsoleLogLevel = LogLevel.All;
        /// <summary>
        /// Enable output log to file.
        /// </summary>
        private const bool FileLogEnable = true;
        /// <summary>
        /// Enable send log to server.
        /// </summary>
        private const bool ServerLogEnable = true;
        /// <summary>
        /// Enable output log to console.
        /// </summary>
        private const bool ConsoleLogEnable = true;
        #endregion


        private LogMgr()
        {
            MyLog.LogDebugAct = Debug.Log;
            MyLog.LogAct = Debug.Log;
            MyLog.LogErrorAct = Debug.LogError;
            MyLog.LogWarningAct = Debug.LogWarning;

            if (FileLogEnable)
            {
                Logger.AddLog("FileLog", new FileLog(FileOutputLogLevel, LogFilePath, LogFileName));
            }
            if (ServerLogEnable)
            {
                Logger.AddLog("ServerLog", new ServerLog(ServerLogLevel));
            }
            if (ConsoleLogEnable)
            {
                var consoleLog = new ConsoleLog(ConsoleLogLevel);
                GameMain.Instance.OnUpdateAct += consoleLog.Update;
                GameMain.Instance.OnGUIAct += consoleLog.OnGUI;
                Logger.AddLog("ConsoleLog", consoleLog);
            }

            //Application.logMessageReceived += LogCallback;
            Application.logMessageReceivedThreaded += LogCallback;
            GameMain.Instance.OnApplicationQuitAct += Logger.Dispose;


            MyLog.Log($"log path :{LogFilePath}");
        }

        void LogCallback(string condition, string stackTrace, LogType type)
        {
            LogLevel logLevel;
            switch (type)
            {
                case LogType.Log:
                    logLevel = LogLevel.Log;
                    break;
                case LogType.Warning:
                    logLevel = LogLevel.Warning;
                    break;
                case LogType.Exception:
                    logLevel = LogLevel.Error;
                    break;
                case LogType.Error:
                    logLevel = LogLevel.Error;
                    break;
                case LogType.Assert:
                    logLevel = LogLevel.Error;
                    break;
                default:
                    logLevel = LogLevel.Error;
                    break;
            }
            LogData logData = new LogData(logLevel, condition, stackTrace);
            Logger.LogOutput(logData);
        }


    }
}
