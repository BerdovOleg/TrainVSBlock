using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    TextMeshProUGUI text;
    int value;
    int minValue;
    public int maxValue;
    Renderer renderer;
    float x;


    private void Awake()
    {
        minValue = 1;
        maxValue = 4;//Временно

        value = Random.Range(minValue, maxValue);
        renderer = GetComponent<Renderer>();
        x = (1f / maxValue);
        renderer.material.color = new Color(1-(x*value), 0, 0);

    }

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = value.ToString();
    }

    public void EnterSnaske()
    {
        value--;
        text.text = value.ToString();
        if (value < 1)
        {
            Debug.Log(value);
            Destroy(gameObject);
            // Snake.Exit();
        }
    }
}
