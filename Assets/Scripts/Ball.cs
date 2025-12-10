using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int score = 1;
    public float lifeTime = 10f;

    private void Start()
    {
        Destroy(transform.parent.gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Bomb"))
            {
                GameManager.Instance.RemoveScore(score);
            }
            else
            {
                GameManager.Instance.AddScore(score);
            }

            GameObject root = transform.parent != null ? transform.parent.gameObject : gameObject;
            Destroy(root);
        }
        else if (other.CompareTag("Ground"))
        {
            GameObject root = transform.parent != null ? transform.parent.gameObject : gameObject;
            Destroy(root);
        }
    }
}
