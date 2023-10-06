using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabToInstantiate;
    //Random random = new System.Random();
    [SerializeField]
    float elapsedTime;
    [SerializeField]
    float previousFrameElapsedTime;
    // Start is called before the first frame update
    void Start() {
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update() {
        previousFrameElapsedTime = elapsedTime;
        elapsedTime += Time.deltaTime;
        // If the previous frame was before the 10s mark and the current frame is
        // after it, spawn enemies once. (Does not break on low FPS)
        if (previousFrameElapsedTime % 10 > elapsedTime % 10) {
            float angleRadians = Random.Range(0,360) * (Mathf.PI / 180);

            // Spawn enemy 15 units away in the generated direction
            float x = 15 * Mathf.Cos(angleRadians);
            float y = 15 * Mathf.Sin(angleRadians);
            Instantiate(prefabToInstantiate, new Vector3(x, y, 0.0f), new Quaternion());       }
    }
}
