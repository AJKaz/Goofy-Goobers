using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 targetPosition;

    [SerializeField] GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float moveSpeed = 10f;

        transform.position += direction * moveSpeed * Time.deltaTime;

        float angle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = transform.forward * angle;
    }
}
