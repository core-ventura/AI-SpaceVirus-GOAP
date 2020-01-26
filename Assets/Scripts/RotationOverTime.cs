using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOverTime : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate ( Vector3.up * ( rotationSpeed * Time.deltaTime ) ); 
    }
}
