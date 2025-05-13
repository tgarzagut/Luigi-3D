using UnityEngine;
using TMPro;

public class CursedChestInteraction : MonoBehaviour
{
    public GameObject chestClosed;
    public GameObject chestOpen;
    public Transform player;
    public PlayerLives playerLives;
    public GameObject winPanel;
    public GameObject loseToLivesPanel;


    public float interactDistance = 3f;
    public float rotationAmount = 10f;
    public TextMeshProUGUI pressEText;

    private bool isOpened = false;
    private static CursedChestInteraction currentClosestChest;

    void Start()
    {
        chestClosed.SetActive(true);
        chestOpen.SetActive(false);

        if (pressEText != null)
            pressEText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isOpened) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactDistance)
        {
            if (currentClosestChest == null || distance < Vector3.Distance(player.position, currentClosestChest.transform.position))
            {
                if (currentClosestChest != null && currentClosestChest != this)
                    currentClosestChest.HidePrompt();

                currentClosestChest = this;
                ShowPrompt();
            }
        }
        else
        {
            if (currentClosestChest == this)
            {
                HidePrompt();
                currentClosestChest = null;
            }
        }

        if (currentClosestChest == this && Input.GetKeyDown(KeyCode.E))
        {
            if (ChestUIManager.Instance.AllChestsOpened())
            {
                StartCoroutine(OpenChest());
            }
            else
            {
                // Optional: flash message or play error sound
                Debug.Log("You need to find all chests before opening this one.");
            }
        }
    }

    void ShowPrompt()
    {
        if (pressEText != null)
        {
            if (ChestUIManager.Instance.AllChestsOpened())
            {
                pressEText.text = "Press E";
            }
            else
            {
                pressEText.text = "Find all chests to interact with cursed chest";
            }

            pressEText.gameObject.SetActive(true);
        }
    }

    void HidePrompt()
    {
        if (pressEText != null)
            pressEText.gameObject.SetActive(false);
    }

    System.Collections.IEnumerator OpenChest()
    {
        isOpened = true;
        HidePrompt();

        float duration = 0.2f;
        float timer = 0f;

        Quaternion startRot = chestClosed.transform.localRotation;
        Quaternion midRot = startRot * Quaternion.Euler(-rotationAmount, 0, 0);
        Quaternion endRot = startRot * Quaternion.Euler(rotationAmount, 0, 0);

        while (timer < duration)
        {
            chestClosed.transform.localRotation = Quaternion.Slerp(startRot, midRot, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < duration)
        {
            chestClosed.transform.localRotation = Quaternion.Slerp(midRot, endRot, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        chestClosed.SetActive(false);
        chestOpen.SetActive(true);

        Debug.Log("Cursed chest opened!");
        if (playerLives.CurrentLives == playerLives.maxLives)
        {
            Debug.Log("Player won with full lives!");
            if (winPanel != null)
                winPanel.SetActive(true);
        }
        else {
            Debug.Log("Player opened chest but lost some lives.");
            if (loseToLivesPanel != null)
                loseToLivesPanel.SetActive(true);
        }
    }
}
