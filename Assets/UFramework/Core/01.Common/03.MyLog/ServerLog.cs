/****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2019 xiaojingli
 * Copyright (c) 2019 renjunyi
 * 
 * https://github.com/XRFramework/XRFramework
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

namespace XRFramework
{
    public class ServerLog : ILog
    {
        public LogLevel LogLevel { get; set; }

        public ServerLog(LogLevel logLevel)
        {
            LogLevel = logLevel;
        }

        public void LogOutput(LogData logData)
        {
            if (LogLevel >= logData.logLevel)
            {
                switch (logData.logLevel)
                {
                    case LogLevel.Log:
                        SendToServer(logData.logLevel, $"Log: {logData.condition}");
                        break;
                    case LogLevel.Warning:
                        SendToServer(logData.logLevel, $"Warning: {logData.condition}\r\n{logData.stackTrace}");
                        break;
                    case LogLevel.Error:
                        SendToServer(logData.logLevel, $"Error: {logData.condition}\r\n{logData.stackTrace}");
                        break;
                    default:
                        break;
                }
            }
        }

        //TODO Send log to Server.
        void SendToServer(LogLevel logLevel, string format)
        {
        }

        public void Dispose()
        {

        }


    }
}
