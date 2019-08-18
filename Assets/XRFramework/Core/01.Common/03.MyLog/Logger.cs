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

using System.Collections.Generic;

namespace XRFramework
{
    public static class Logger
    {
        private static Dictionary<string, ILog> logDic = new Dictionary<string, ILog>();

        public static void AddLog(string name, ILog log)
        {
            logDic.Add(name, log);
        }

        public static void LogOutput(LogData logData)
        {
            foreach (var log in logDic)
            {
                log.Value.LogOutput(logData);
            }
        }

        public static void Dispose()
        {
            foreach (var log in logDic)
            {
                log.Value.Dispose();
            }
        }
    }
}
