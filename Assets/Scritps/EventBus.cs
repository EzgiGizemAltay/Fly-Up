using System.Collections.Generic;

public static class EventBus<TEvent> 
{
    private static readonly List<Listener<TEvent>> _listeners = new List<Listener<TEvent>>();
    
    public static void AddListener(Listener<TEvent> listener) 
    {
        _listeners.Add(listener);
    }
    
    public static void RemoveListener(Listener<TEvent> listener) 
    {
        _listeners.Remove(listener);
    }
    
    public static void Emit(TEvent e) 
    {
        for(var i = _listeners.Count - 1; i >= 0; i--) 
        {
            _listeners[i]?.Invoke(e);
        }
    }
}