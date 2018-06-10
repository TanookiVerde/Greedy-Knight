using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopPlayerAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    private Character player;

	void Start()
	{		
        player = FindObjectOfType<Character>();
	}
    public void OnPointerEnter(PointerEventData eventData)
    {
        player.SetOverPause(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        player.SetOverPause(false);
    }
	void OnDisable()
	{
        player.SetOverPause(false);		
	}
}
