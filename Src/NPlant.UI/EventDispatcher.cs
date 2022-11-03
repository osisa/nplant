// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="EventDispatcher.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace NPlant.UI
{
    public static class EventDispatcher
    {
        #region Static Fields

        [ThreadStatic]
        private static List<Delegate> _actions;

        #endregion

        #region Public Methods and Operators

        public static int Raise<T>(T @event)
        {
            var count = 0;

            if (_actions != null)
            {
                foreach (var action in _actions.OfType<Action<T>>())
                {
                    action(@event);
                    count++;
                }
            }

            return count;
        }

        public static IDisposable Register<T>(Action<T> action)
        {
            action.CheckForNullArg("action");

            _actions ??= new List<Delegate>();
            _actions.Add(action);

            return new EventDispatcherRegistration<T>(action);
        }

        #endregion

        #region Methods

        private static void UnRegister<T>(Action<T> action)
        {
            _actions?.Remove(action);
        }

        #endregion

        internal class EventDispatcherRegistration<T> : IDisposable
        {
            #region Fields

            private readonly Action<T> _action;

            #endregion

            #region Constructors and Destructors

            public EventDispatcherRegistration(Action<T> action)
            {
                _action = action;
            }

            #endregion

            #region Public Methods and Operators

            public void Dispose()
            {
                UnRegister(_action);
            }

            #endregion
        }
    }
}