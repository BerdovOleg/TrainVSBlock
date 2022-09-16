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
    [SerializeField] private GameObject sectors;
    GameObject[] prefubs;
    public int SectorCount;
    
    private Vector3 startV3;
    private float speed = 3f;
    private float LeftBoard = -5.5f;
    private float RightBoard = 5.5f;

    private float coldawn = 0.9f;
    private float timer;


    private Vector3 Forward;
    // Start is called before the first frame update
    void Start()
    {
        prefubs = new GameObject[SectorCount];
        Forward = Vector3.forward;
        int nubbers = -1;
        for (int i = 0; i < prefubs.Length; i++)
        {
            var V3 = transform.position;
            prefubs[i] = Instantiate(sectors, new Vector3(V3.x, V3.y, nubbers), Quaternion.identity);
            nubbers--;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy _enemy))
        {
            Forward = Vector3.zero;
        }
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
                SectorLeft(true);
            }
            else if (startV3.normalized.x < mousePos.normalized.x & transform.position.x < RightBoard & timer <= coldawn)
            {
                transform.position =
                    new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                timer = 1f;
                SectorLeft(false);
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

    private void SectorLeft(bool left)
    {
        int i = prefubs.Length;
        int cur = 0;
        float localTimer = 1;
        return;
        //while (cur<i)
        //{
        //    return;
        //    if(0,01f <= 0.1f)
        //    {
        //        if (left)
        //        {
        //            Debug.Log("перемещение в лево " +cur);
        //            prefubs[cur].transform.position =
        //                new Vector3(prefubs[cur].transform.position.x - 0.5f, prefubs[cur].transform.position.y, prefubs[cur].transform.position.z);
        //            localTimer = 1f;
        //            cur++;
        //        }
        //        else
        //        {
        //            prefubs[cur].transform.position =
        //                     new Vector3(prefubs[cur].transform.position.x + 0.5f, prefubs[cur].transform.position.y, prefubs[cur].transform.position.z);
        //            localTimer = 1f;
        //            cur++;
        //        }
        //    }
        //    localTimer -= Time.deltaTime;
        //}
    }

    private void MoveForward()
    {
        transform.Translate(Forward * speed * Time.deltaTime);
        camera.transform.position =
            new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z);
        foreach (var item in prefubs)
        {
            item.transform.Translate(Forward * speed * Time.deltaTime);
        }
    }
}
