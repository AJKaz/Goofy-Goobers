using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugTextMesh;

    private Canvas canvas;
    private static Dictionary<string, string> debugText = new();

    // Start is called before the first frame update
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        debugTextMesh.text = "";
        foreach(KeyValuePair<string, string> item in debugText)
        {
            debugTextMesh.text += $"{item.Key}: {item.Value}\n";
        }
    }

    public static void AddDebugText(string name, string data)
    {
        debugText[name] = data;
    }

    public void EnableDebugCanvas(bool debugEnabled)
    {
        canvas.enabled = debugEnabled;
    }

    public void OnToggleDebugCanvas(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            EnableDebugCanvas(!canvas.enabled);
        }
    }
}
