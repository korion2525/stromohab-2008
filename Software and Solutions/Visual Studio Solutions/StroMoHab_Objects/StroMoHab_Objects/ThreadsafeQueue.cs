using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StroMoHab_Objects
{
    /// <summary>
    /// A Thread Safe non-blocking queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadsafeQueue<T> : IDisposable
    {
        private Queue<T> _queue;
        private bool _isBeingAccessed = false;
        private int MAX_NUMBER_OF_ITEMS_IN_QUEUE;

        #region Constructors

        /// <summary>
        /// Initialises a new, empty instance of the StroMoHab.Objects.ThreadsafeQueue.
        /// </summary>
        /// <param name="capacity">The maximum number of items the queue can contain.</param>
        public ThreadsafeQueue(int capacity)
        {
            MAX_NUMBER_OF_ITEMS_IN_QUEUE = capacity;
            _queue = new Queue<T>(MAX_NUMBER_OF_ITEMS_IN_QUEUE);
        }

        #endregion Constructors

        /// <summary>
        /// Adds data to the queue.
        /// </summary>
        /// <param name="data">The data to add the the queue</param>
        /// <returns>true if operation is successful, else returns false</returns>
        /// <exception cref="ArgumentNullException">If data to be enqueued is null</exception>
        public bool Enqueue(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            //if not accessing the queue
            if (_isBeingAccessed == false)
            {
                //lock access to the queue
                _isBeingAccessed = true;

                //if queue is not full
                if (_queue.Count < MAX_NUMBER_OF_ITEMS_IN_QUEUE)
                {
                    _isBeingAccessed = true;
                    //add data to the queue
                    _queue.Enqueue(data);
                    //release lock as no longer accessing queue
                    _isBeingAccessed = false;
                    //return succesfull operation
                    return true;
                }
                else
                {
                    //return failed operation
                    return false;
                }
            }
            else //queue is being accessed
            {
                //return failed operation
                return false;
            }
        }

        public T Dequeue()
        {
            //if queue is not being accessed
            if (_isBeingAccessed == false)
            {
                //lock access to the queue
                _isBeingAccessed = true;

                //if queue is not empty
                if (_queue.Count > 0)
                {
                    //dequeue data object
                    T dequeuedT = _queue.Dequeue();
                    //release lock
                    _isBeingAccessed = false;
                    //return data
                    return (dequeuedT);
                }
                else //queue is empty
                {
                    _isBeingAccessed = false;
                    return (default(T));
                }
            }
            else //queue is being accessed
            {
                //return failed operation
                return (default(T));
            }
        }

        void IDisposable.Dispose()
        {
            _isBeingAccessed = false;
        }

    }
}
