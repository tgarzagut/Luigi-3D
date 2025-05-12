using UnityEngine;
using TMPro;

public class ChestInteraction : MonoBehaviour
{
    public GameObject chestClosed1;
    public GameObject chestOpen1;
    public Transform player;

    public float interactDistance = 3f;
    public float rotationAmount = 10f;
    public TextMeshProUGUI pressEText;

    private bool isOpened = false;
    private static ChestInteraction currentClosestChest;

    void Start()
    {
        chestClosed1.SetActive(true);
        chestOpen1.SetActive(false);

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
            StartCoroutine(OpenChest());
        }
    }

    void ShowPrompt()
    {
        if (pressEText != null)
        {
            pressEText.text = "Press E";
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

        Quaternion startRot = chestClosed1.transform.localRotation;
        Quaternion midRot = startRot * Quaternion.Euler(-rotationAmount, 0, 0);
        Quaternion endRot = startRot * Quaternion.Euler(rotationAmount, 0, 0);

        while (timer < duration)
        {
            chestClosed1.transform.localRotation = Quaternion.Slerp(startRot, midRot, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < duration)
        {
            chestClosed1.transform.localRotation = Quaternion.Slerp(midRot, endRot, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        chestClosed1.SetActive(false);
        chestOpen1.SetActive(true);
        ChestUIManager.Instance.ShowNextChestIcon();
    }
}
