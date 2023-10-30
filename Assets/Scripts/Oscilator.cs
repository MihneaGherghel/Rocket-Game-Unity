using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movemenetVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave=Mathf.Sin(cycles*tau);
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movemenetVector*movementFactor;
        transform.position= startingPosition + offset;
    }
}
