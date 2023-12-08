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

    void Start()
    {
        GameManager.Instance.UpdateButtonInteractability();
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
                GameManager.Instance.isInOnboarding = false;
                GameManager.Instance.UpdateButtonInteractability();
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
