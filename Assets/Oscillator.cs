using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    float movementFactor;

    //cashe
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // Epsilon - najmniejsza wartośc float

        float cycles = Time.time / period; //  Time.time - czas od początku sceny uzależnienie szybkości zmiany sin od wartości period
        const float tau = Mathf.PI * 2f;

        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f; // zmiana zakresu z -1 +1 na 0 +1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;

    }
}
