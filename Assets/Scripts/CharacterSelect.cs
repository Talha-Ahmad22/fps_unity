using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    [Header("Weapon Models")]
    public GameObject[] weaponPreviews; // 2 weapon preview models (inactive by default)

    [Header("UI Buttons")]
    public Button leftArrowButton;
    public Button rightArrowButton;
    public Button playButton;
    public Button exitButton;

    private int selectedWeaponIndex = 0;

    void Start()
    {
        UpdateWeaponPreview();

        leftArrowButton.onClick.AddListener(() => ChangeWeapon(-1));
        rightArrowButton.onClick.AddListener(() => ChangeWeapon(1));
        playButton.onClick.AddListener(PlayGame);
    }

    void ChangeWeapon(int direction)
    {
        // Deactivate current weapon preview
        weaponPreviews[selectedWeaponIndex].SetActive(false);

        // Change index
        selectedWeaponIndex += direction;
        if (selectedWeaponIndex < 0) selectedWeaponIndex = weaponPreviews.Length - 1;
        if (selectedWeaponIndex >= weaponPreviews.Length) selectedWeaponIndex = 0;

        // Activate new weapon preview
        UpdateWeaponPreview();
    }

    void UpdateWeaponPreview()
    {
        for (int i = 0; i < weaponPreviews.Length; i++)
        {
            weaponPreviews[i].SetActive(i == selectedWeaponIndex);
        }
    }

    void PlayGame()
    {
        PlayerPrefs.SetInt("SelectedWeapon", selectedWeaponIndex);
        GameManager.selectedWeaponIndex = selectedWeaponIndex; // Store selected weapon index in GameManager
        SceneManager.LoadScene("Level1"); // Replace with your game scene name
    }
}
