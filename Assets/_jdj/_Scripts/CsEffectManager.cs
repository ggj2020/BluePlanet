using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsEffectManager : MonoBehaviour
{
    public ParticleSystem psHobering;
    public ParticleSystem psGetItem;


    ParticleSystem.EmissionModule emHobering;


    public enum EffectType
    {
        Hovering,
        GetItem,
    }



    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.A))
    //        EventManager.TriggerEvent(new SetEffect(EffectType.GetItem, Vector3.zero));
    //}

    void RunEffect(IEvent param)
    {
        var _param = (SetEffect)param;


        switch (_param.effectType) {
            case EffectType.Hovering:
                emHobering = psHobering.emission;
                emHobering.enabled = !emHobering.enabled;
                break;
            case EffectType.GetItem:
                Instantiate(psGetItem, _param.position, Quaternion.identity);
                break;
        }

    }


    private void OnEnable()
    {
        EventManager.StartListening(typeof(SetEffect), RunEffect);
        
    }

    private void OnDisable()
    {
        EventManager.StopListening(typeof(SetEffect), RunEffect);
    }
}
