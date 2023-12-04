using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    private Transform transform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float dampingSpeed = 0.995f;
    Vector3 initialPosition;

    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0f)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
            shakeMagnitude *= dampingSpeed;
        } else
        {
            shakeDuration = 0f;
            shakeMagnitude = 0.1f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float shakeDuration = 0.25f, float shakeMagnitude = 0.1f, float dampingSpeed = 0.995f)
    {
        this.shakeDuration = shakeDuration;
        this.shakeMagnitude = shakeMagnitude;
        this.dampingSpeed = dampingSpeed;
    }
}
