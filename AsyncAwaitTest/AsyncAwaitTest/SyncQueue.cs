using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncAwaitTest 
{
    public class SyncQueue<T>
    {
        private Queue<T> queue = new Queue<T>();
        private object _object = new object();

        public void Enqueue(T item)
        {
            lock (_object)
            {
                queue.Enqueue(item);
            }
        }

        public bool TryGetItem(out T item)
        {
            lock (_object)
            {
                if (queue.Count > 0)
                {
                    item = queue.Dequeue();
                    return true;
                }
            }

            item = default(T);
            return false;
        }

        public int Size()
        {
            var result = 0;
            lock (_object)
            {
                result = queue.Count;
            }

            return result;
        }

    }
}
