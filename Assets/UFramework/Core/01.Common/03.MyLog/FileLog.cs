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

using System;
using System.IO;
using System.Text;

namespace UFramework
{
    public class FileLog : ILog
    {
        private object mLock = new object();
        private StreamWriter mStreamWriter;

        public LogLevel LogLevel { get; set; }

        public FileLog(LogLevel logLevel, string filePath, string fileName)
        {
            LogLevel = logLevel;

            //Save last log to lastlog.log file.
            try
            {
                FileInfo fileinfo = new FileInfo(filePath + fileName);
                if (fileinfo.Exists)
                {
                    string lastPath = $"{filePath}lastlog.log";
                    File.Delete(lastPath);
                    fileinfo.MoveTo(lastPath);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Move log error!", e);
            }

            //Create log file.
            string path = filePath + fileName;
            string directoryName = Path.GetDirectoryName(path);
            try
            {
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                mStreamWriter = new StreamWriter(path, false, Encoding.UTF8);
            }
            catch (Exception)
            {
                mStreamWriter = new StreamWriter(path + DateTime.Now.ToString("yyyyMMdd HH_mm_ss") + ".log", false, Encoding.UTF8);
            }
        }

        public void LogOutput(LogData logData)
        {
            if (LogLevel >= logData.logLevel)
            {
                switch (logData.logLevel)
                {
                    case LogLevel.Log:
                        OutputToFile($"Log: {logData.condition}");
                        break;
                    case LogLevel.Warning:
                        OutputToFile($"Warning: {logData.condition}\r\n{logData.stackTrace}");
                        break;
                    case LogLevel.Error:
                        OutputToFile($"Error: {logData.condition}\r\n{logData.stackTrace}");
                        break;
                    default:
                        break;
                }
            }
        }

        void OutputToFile(string format)
        {
            lock (mLock)
            {
                if (mStreamWriter != null)
                {
                    mStreamWriter.Write($"[{DateTime.Now}] ");
                    mStreamWriter.WriteLine(format);
                    mStreamWriter.Flush();
                }
            }
        }

        public void Dispose()
        {
            if (mStreamWriter != null)
            {
                mStreamWriter.Dispose();
                mStreamWriter = null;
            }
        }
    }
}