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

using UnityEngine;
using System.Collections.Generic;

namespace XRFramework
{
    public class ConsoleLog : ILog
    {
        #region Fields
        /// <summary>
        /// Show console.
        /// Press F1 key or at least three fingers on the screen.
        /// </summary>
        private bool mVisible = false;
        /// <summary>
        /// Log datas.
        /// </summary>
        private List<LogData> mLogDataList = new List<LogData>();
        #endregion

        #region OnGUI
        private bool mCollapse;
        private Vector2 mScrollPos;
        private bool mScrollToBottom;
        private const int UIFontSize = 40;
        private const int LogFontSize = 30;
        private const int Margin = 20;
        private bool mStackTrace = false;
        //Half of screen
        private Rect mWindowRect = new Rect(Margin, Margin, Screen.width * 0.5f - 2 * Margin, Screen.height - 2 * Margin);
        //Full screen
        //Rect mWindowRect = new Rect(margin, margin, Screen.width - 2 * margin, Screen.height - 2 * margin);
        private Dictionary<LogLevel, bool> mTogDic = new Dictionary<LogLevel, bool>()
        {
            { LogLevel.Log, true },
            { LogLevel.Warning, true },
            { LogLevel.Error, true }
        };
        private static readonly Dictionary<LogLevel, Color> LogLevelColor = new Dictionary<LogLevel, Color>() {
            { LogLevel.Log, Color.green },
            { LogLevel.Warning, Color.yellow },
            { LogLevel.Error, Color.red }
        };
        #endregion

        public LogLevel LogLevel { get; set; }

        public ConsoleLog(LogLevel logLevel)
        {
            LogLevel = logLevel;
        }

        #region Methods
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1) || Input.touchCount >= 3)
            {
                mVisible = !mVisible;
            }
        }
        public void OnGUI()
        {
            if (!mVisible) return;

            mWindowRect = GUILayout.Window(0, mWindowRect, ConsoleWindow, "Console");
        }

        void ConsoleWindow(int windowID)
        {
            GUILayout.BeginHorizontal();
            GUI.color = Color.magenta;
            if (GUILayout.Button(FormatFontSize("Clear", UIFontSize)))
            {
                mLogDataList.Clear();
            }

            mCollapse = GUILayout.Toggle(mCollapse, FormatFontSize("Collapse", UIFontSize));
            mScrollToBottom = GUILayout.Toggle(mScrollToBottom, FormatFontSize("ScrollToBottom", UIFontSize));
            mStackTrace = GUILayout.Toggle(mStackTrace, FormatFontSize("Stack", UIFontSize));
            GUILayout.FlexibleSpace();

            GUI.color = Color.white;
            GUI.contentColor = LogLevelColor[LogLevel.Log];
            mTogDic[LogLevel.Log] = GUILayout.Toggle(mTogDic[LogLevel.Log], FormatFontSize("Log", UIFontSize));
            GUI.contentColor = LogLevelColor[LogLevel.Warning];
            mTogDic[LogLevel.Warning] = GUILayout.Toggle(mTogDic[LogLevel.Warning], FormatFontSize("Warning", UIFontSize));
            GUI.contentColor = LogLevelColor[LogLevel.Error];
            mTogDic[LogLevel.Error] = GUILayout.Toggle(mTogDic[LogLevel.Error], FormatFontSize("Error", UIFontSize));
            GUILayout.EndHorizontal();

            if (mScrollToBottom)
            {
                GUILayout.BeginScrollView(Vector2.up * mLogDataList.Count * 10000.0f);
            }
            else
            {
                mScrollPos = GUILayout.BeginScrollView(mScrollPos);
            }
            for (int i = 0; i < mLogDataList.Count; i++)
            {
                //If it is the same as the previous log, it collapses
                if (mCollapse && i > 0
                    && mLogDataList[i].logLevel == mLogDataList[i - 1].logLevel
                    && mLogDataList[i].condition == mLogDataList[i - 1].condition
                    && mLogDataList[i].stackTrace == mLogDataList[i - 1].stackTrace
                )
                {
                    continue;
                }

                bool isPrint = false;
                if (mTogDic.TryGetValue(mLogDataList[i].logLevel, out isPrint) && isPrint)
                {
                    GUI.contentColor = LogLevelColor[mLogDataList[i].logLevel];
                    switch (mLogDataList[i].logLevel)
                    {
                        case LogLevel.Error:
                        case LogLevel.Warning:
                            if (mStackTrace)
                            {
                                GUILayout.Label(FormatFontSize($"{mLogDataList[i].condition}\r\n{mLogDataList[i].stackTrace}", LogFontSize));
                            }
                            else
                            {
                                GUILayout.Label(FormatFontSize(mLogDataList[i].condition, LogFontSize));
                            }
                            break;
                        default:
                            GUILayout.Label(FormatFontSize(mLogDataList[i].condition, LogFontSize));
                            break;
                    }
                }

            }
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        private string FormatFontSize(string str, int fontSize)
        {
            return $"<size={fontSize}>{str}</size>";
        }

        public void LogOutput(LogData logData)
        {
            mLogDataList.Add(logData);
        }

        public void Dispose()
        {

        }
        #endregion
    }
}
