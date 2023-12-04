using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ButtonInfoPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float MAX_TIME = 0.25f;
    private float timer = MAX_TIME;
    bool isTimerTicking = false;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isTimerTicking = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isTimerTicking = false;
    }


    // Update is called once per frame
    void Update()
    {
        GameObject child = gameObject.transform.GetChild(1).gameObject;


        if (isTimerTicking)
        {

            if (timer <= 0)
            {
                child.SetActive(true);
                timer = MAX_TIME;
            }
            timer -= Time.deltaTime;
        }
        else
        {
            timer = MAX_TIME;
            child.SetActive(false);
        }

    }
}
