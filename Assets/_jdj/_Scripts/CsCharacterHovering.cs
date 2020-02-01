using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsCharacterHovering : MonoBehaviour
{

    [System.Serializable]
    public class HoveringState
    {
        public string name;
        public float speed;
        public float period;
        public float amplitude;
    }



    public List<HoveringState> hoveringStates;


    public float currentSpeed;
    public float currentPeriod;
    public float currentAmplitude;

    public float animationCurrentTime;


    Transform trans;
    private void Awake()
    {
        trans = transform;
        animationCurrentTime = 0.0f;
        SetCurrentHoveringState(hoveringStates[0]);
    }

    void Update()
    {
        trans.localPosition = Vector3.up * Mathf.Sin(animationCurrentTime * currentPeriod) * currentAmplitude;

        animationCurrentTime += Time.deltaTime * currentSpeed;

        //if(Input.GetKeyDown(KeyCode.A))
        //    EventManager.TriggerEvent(new SetCharacterHoveringState_Idle());
    }


    void SetCurrentHoveringState(HoveringState hoveringState) {
        currentSpeed = hoveringState.speed;
        currentPeriod = hoveringState.period;
        currentAmplitude = hoveringState.amplitude;
        animationCurrentTime = 0;
    }

    void SetCharacterHoveringState_Idle(IEvent parame)
    {
        SetCurrentHoveringState(hoveringStates[0]);
    }

    void SetCharacterHoveringState_danger(IEvent parame)
    {
        SetCurrentHoveringState(hoveringStates[1]);
    }



    private void OnEnable()
    {
        EventManager.StartListening(typeof(SetCharacterHoveringState_Idle), SetCharacterHoveringState_Idle);
        EventManager.StartListening(typeof(SetCharacterHoveringState_Idle), SetCharacterHoveringState_danger);
    }

    private void OnDisable()
    {
        EventManager.StopListening(typeof(SetCharacterHoveringState_Idle), SetCharacterHoveringState_Idle);
        EventManager.StopListening(typeof(SetCharacterHoveringState_Idle), SetCharacterHoveringState_danger);
    }
}
