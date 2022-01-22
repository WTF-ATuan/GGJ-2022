using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenu_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool IsEnter;
    Vector2 Start_Pot;

    void Start()
    {
        Start_Pot = transform.localPosition;
    }

    void Update()
    {
        if (IsEnter)
        {
            transform.localPosition = Start_Pot + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsEnter = false;
    }
}
