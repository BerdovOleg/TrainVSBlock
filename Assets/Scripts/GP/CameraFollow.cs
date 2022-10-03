using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform SnakePos;
    [SerializeField] Vector3 StartPos;
    [SerializeField] float Camspeed;
    [SerializeField] Vector3 delta;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        delta = StartPos + SnakePos.position;
        delta = new Vector3(delta.x, delta.y, delta.z);
        var a = new Vector3(transform.position.x,transform.position.y, delta.z+10.5f);
        transform.position =Vector3.MoveTowards(transform.position, a,  Camspeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        
    }
}
