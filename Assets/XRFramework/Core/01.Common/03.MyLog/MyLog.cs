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

using System;

namespace XRFramework
{
    public struct MyLog
    {
        public static Action<string> LogDebugAct;

        public static Action<string> LogAct;

        public static Action<string> LogWarningAct;

        public static Action<string> LogErrorAct;

        public static void LogDebug(string format)
        {
            LogDebugAct(format);
        }

        public static void Log(string format)
        {
            LogAct(format);
        }

        public static void LogWarning(string format)
        {
            LogWarningAct(format);
        }

        public static void LogError(string format)
        {
            LogErrorAct(format);
        }
    }
}
