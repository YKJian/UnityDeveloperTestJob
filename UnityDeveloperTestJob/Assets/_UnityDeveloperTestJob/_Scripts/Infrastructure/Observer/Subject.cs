using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Observer
{
    public abstract class Subject : MonoBehaviour
    {
        private HashSet<IBaseObserver> m_observers = new HashSet<IBaseObserver>();

        public void AddObserver(IBaseObserver observer) =>
            m_observers.Add(observer);

        public void RemoveObserver(IBaseObserver observer) =>
             m_observers.Remove(observer);

        public void Notify()
        {
            NotifyObservers<object>();
        }

        public void Notify<T>(T eventArgs)
        {
            NotifyObservers(eventArgs);
        }

        private void NotifyObservers<T>(T eventArgs = default)
        {
            foreach (IBaseObserver observer in m_observers.ToArray())
            {
                switch (observer)
                {
                    case ISimpleObserver simpleObserver: 
                        simpleObserver.OnNotify(); 
                        break;
                    case IRequiringExternalDataObserver requiringObserver: 
                        requiringObserver.OnNotify(eventArgs);
                        break;
                }
            }
        }
    }
}
