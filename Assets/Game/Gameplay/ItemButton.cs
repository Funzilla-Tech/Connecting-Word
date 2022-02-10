using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
 
// Button that raises onDown event when OnPointerDown is called.
[AddComponentMenu("Aeronauts/AeButton")]
public class ItemButton : Button
{
    // Event delegate triggered on mouse or touch down.
    [SerializeField]
    ButtonDownEvent _onDown = new ButtonDownEvent();
 
    protected ItemButton() { }
 
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
 
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
 
        _onDown.Invoke();
    }
 
    public ButtonDownEvent onDown
    {
        get { return _onDown; }
        set { _onDown = value; }
    }
 
    [Serializable]
    public class ButtonDownEvent : UnityEvent { }
}