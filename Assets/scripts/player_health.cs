using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    void Start()
    {
        currentLives = maxLives;
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
        currentLives--;
        Debug.Log("Player hit! Lives remaining: " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("Player is out of lives!");

        }
    }
}
