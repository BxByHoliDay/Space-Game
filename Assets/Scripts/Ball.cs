using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Ball Settings")]
    public int score = 1;
    public float lifeTime = 10f;

    private GameObject rootObject;

    private void Start()
    {
        rootObject = transform.parent != null ? transform.parent.gameObject : gameObject;
        Destroy(rootObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerCollision();
        }
        else if (other.CompareTag("Ground"))
        {
            DestroySelf();
        }
    }

    private void HandlePlayerCollision()
    {
        if (CompareTag("Bomb"))
            GameManager.Instance.RemoveScore(score);
        else
            GameManager.Instance.AddScore(score);

        DestroySelf();
    }

    private void DestroySelf()
    {
        if (rootObject != null)
            Destroy(rootObject);
    }
}
