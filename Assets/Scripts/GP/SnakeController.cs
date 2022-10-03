using System;
using TMPro;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] float SnakeSpeed;
    [SerializeField] float Sensitivity;
    [SerializeField] Rigidbody Rigidbody;
    Vector2 touchLastPos;
    [SerializeField] float sidewaysSpeed;
    [SerializeField] Camera mainCamera;
    public int Length = 4;
    private bool startSnake;
    [SerializeField] private SnakeTail _SnakeTail;
    public Enemy enemy;
    public Finish finish;

    [SerializeField] private AudioClip PicupClip;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        SetText(Length);
    }

    public void StartSnake() {
        startSnake = true;
        _SnakeTail.AddTail(Length);
        SnakeSpeed = 10;
    }

    private void SetText(int i)
    {
        text.text =  i.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MousControll();
    }

    public void SnakeEnterBlock()
    {
        print("Lenght Snake: " + Length);
        Length--;
        _SnakeTail.RemoveTail();
        SetText(Length);
    }

    private void MousControll()
    {
        if (startSnake & !finish)
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
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 1& startSnake) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        Rigidbody.velocity = new Vector3(sidewaysSpeed * 10, 0, SnakeSpeed);

        sidewaysSpeed = 0;
     }

    public void Destroy()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy _enemy))
        {
            enemy = _enemy;
        }

        if (other.TryGetComponent<Finish>(out Finish _finish))
        {
            finish = _finish;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemy != null)
        {
            if (enemy.transform.position.magnitude > transform.position.magnitude)
            {
                enemy = null;
            }
        }
    }

    public void StopSnake()
    {
        SnakeSpeed = 0;
        startSnake = false;
    }

    public void EatFood(int value)
    {
        Length += value;
        _SnakeTail.AddTail(value);
        audioSource.clip = PicupClip;
        audioSource.Play();
        SetText(Length);
    }
}