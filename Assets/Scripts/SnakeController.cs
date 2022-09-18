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
        text.text = Length.ToString();
        for (int i = 0; i < Length; i++) _SnakeTail.AddTail();
    }

    // Update is called once per frame
    void Update()
    {
        MousControll();

        if (Input.GetKeyDown(KeyCode.A))
        {
            Length++;
            _SnakeTail.AddTail();
            text.text = (Length.ToString());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Length--;
            _SnakeTail.RemoveTail();
            text.text = (Length.ToString());
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Enter();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
            Exit();
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    public void Enter()
    {
        SnakeSpeed = 0;
        if (timer <= delay)
        {
            timer = 1f;
            Length--;
            text.text = Length.ToString();
            _SnakeTail.RemoveTail();
            if (Length < 1)
            {
                Debug.Log(Length);
            }
        }
        timer -= Time.deltaTime;
        SnakeSpeed = 10;
    }

    public void Exit()
    {
        SnakeSpeed = 10;
    }
}