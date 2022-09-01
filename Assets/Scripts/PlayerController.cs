using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI StartText;
    [SerializeField]private TextMeshProUGUI CurentText;
    [SerializeField]private TextMeshProUGUI TimerTest;
    [SerializeField] private GameObject camera;
    
    private Vector3 startV3;
    private float speed = 3f;
    private float LeftBoard = -5.5f;
    private float RightBoard = 5.5f;

    private float coldawn = 0.9f;
    private float timer;

    Sector[] blocks;

    private Vector3 Forward;
    // Start is called before the first frame update
    void Start()
    {
        Forward = Vector3.forward;

        blocks = GetComponentsInChildren<Sector>();
        int nubbers = -1;
        foreach (var item in blocks)
        {
            var V3 = item.gameObject.transform.position;
            item.gameObject.transform.position = new Vector3(V3.x, V3.y, nubbers);

            nubbers--;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy _enemy))
        {
            Debug.Log("IsEnemy");
            Forward = Vector3.zero;
        }
        Debug.Log("Messages");
    }

    void OnCollisionExit(Collision collision)
    {
        Forward = Vector3.forward;
    }




    // Update is called once per frame
    void Update()
    {
        MoveHorizontle();

        MoveForward();
    }

    private void MoveHorizontle()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 mousePos = Input.mousePosition;

            StartText.text = "   Start: " + startV3.normalized;
            CurentText.text = "Current: " + mousePos.normalized;

            if (startV3.normalized.x > mousePos.normalized.x & transform.position.x > LeftBoard & timer <= coldawn)
            {
                transform.position =
                    new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                timer = 1f;
                SectorLeft();
            }
            else if (startV3.normalized.x < mousePos.normalized.x & transform.position.x < RightBoard & timer <= coldawn)
            {
                transform.position =
                    new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                timer = 1f;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        else
        {
            startV3 = Input.mousePosition;
            timer = 0;
        }

        TimerTest.text = timer.ToString();
    }

    private void SectorLeft()
    {
        float localTimer = 1;

        if (localTimer<=coldawn)
        {
            foreach (var item in blocks)
            {
                item.transform.position =
                         new Vector3(item.transform.position.x - 0.5f, item.transform.position.y, item.transform.position.z);
                localTimer = 1f;
            }
        }
        localTimer -= Time.deltaTime;
    }

    private void MoveForward()
    {
        transform.Translate(Forward * speed * Time.deltaTime);
        camera.transform.position =
            new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z);
    }
}
