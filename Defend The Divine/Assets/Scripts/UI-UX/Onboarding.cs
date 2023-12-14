using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        if (stateVal == 0 || stateVal > arrows.Count) return;

        if (stateVal == arrows.Count) {
            arrows[stateVal - 1].SetActive(false);
            GameManager.Instance.isInOnboarding = false;
            GameManager.Instance.UpdateButtonInteractability();
            gameObject.SetActive(false);
            ButtonInfoPopup.MAX_TIME = 0.75f;
            return;
        }

        arrows[stateVal].SetActive(true);
        arrows[stateVal - 1].SetActive(false);
    }
}
