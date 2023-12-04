using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color color;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = new Color(0f, 0f, 0f, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        color.a += Time.deltaTime / 2;
        if (color.a >= 1.0f) SceneManager.LoadScene("GameOver");
        spriteRenderer.color = color;
    }
}
