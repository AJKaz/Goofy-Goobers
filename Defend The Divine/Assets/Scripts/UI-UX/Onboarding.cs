using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Onboarding : MonoBehaviour, IPointerClickHandler
{
    private int stateVal = 0;
    [SerializeField] GameObject tutorialText;
    [SerializeField] List<GameObject> arrows = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        stateVal++;

        switch (stateVal)
        {
            case 0:
                break;
            case 1:
                arrows[stateVal].SetActive(true);
                arrows[stateVal-1].SetActive(false);
                break;
            case 2:
                arrows[stateVal].SetActive(true);
                arrows[stateVal-1].SetActive(false);
                break;
            case 3:
                arrows[stateVal].SetActive(true);
                arrows[stateVal-1].SetActive(false);
                break;
            case 4:
                arrows[stateVal].SetActive(true);
                arrows[stateVal-1].SetActive(false);
                break;
            case 5:
                arrows[stateVal-1].SetActive(false);
                Time.timeScale = 1;
                break;
            default:
                break;
        }
    }
}
