using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Shop UI Elements")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject playMenuPanel; // Reference for PlayMenuPanel
    [SerializeField] private TMP_Text totalCoinsText;

    [Header("Plane Item UI")]
    [SerializeField] private TMP_Text[] buttonTexts;
    [SerializeField] private Button[] planeButtons;

    [Header("Plane Prices")]
    [SerializeField] private int[] planePrices = { 0, 200, 500 };

    private const string COINS_KEY = "TotalCoins";
    private const string SELECTED_PLANE_KEY = "SelectedPlane";

    private void Start()
    {
        PlayerPrefs.SetInt("Plane_0_Unlocked", 1);
        UpdateUI();
    }

    private void OnEnable()
    {
        // Re-fetch coins and state every time the Shop panel/object becomes active
        UpdateUI();
    }

    public void OpenShop()
    {
        if (playMenuPanel != null) playMenuPanel.SetActive(false); // Hide Play Menu
        if (shopPanel != null) shopPanel.SetActive(true);         // Show Shop
        UpdateUI();
    }

    public void CloseShop()
    {
        if (shopPanel != null) shopPanel.SetActive(false);       // Hide Shop
        if (playMenuPanel != null) playMenuPanel.SetActive(true); // Return to Play Menu

        // Ensure Main Menu UI updates its coin display when returning to Play Menu
        RefreshMainMenuCoins();
    }

    public void UpdateUI()
    {
        int totalCoins = PlayerPrefs.GetInt(COINS_KEY, 0);
        int selectedPlane = PlayerPrefs.GetInt(SELECTED_PLANE_KEY, 0);

        if (totalCoinsText != null)
        {
            totalCoinsText.text = "x" + totalCoins;
        }

        for (int i = 0; i < planePrices.Length; i++)
        {
            bool isUnlocked = PlayerPrefs.GetInt("Plane_" + i + "_Unlocked", 0) == 1;

            if (selectedPlane == i)
            {
                buttonTexts[i].text = "Selected";
                planeButtons[i].interactable = false;
            }
            else if (isUnlocked)
            {
                buttonTexts[i].text = "Equip";
                planeButtons[i].interactable = true;
            }
            else
            {
                buttonTexts[i].text = "$ " + planePrices[i];
                planeButtons[i].interactable = (totalCoins >= planePrices[i]);
            }
        }
    }

    public void OnClickPlaneButton(int planeIndex)
    {
        int totalCoins = PlayerPrefs.GetInt(COINS_KEY, 0);
        bool isUnlocked = PlayerPrefs.GetInt("Plane_" + planeIndex + "_Unlocked", 0) == 1;

        if (isUnlocked)
        {
            PlayerPrefs.SetInt(SELECTED_PLANE_KEY, planeIndex);
        }
        else if (totalCoins >= planePrices[planeIndex])
        {
            totalCoins -= planePrices[planeIndex];
            PlayerPrefs.SetInt(COINS_KEY, totalCoins);
            PlayerPrefs.SetInt("Plane_" + planeIndex + "_Unlocked", 1);
            PlayerPrefs.SetInt(SELECTED_PLANE_KEY, planeIndex);
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