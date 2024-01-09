using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
Vector3 startingPosition;
[SerializeField] Vector3 movementVector;
float movementFactor;
[SerializeField] float period = 2f;

[SerializeField] float xSpin = 0;
[SerializeField] float ySpin = 0;
[SerializeField] float zSpin = 0;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period == 0f) {return;}  
        float cycles = Time.time / period; // continually growing over time
        
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so its cleaner
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

        transform.Rotate(new Vector3(xSpin, ySpin, zSpin) * Time.deltaTime);
    }
}
