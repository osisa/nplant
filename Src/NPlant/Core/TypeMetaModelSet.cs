// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="TypeMetaModelSet.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace NPlant.Core
{
    public class TypeMetaModelSet
    {
        #region Fields

        private readonly IDictionary<Type, TypeMetaModel> _dictionary = new Dictionary<Type, TypeMetaModel>();

        #endregion

        #region Public Indexers

        public TypeMetaModel this[Type type]
        {
            get
            {
                if (_dictionary.TryGetValue(type, out var model))
                    return model;

                model = new TypeMetaModel(type);
                _dictionary.Add(type, model);

                return model;
            }
            set => _dictionary[type] = value;
        }

        #endregion
    }
}