using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ButtonInfoPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static float MAX_TIME = 0.1f;    // Onboarding.cs sets time after onboarding is complete, keep as 0.1f for onboarding
    private float timer;
    bool isTimerTicking = false;

    private void Start() {
        timer = MAX_TIME;
    }

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
