using UnityEngine;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour
{
    public GameObject statsPanel;
    public TMP_Text statsText;

    private void Start()
    {
        statsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleStatsPanel();
        }
    }

    public void ToggleStatsPanel()
    {
        bool isActive = statsPanel.activeSelf;
        statsPanel.SetActive(!isActive);

        if (!isActive)
        {
            UpdateStatsText();
        }
    }

    public void ShowStatsOnShopOpen()
    {
        statsPanel.SetActive(true);
        UpdateStatsText();
    }

    void UpdateStatsText()
    {
        var fps = FindObjectOfType<FPSPlayer>();
        string text =
            $"<b>Player Stats</b>\n" +
            $"Health: {fps.hitPoints}/{fps.maximumHitPoints}\n" +
            $"Damage Multiplier: {PlayerStats.Instance.damageMultiplier:F2}\n" +
            $"Coin Bonus: {PlayerStats.Instance.bonusCoinAmount}\n" +
            $"Coin Drop Chance: {PlayerStats.Instance.bonusCoinChance * 100f:F0}%\n" +
            $"Regen Rate: {PlayerStats.Instance.regenRate:F1}/s\n" +
            $"Regen Delay: {PlayerStats.Instance.regenDelay:F1}s\n";

        statsText.text = text;
    }
}
