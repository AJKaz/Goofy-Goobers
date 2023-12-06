using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 2f;
    public float timeSinceInst = 0f;
    public Vector3 randomOffsetMagnitude = new Vector3(0.15f, 0, 0);
    public Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.position += new Vector3(0, 0.5f, 0);
        transform.position += new Vector3(
            Random.Range(-randomOffsetMagnitude.x, randomOffsetMagnitude.x),
            Random.Range(-randomOffsetMagnitude.y, randomOffsetMagnitude.y),
            Random.Range(-randomOffsetMagnitude.z, randomOffsetMagnitude.z)
        );

        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceInst += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, initialPosition.y + GetCurveProgress(timeSinceInst), transform.position.z);
    }

    private float GetCurveProgress(float timePassed)
    {
        return timePassed;
    }
}