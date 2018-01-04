using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class ThreadSafeHashSet<T> : IEnumerable<T>, ISet<T>, ICollection<T>
    {
        //https://pastebin.com/VhSPaBBr

        private HashSet<T> hashSet;
        private object mutex = new object();
        //public object Mutex { get { return this.mutex; } } //??

        public ThreadSafeHashSet()
        {
            this.hashSet = new HashSet<T>();
        }

        public ThreadSafeHashSet(IEnumerable<T> collection)
        {
            this.hashSet = new HashSet<T>(collection);
        }

        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator()
        {
            HashSet<T> clone;

            lock (this.mutex)
            {
                clone = new HashSet<T>(this.hashSet);
            }

            return clone.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion IEnumerable<T>

        #region ISet<T>

        public bool Add(T item)
        {
            bool elementWasAdded;

            lock (this.mutex)
            {
                elementWasAdded = this.hashSet.Add(item);
            }

            return elementWasAdded;
        }

        public bool Remove(T item)
        {
            bool foundAndRemoved;

            lock (this.mutex)
            {
                foundAndRemoved = this.hashSet.Remove(item);
            }

            return foundAndRemoved;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            lock (this.mutex)
            {
                this.hashSet.ExceptWith(other);
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            lock (this.mutex)
            {
                this.hashSet.IntersectWith(other);
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            bool isProperSubsetOf = this.hashSet.IsProperSubsetOf(other);
            return isProperSubsetOf;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            bool isProperSupersetOf = this.hashSet.IsProperSupersetOf(other);
            return isProperSupersetOf;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            bool isSubsetOf = this.hashSet.IsSubsetOf(other);
            return isSubsetOf;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            bool isSupersetOf = this.hashSet.IsSupersetOf(other);
            return isSupersetOf;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            bool overlaps = this.hashSet.Overlaps(other);
            return overlaps;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            bool setsAreEqual = this.hashSet.Overlaps(other);
            return setsAreEqual;
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            lock (this.mutex)
            {
                this.hashSet.SymmetricExceptWith(other);
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            lock (this.mutex)
            {
                this.hashSet.UnionWith(other);
            }
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        public void Clear()
        {
            lock (this.mutex)
            {
                this.hashSet.Clear();
            }
        }

        public bool Contains(T item)
        {
            bool contains = this.hashSet.Contains(item);
            return contains;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int i = 0;

            foreach (T item in this)
            {
                array[arrayIndex + i] = item;
                i++;
            }
        }

        public int Count
        {
            get { return this.hashSet.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ISet<T>)(this.hashSet)).IsReadOnly; }
        }

        #endregion ISet<T>

        public int RemoveWhere(Predicate<T> predicate)
        {
            HashSet<T> toBeRemoved = new HashSet<T>();

            foreach (T item in this) // GetEnumerator() is thread safe
            {
                if (predicate(item))
                {
                    toBeRemoved.Add(item);
                }
            }

            int foundAndRemovedCount = 0;

            lock (this.mutex)
            {
                foreach (T item in toBeRemoved)
                {
                    bool foundAndRemoved = this.hashSet.Remove(item);

                    if (foundAndRemoved)
                    {
                        foundAndRemovedCount++;
                    }
                }
            }

            return foundAndRemovedCount;
        }

    }
}
