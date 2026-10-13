using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages plane shop menu navigation, plane unlocking and purchasing logic, and equip state UI.
/// </summary>
public class ShopManager : MonoBehaviour
{
    [Header("Shop UI Elements")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject playMenuPanel;
    [SerializeField] private TMP_Text totalCoinsText;

    [Header("Plane Item UI")]
    [SerializeField] private TMP_Text[] buttonTexts;
    [SerializeField] private Button[] planeButtons;

    [Header("Plane Prices")]
    [SerializeField] private int[] planePrices = { 0, 200, 500 };

    private const string CoinsKey = "TotalCoins";
    private const string SelectedPlaneKey = "SelectedPlane";

    private void Start()
    {
        PlayerPrefs.SetInt("Plane_0_Unlocked", 1);
        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void OpenShop()
    {
        if (playMenuPanel != null) playMenuPanel.SetActive(false);
        if (shopPanel != null) shopPanel.SetActive(true);
        UpdateUI();
    }

    public void CloseShop()
    {
        if (shopPanel != null) shopPanel.SetActive(false);
        if (playMenuPanel != null) playMenuPanel.SetActive(true);

        RefreshMainMenuCoins();
    }

    public void UpdateUI()
    {
        int totalCoins = PlayerPrefs.GetInt(CoinsKey, 0);
        int selectedPlane = PlayerPrefs.GetInt(SelectedPlaneKey, 0);

        if (totalCoinsText != null)
        {
            totalCoinsText.text = "x" + totalCoins;
        }

        for (int i = 0; i < planePrices.Length; i++)
        {
            if (i >= buttonTexts.Length || i >= planeButtons.Length) break;

            bool isUnlocked = PlayerPrefs.GetInt("Plane_" + i + "_Unlocked", 0) == 1;

            if (selectedPlane == i)
            {
                if (buttonTexts[i] != null) buttonTexts[i].text = "Selected";
                if (planeButtons[i] != null) planeButtons[i].interactable = false;
            }
            else if (isUnlocked)
            {
                if (buttonTexts[i] != null) buttonTexts[i].text = "Equip";
                if (planeButtons[i] != null) planeButtons[i].interactable = true;
            }
            else
            {
                if (buttonTexts[i] != null) buttonTexts[i].text = "$ " + planePrices[i];
                if (planeButtons[i] != null) planeButtons[i].interactable = totalCoins >= planePrices[i];
            }
        }
    }

    public void OnClickPlaneButton(int planeIndex)
    {
        int totalCoins = PlayerPrefs.GetInt(CoinsKey, 0);
        bool isUnlocked = PlayerPrefs.GetInt("Plane_" + planeIndex + "_Unlocked", 0) == 1;

        if (isUnlocked)
        {
            PlayerPrefs.SetInt(SelectedPlaneKey, planeIndex);
        }
        else if (totalCoins >= planePrices[planeIndex])
        {
            totalCoins -= planePrices[planeIndex];
            PlayerPrefs.SetInt(CoinsKey, totalCoins);
            PlayerPrefs.SetInt("Plane_" + planeIndex + "_Unlocked", 1);
            PlayerPrefs.SetInt(SelectedPlaneKey, planeIndex);
        }

        PlayerPrefs.Save();
        UpdateUI();
        RefreshMainMenuCoins();
    }

    private void RefreshMainMenuCoins()
    {
        MainMenuManager mainMenu = FindFirstObjectByType<MainMenuManager>();
        if (mainMenu != null)
        {
            mainMenu.UpdateCoinUI();
        }
    }
}