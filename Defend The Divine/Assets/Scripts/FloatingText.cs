using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.position += new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}