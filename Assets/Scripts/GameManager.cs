using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int selectedWeaponIndex = 0; // 0 or 1
                                               // Start is called before the first frame update
    public int coins = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        // Optional: update UI here
    }
}
