﻿/****************************************************************************
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
using System.Reflection;

namespace XRFramework
{
    /// <summary>
    /// Singleton template.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>
    {
        #region Fields
        /// <summary>
        /// The instance.
        /// </summary>
        private static T mInstance;

        /// <summary>
        /// The thread lock.
        /// </summary>
        private static object mLocker = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Get the instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (mLocker)
                    {
                        if (mInstance == null)
                        {
                            //Get the private constructor.
                            ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

                            //Get the no-argument constructor.
                            ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

                            if (ctor == null)
                            {
                                throw new Exception("Non-Public Constructor() not found! in " + typeof(T));
                            }

                            // Create instance by constructor.
                            mInstance = ctor.Invoke(null) as T;
                        }
                    }
                }
                return mInstance;
            }
        }
        #endregion

        #region Constructors
        protected Singleton()
        {

        }
        #endregion

        #region Methods
        public virtual void ClearSingleton()
        {
            mInstance = null;
        }
        #endregion
    }
}
