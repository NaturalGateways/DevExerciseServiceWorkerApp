using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.UI
{
    public interface IModelListener
    {
        /// <summary>Called when the data has cchanged.</summary>
        void OnDataChanged(Model model);
    }

    public class Model
    {
        /// <summary>The list of listeners.</summary>
        private List<WeakReference<IModelListener>> m_listenerList = new List<WeakReference<IModelListener>>();

        /// <summary>Adds a listener to the model.</summary>
        public void AddListener(IModelListener listener)
        {
            lock (m_listenerList)
            {
                for (int listenerIndex = 0; listenerIndex != m_listenerList.Count; ++listenerIndex)
                {
                    IModelListener testTarget = null;
                    if (m_listenerList[listenerIndex].TryGetTarget(out testTarget) == false)
                    {
                        m_listenerList[listenerIndex] = new WeakReference<IModelListener>(listener);
                        return;
                    }
                }
                m_listenerList.Add(new WeakReference<IModelListener>(listener));
            }
        }

        /// <summary>Removes a listener from the model.</summary>
        public void RemoveListener(IModelListener listener)
        {
            lock (m_listenerList)
            {
                for (int listenerIndex = 0; listenerIndex != m_listenerList.Count; ++listenerIndex)
                {
                    IModelListener testTarget = null;
                    if (m_listenerList[listenerIndex].TryGetTarget(out testTarget))
                    {
                        if (object.ReferenceEquals(listener, testTarget))
                        {
                            m_listenerList.RemoveAt(listenerIndex);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>Called when something changes.</summary>
        public void OnDataChanged()
        {
#if DEBUG
            List<IModelListener> debugListenerList = new List<IModelListener>();
            lock (m_listenerList)
            {
                for (int listenerIndex = 0; listenerIndex != m_listenerList.Count; ++listenerIndex)
                {
                    IModelListener testTarget = null;
                    if (m_listenerList[listenerIndex].TryGetTarget(out testTarget))
                    {
                        debugListenerList.Add(testTarget);
                    }
                }
            }
#endif

            lock (m_listenerList)
            {
                for (int listenerIndex = 0; listenerIndex != m_listenerList.Count; ++listenerIndex)
                {
                    IModelListener testTarget = null;
                    if (m_listenerList[listenerIndex].TryGetTarget(out testTarget))
                    {
                        testTarget.OnDataChanged(this);
                    }
                }
            }
        }
    }
}
