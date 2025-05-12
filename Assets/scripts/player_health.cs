using UnityEngine;
using UnityEngine.UI; // Required for Image

public class PlayerLives : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public Image[] lifeIcons;

    void Start()
    {
        currentLives = maxLives;
        UpdateHearts();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("curse"))
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        if (currentLives <= 0) return;

        currentLives--;
        Debug.Log("Player hit! Lives remaining: " + currentLives);

        UpdateHearts();

        if (currentLives <= 0)
        {
            Debug.Log("Player is out of lives!");
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < currentLives;
        }
    }
}
