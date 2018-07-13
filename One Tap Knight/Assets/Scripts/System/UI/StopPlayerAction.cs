using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopPlayerAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData)
    {
        var character = FindObjectOfType<Character>();
        if (character != null)
            FindObjectOfType<Character>().SetOverPause(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        var character = FindObjectOfType<Character>();
        if (character != null)
            FindObjectOfType<Character>().SetOverPause(false);
    }
	void OnDisable()
	{
        var character = FindObjectOfType<Character>();
        if(character != null)
            FindObjectOfType<Character>().SetOverPause(false);		
	}
}
