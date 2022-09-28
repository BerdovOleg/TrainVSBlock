using System;
using TMPro;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] float SnakeSpeed = 10;
    [SerializeField] float Sensitivity;
    [SerializeField] Rigidbody Rigidbody;
    Vector2 touchLastPos;
    [SerializeField] float sidewaysSpeed;
    [SerializeField] Camera mainCamera;
    public int Length = 4;
    [SerializeField]private SnakeTail _SnakeTail;

    float delay = .9f;
    float timer = .9f;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        SetText(Length);
        _SnakeTail.AddTail(Length);
    }

    private void SetText(int i)
    {
        i ++;
        text.text =  i.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MousControll();

        if (Input.GetKeyDown(KeyCode.A))
        {
            Length++;
            _SnakeTail.AddTail(1);
            SetText(Length);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Length--;
            _SnakeTail.RemoveTail();
            SetText(Length);
        }

    }

    private void MousControll()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
            sidewaysSpeed += delta.x * Sensitivity;
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 1) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        Rigidbody.velocity = new Vector3(sidewaysSpeed * 10, 0, SnakeSpeed);

        sidewaysSpeed = 0;
     }

   
    private void OnCollisionStay(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal.normalized;
        float dot = Vector3.Dot(normal, Vector3.down);

        if (dot < 0.5) Debug.Log(dot);
    }

    public void Enter()
    {
        SnakeSpeed = 0;
        Length--;
        SetText(Length);
        _SnakeTail.RemoveTail();
        Debug.Log("RemoveTail");
        if (Length < 1)
        {
            GameOver();
        }

    }

    private void GameOver()
    {
        Debug.Log(Length);
    }

    public void Exit()
    {
        SnakeSpeed = 5;
    }

    public void EatFood(int value)
    {
        Length += value;
        _SnakeTail.AddTail(value);
        SetText(Length);
    }
}