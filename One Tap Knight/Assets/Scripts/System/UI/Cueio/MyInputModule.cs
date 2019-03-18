using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System;

public class MyInputModule : StandaloneInputModule
{
    public static ExecuteEvents.EventFunction<IDeleteHandler> deleteHandler = Execute;
    private static void Execute(IDeleteHandler handler, BaseEventData eventData)
    {
        handler.OnDelete(eventData);
    }

    [SerializeField]
    private string m_DeleteButton = "Delete";

    public string DeleteButton
    {
        get { return m_DeleteButton; }
        set { m_DeleteButton = value; }
    }

    public override void Process()
    {
        bool usedEvent = SendUpdateEventToSelectedObject();

        if (eventSystem.sendNavigationEvents)
        {
            if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();

            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        }

        //ProcessMouseEvent();
    }

    protected new bool SendSubmitEventToSelectedObject()
    {
        var data = GetBaseEventData();
        if (eventSystem.currentSelectedGameObject == null)
            return false;
        //if (Input.GetButtonDown(m_DeleteButton))
        //    ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, deleteHandler);
        base.SendSubmitEventToSelectedObject();
        return data.used;
    }
}

public interface IDeleteHandler : IEventSystemHandler
{
    void OnDelete(BaseEventData eventData);
}

