using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Food : MonoBehaviour
{
    public int minValue;
    public int maxValue;
    private int value;
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        minValue = 1;
        maxValue = 4;//Временно
        value = Random.Range(minValue, maxValue);
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = value.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<SnakeController>(out SnakeController snake))
        {
            snake.EatFood(value);
            Destroy(gameObject);
        }
    }

}
