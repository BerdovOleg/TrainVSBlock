using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    TextMeshProUGUI text;
    int value;
    bool SnakeEnter = false;
    float delay = .9f;
    float timer = .9f;
    public int minValue;
    public int maxValue;
    SnakeController Snake;

    private void Awake()
    {
        minValue = 1;
        maxValue = 4;//Временно
        value = Random.Range(minValue, maxValue);
    }

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = value.ToString();
    }
    private void Update()
    {
        if (SnakeEnter)
        {
            del();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<SnakeController>(out SnakeController snake))
        {
            Snake = snake;
            SnakeEnter = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<SnakeController>(out SnakeController snake))
        {
            SnakeEnter = false;
        }
    }

    private void del()
    {
        if (timer <= delay)
        {
            timer = 1f;
            value--;
            Snake.Enter();
            text.text = value.ToString();
            if (value < 1)
            {
                Debug.Log(value);
                Destroy(gameObject);
                SnakeEnter = false;
                Snake.Exit();
            }
        }
        timer -= Time.deltaTime;
    }
}
