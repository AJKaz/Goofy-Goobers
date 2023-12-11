using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SpellActivate : MonoBehaviour
{
    [SerializeField] GameObject divinePillar;

    [Header("Spell UI Buttons")]
    [SerializeField] Button freezeSpellButton;

    [Header("Freeze Spell")]
    public FreezeSpell FreezeSpellPrefab;
    [SerializeField] private float freezeSpellCooldown = 30f;
    private bool isFreezeOnCooldown = false;
    private float freezeSpellTimer;
    public bool IsFreezeOnCooldown { get { return isFreezeOnCooldown; } }

    void Start()
    {
        freezeSpellButton.onClick.AddListener(delegate { OnClick(0); });
        freezeSpellTimer = freezeSpellCooldown;
    }

    private void Update() {
        if (isFreezeOnCooldown) {
            freezeSpellTimer -= Time.deltaTime;
            if (freezeSpellTimer <= 0f) {
                freezeSpellTimer = freezeSpellCooldown;
                isFreezeOnCooldown = false;
                GameManager.Instance.UpdateButtonInteractability();
            }
        }
    }

    public void OnClick(int index)
    {
        // Trigger camera shake
        FindFirstObjectByType<ShakeBehavior>().TriggerShake(0.5f, 0.3f);

        switch (index)
        {
            case 0:
                // Can cast spell if they have enough money & spell's not on cooldown
                if (GameManager.Instance.Money >= FreezeSpellPrefab.Cost && !isFreezeOnCooldown) {
                    //StartCoroutine(FreezeSpellCoroutine());
                    isFreezeOnCooldown = true;
                    freezeSpellButton.interactable = false;
                    Instantiate(FreezeSpellPrefab, divinePillar.transform.position, Quaternion.identity);
                    GameManager.Instance.AddMoney(-FreezeSpellPrefab.Cost);
                    GameManager.Instance.towerPlacement.deselectTower();
                }
                break;
            default:
                break;
        }
    }

    /*IEnumerator FreezeSpellCoroutine() {
        isFreezeOnCooldown = true;
        freezeSpellButton.interactable = false;

        yield return new WaitForSeconds(freezeSpellCooldown);

        isFreezeOnCooldown = false;
        GameManager.Instance.UpdateButtonInteractability();
    }*/

    public void ResetAllSpellCooldowns()
    {
        isFreezeOnCooldown = false;
        freezeSpellTimer = freezeSpellCooldown;
        GameManager.Instance.UpdateButtonInteractability();
        // Other spells go here
    }

}
