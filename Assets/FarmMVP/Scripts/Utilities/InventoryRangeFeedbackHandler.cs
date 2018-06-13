using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
[RequireComponent(typeof(Devdog.General.TriggerBase))]
public class InventoryRangeFeedbackHandler : MonoBehaviour {


    private Devdog.General.TriggerBase _rangeHandler;

    public UnityEvent onRangeEnterEvent;
    public UnityEvent onRangeExitEvent;
    public UnityEvent onActionButtonEvent;

    private bool onEnterFired;
    private bool onExitFired;

    private void Awake()
    {
        _rangeHandler = GetComponent<Devdog.General.TriggerBase>();
    }
	
	// Update is called once per frame
	void Update () {

        if(_rangeHandler.inRange && !onEnterFired)
        {
            onRangeEnterEvent.Invoke();
            onEnterFired = true;
            onExitFired = false;
        }

        if(!_rangeHandler.inRange && !onExitFired)
        {
            onRangeExitEvent.Invoke();
            onExitFired = true;
            onEnterFired = false;
        }

        if(onActionButtonEvent != null)
        {
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                onActionButtonEvent.Invoke();
            }
        }
		
	}
}
