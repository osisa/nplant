// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="KeyedList.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace NPlant.Core
{
    public class KeyedList<T>
        where T : class, IKeyedItem
    {
        #region Public Properties

        public int Count => InnerList.Count;

        #endregion

        #region Properties

        internal IList<T> InnerList { get; } = new List<T>();

        #endregion

        #region Public Indexers

        public T this[int index, bool throwOnNotFound = true]
        {
            get
            {
                if (this.TryGetValueByIndex(index, out var item))
                    return item;

                if (throwOnNotFound)
                    throw new NPlantException("Failed to find item at index of {0} in the list of {1} instances".FormatWith(index, typeof(T).FullName));

                return default;
            }
        }

        public T this[string key, bool throwOnNotFound = true]
        {
            get
            {
                if (this.TryGetValue(key, out var item))
                    return item;

                if (throwOnNotFound)
                    throw new NPlantException("Failed to find item of key '{0}' in the list of {1} instances".FormatWith(key, typeof(T).FullName));

                return default;
            }
            set => this.Add(value);
        }

        #endregion

        #region Public Methods and Operators

        public void Add(T item)
        {
            item.CheckForNullArg("item");

            var existingItem = FindItem(item.Key);

            if (existingItem != null)
                InnerList.Remove(existingItem);

            InnerList.Add(item);
        }

        public void AddRange(T item, params T[] others)
        {
            item.CheckForNullArg("item");

            this.Add(item);

            if (others != null)
                this.AddRange(others);
        }

        public void AddRange(IEnumerable<T> items)
        {
            items.CheckForNullArg("items");

            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public bool ContainsKey(string key)
        {
            return InnerList.Any(innerItem => key == innerItem.Key);
        }

        public bool TryGetValue(string key, out T item)
        {
            item = this.FindItem(key);

            return item != null;
        }

        public bool TryGetValueByIndex(int index, out T item)
        {
            item = null;

            if (index.IsWithin(0, InnerList.Count - 1))
            {
                item = InnerList[index];
            }

            return item != null;
        }

        #endregion

        #region Methods

        private T FindItem(string key)
        {
            return InnerList.FirstOrDefault(innerItem => key == innerItem.Key);
        }

        #endregion
    }

    public interface IKeyedItem
    {
        #region Public Properties

        string Key { get; }

        #endregion
    }
}