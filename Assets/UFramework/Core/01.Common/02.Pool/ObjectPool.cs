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
using System.Collections.Generic;

namespace UFramework
{
    public class ObjectPool<T> : IPool<T>
    {
        #region Fields
        /// <summary>
        /// Default capacity of the object pool.
        /// </summary>
        private const int DefaultCapacity = int.MaxValue;

        /// <summary>
        /// Max capacity of Object pool.
        /// </summary>
        private int mMaxCount;
        /// <summary>
        /// Function that create an object.
        /// </summary>
        private readonly Func<T> mCreateFunc;
        /// <summary>
        /// Event that successfully get an object.
        /// </summary>
        private readonly Action<T> mGetAct;
        /// <summary>
        /// Event that successfully release an object.
        /// </summary>
        private readonly Action<T> mReleaseAct;
        /// <summary>
        /// Object pool container.
        /// </summary>
        private readonly Stack<T> mStack = new Stack<T>();
        #endregion

        #region Properties
        /// <summary>
        /// Number of current objects of object pool.
        /// </summary>
        public int CurCount
        {
            get { return mStack.Count; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construct Function.
        /// </summary>
        /// <param name="createFunc">Function that create an object.</param>
        /// <param name="getAct">Event that get an object.</param>
        /// <param name="releaseAct">Event that release an object.</param>
        public ObjectPool(Func<T> createFunc, Action<T> getAct, Action<T> releaseAct) : this(DefaultCapacity, createFunc, getAct, releaseAct)
        {

        }

        /// <summary>
        /// Construct Function.
        /// </summary>
        /// <param name="maxCount">Max capacity.</param>
        /// <param name="createFunc">Function that create an object.</param>
        /// <param name="getAct">Event that get an object.</param>
        /// <param name="releaseAct">Event that release an object.</param>
        public ObjectPool(int maxCount, Func<T> createFunc, Action<T> getAct, Action<T> releaseAct)
        {
            mMaxCount = maxCount;
            mCreateFunc = createFunc;
            mGetAct = getAct;
            mReleaseAct = releaseAct;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get an object.
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T element = mStack.Count > 0 ? mStack.Pop() : mCreateFunc();
            mGetAct?.Invoke(element);
            return element;
        }

        /// <summary>
        /// Release an object.
        /// </summary>
        /// <param name="element"></param>
        /// <returns>true:success, false:fail</returns>
        public bool Release(T element)
        {
            //Check stack capacity is enough?
            //Check target element is the same as stack top element?
            if (mStack.Count > 0 && ReferenceEquals(mStack.Peek(), element))
            {
                return false;
            }

            //Check stack capacity.
            if (mStack.Count >= mMaxCount)
            {
                return false;
            }

            mReleaseAct?.Invoke(element);
            mStack.Push(element);
            return true;
        }
        #endregion
    }
}
