using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Button testButton;

    void Start()
    {
        testButton.onClick.AddListener(() => Debug.Log("Button Clicked"));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (testButton != null)
            {
                testButton.onClick.Invoke();
                Debug.Log("Upgrade Damage button activated via key.");
            }
        }
    }
}
