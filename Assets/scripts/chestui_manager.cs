using UnityEngine;
using UnityEngine.UI;

public class ChestUIManager : MonoBehaviour
{
    public Image[] chestIcons;
    private int currentChestCount = 0;

    public static ChestUIManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowNextChestIcon()
    {
        if (currentChestCount < chestIcons.Length)
        {
            chestIcons[currentChestCount].gameObject.SetActive(true);
            currentChestCount++;
        }
    }
}
