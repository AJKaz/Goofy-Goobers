using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputManager inputManager;

    [HideInInspector] 
    public List<Enemy> enemies;

    [SerializeField] private Transform[] path;

    [SerializeField] private int startingMoney = 100;

    /* Should be called Demon Essence in UI */
    private int money;

    public GameObject towerUiCanvas;

    [SerializeField] private SpellActivate spellActivate;

    private WaveManager waveManager;
    public WaveManager WaveManager { get { return waveManager; } }

    public TowerPlacement towerPlacement;

    public DivinePillar divinePillar;

    [Header("UI Stuff")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text cannonTowerCost;
    [SerializeField] private TMP_Text swordTowerCost;
    [SerializeField] private TMP_Text piercingTowerCost;
    [SerializeField] private TMP_Text freezeSpellCost;
    [SerializeField] private TMP_Text waveNumber;

    // Buttons
    private Button tower1Button;
    private Button tower2Button;
    private Button tower3Button;
    private Button spell1Button;

    [HideInInspector] public bool isInOnboarding = true;

    public int Money { get { return money; } }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        if (towerPlacement == null) {
            towerPlacement = GetComponent<TowerPlacement>();
        }
        if (spellActivate == null) {
            spellActivate = GetComponent<SpellActivate>();
        }

        money = startingMoney;
        waveManager = GetComponent<WaveManager>();

        UpdateMoneyText();

        Transform buildingPanel = towerUiCanvas.transform.Find("BuildingPanel");
        if (buildingPanel != null) {
            tower1Button = buildingPanel.Find("CannonTower").GetComponent<Button>();
            tower2Button = buildingPanel.Find("SwordTower").GetComponent<Button>();
            tower3Button = buildingPanel.Find("PiercingTower").GetComponent<Button>();
            spell1Button = buildingPanel.Find("FreezeSpell").GetComponent<Button>();


            if (tower1Button == null) {
                Debug.LogError("tower1Button not found!");
            }
            if (tower2Button == null) {
                Debug.LogError("tower2Button not found!");
            }
            if (tower3Button == null) {
                Debug.LogError("tower3Button not found!");
            }
            if (spell1Button == null) {
                Debug.LogError("spell1Button not found!");
            }
        }
        else {
            Debug.LogError("BuildingPanel not found!");
        }
    }

    private void Start() {
        cannonTowerCost.text = $"{towerPlacement.towerType1Prefab.Cost}";
        swordTowerCost.text = $"{towerPlacement.towerType2Prefab.Cost}";
        piercingTowerCost.text = $"{towerPlacement.towerType3Prefab.Cost}";
        freezeSpellCost.text = $"{spellActivate.FreezeSpellPrefab.Cost}";
    }

    public Transform[] GetPath() {
        return path;
    }

    public void AddMoney(int amount) {
        money += amount;
        // Clamp money between 0-750
        if (money > 750) money = 750;
        if (money < 0) money = 0;
        UpdateMoneyText();
        UpdateButtonInteractability();
    }

    public void UpdateButtonInteractability() {
        tower1Button.interactable = isInOnboarding || money < towerPlacement.towerType1Prefab.Cost ? false : true;
        tower2Button.interactable = isInOnboarding || money < towerPlacement.towerType2Prefab.Cost ? false : true;
        tower3Button.interactable = isInOnboarding || money < towerPlacement.towerType3Prefab.Cost ? false : true;
        spell1Button.interactable = isInOnboarding || spellActivate.IsFreezeOnCooldown || money < spellActivate.FreezeSpellPrefab.Cost ? false : true;
    }

    public bool RemoveEnemy(Enemy enemy) {
        return enemies.Remove(enemy);
    }

    public void UpdateMoneyText() {
        moneyText.text = $"{money}";
    }

    public void UpdateWaveNumber(int currentWave) {
        waveNumber.text = $"{currentWave}";
    }
}
