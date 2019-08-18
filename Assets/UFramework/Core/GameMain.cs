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

namespace UFramework
{
    public class GameMain : MonoSingleton<GameMain>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            Init();
        }

        void Init()
        {
            //Log Initialization.
            var logMgr = LogMgr.Instance;
        }

        #region Life cycle event
        public Action OnUpdateAct;
        public Action OnFixedUpdateAct;
        public Action OnLatedUpdateAct;
        public Action OnGUIAct;
        public Action OnDestroyAct;
        public Action OnApplicationQuitAct;

        void Update()
        {
            OnUpdateAct?.Invoke();
        }

        void FixedUpdate()
        {
            OnFixedUpdateAct?.Invoke();
        }

        void LatedUpdate()
        {
            OnLatedUpdateAct?.Invoke();
        }

        void OnGUI()
        {
            OnGUIAct?.Invoke();
        }

        void OnApplicationQuit()
        {
            OnApplicationQuitAct?.Invoke();
            ClearSingleton();
        }
        #endregion
    }
}