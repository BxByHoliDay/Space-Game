using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityStep = -1f;   
    public float interval = 10f;     
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            
            Physics.gravity += new Vector3(0, gravityStep, 0);
            Debug.Log("Gravity Now: " + Physics.gravity);
        }
    }
}
