using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestorySelf : MonoBehaviour
{

    public float timeUntilDestruction;
    float timer;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeUntilDestruction)
        {
            Destroy(gameObject);
        }

    }
}
