using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpellActivate : MonoBehaviour
{
    [SerializeField] Button spell1Button;
    [SerializeField] Button spell2Button;
    [SerializeField] Button spell3Button;
    // Start is called before the first frame update
    void Start()
    {
        spell1Button.onClick.AddListener(delegate { OnClick(0); });
        spell2Button.onClick.AddListener(delegate { OnClick(1); });
        spell3Button.onClick.AddListener(delegate { OnClick(2); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("spell 1 activate");
                break;
            case 1:
                Debug.Log("spell 2 activate");
                break;
            case 2:
                Debug.Log("spell 3 activate");
                break;
            default:
                break;
        }
    }
    
}
