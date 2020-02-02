using System;
using UnityEngine.Events;
using System.Collections.Generic;

public interface IEvent{}
public struct ShakePuzzleEvent : IEvent {}
public struct GeneratePuzzleEvent : IEvent {}
public struct TryJumpEvent : IEvent {}
public struct GameOverEvent : IEvent {}



//My!!!
public struct ShakeCamera : IEvent { }
public struct SetCharacterHoveringState_Idle : IEvent { }
public struct SetCharacterHoveringState_danger : IEvent { }

public struct SetEffect : IEvent {
    public SetEffect(CsEffectManager.EffectType effectType, UnityEngine.Vector3 position) {
        this.effectType = effectType;
        this.position = position;
    }
    public CsEffectManager.EffectType effectType;
    public UnityEngine.Vector3 position;
}



public struct GarbageAcquireEvent : IEvent 
{
    int garbageIdx;
    public GarbageAcquireEvent(int garbageIdx) { this.garbageIdx = garbageIdx; }
    public int GetGarbageIdx()
    {
        return garbageIdx;
    }
}
public struct JumpEvent : IEvent {}

public class EventHandler: UnityEvent<IEvent> {}

public class EventManager
{
    static private Dictionary<Type, EventHandler> eventDictionary = new Dictionary<Type, EventHandler>();

    static public void StartListening(Type type, UnityAction<IEvent> listener)
    {
        EventHandler handler = null;
        if (eventDictionary.TryGetValue(type, out handler))
        {
            handler.AddListener(listener);
        }
        else
        {
            handler = new EventHandler();
            handler.AddListener(listener);
            eventDictionary.Add(type, handler);
        }            
    }

    static public void StopListening(Type type, UnityAction<IEvent> listener)
    {
        EventHandler handler = null;
        if (eventDictionary.TryGetValue(type, out handler))
        {
            handler.RemoveListener(listener);
        }
    }

    static public void TriggerEvent(IEvent eventObject)
    {
        EventHandler handler = null;
        if (eventDictionary.TryGetValue(eventObject.GetType(), out handler))
        {
            handler.Invoke(eventObject);
        }
    }
}