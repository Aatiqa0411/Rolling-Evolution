using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRotation : MonoBehaviour
{
    
    public float rotationSpeed = 100f; // degrees per second
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime,0f, 0f);
    }
    
}
