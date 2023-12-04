using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SpellActivate : MonoBehaviour
{
    [SerializeField] GameObject divinePillar;

    [Header("Spell UI Buttons")]
    [SerializeField] Button freezeSpellButton;
    [SerializeField] Button spell2Button;
    [SerializeField] Button spell3Button;

    [Header("Freeze Spell")]
    public FreezeSpell FreezeSpellPrefab;
    [SerializeField] private float freezeSpellCooldown = 15f;
    private bool isFreezeOnCooldown = false;
    public bool IsFreezeOnCooldown { get { return isFreezeOnCooldown; } }

    void Start()
    {
        freezeSpellButton.onClick.AddListener(delegate { OnClick(0); });
        spell2Button.onClick.AddListener(delegate { OnClick(1); });
    }

    public void OnClick(int index)
    {
        switch (index)
        {
            case 0:
                // Can cast spell if they have enough money & spell's not on cooldown
                if (GameManager.Instance.Money >= FreezeSpellPrefab.Cost && !isFreezeOnCooldown) {
                    StartCoroutine(FreezeSpellCoroutine());
                    Instantiate(FreezeSpellPrefab, divinePillar.transform.position, Quaternion.identity);
                    GameManager.Instance.AddMoney(-FreezeSpellPrefab.Cost);
                    GameManager.Instance.towerPlacement.deselectTower();
                }
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

    IEnumerator FreezeSpellCoroutine() {
        isFreezeOnCooldown = true;
        freezeSpellButton.interactable = false;

        yield return new WaitForSeconds(freezeSpellCooldown);

        isFreezeOnCooldown = false;
        GameManager.Instance.UpdateButtonInteractability();
    }

    public void ResetAllSpellCooldowns()
    {
        isFreezeOnCooldown = false;
        // Other spells go here
    }

}
