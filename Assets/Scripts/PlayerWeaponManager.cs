using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public GameObject[] weaponPrefabs; // assign both weapon prefabs here
    public GameObject[] ammoPrefabs; // assign both ammo prefabs here
    public Transform weaponSpawnPoint;

    void Start()
    {
        //set spawn point to player's transform
        if (weaponSpawnPoint == null)
        {
            weaponSpawnPoint = transform;
        }
        int selectedWeapon = GameManager.selectedWeaponIndex;
        GameObject weapon = Instantiate(weaponPrefabs[selectedWeapon], weaponSpawnPoint.position, weaponSpawnPoint.rotation);
        GameObject ammo = Instantiate(ammoPrefabs[selectedWeapon], weaponSpawnPoint.position, weaponSpawnPoint.rotation);
    }
}
