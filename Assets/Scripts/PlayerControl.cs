using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] [Range(.01f, .1f)] float speed;
    Vector3 _previevMousePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _previevMousePos;
            delta = delta.normalized;
            
        }

        if (Input.GetAxis("Horizontal")>0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }


        _previevMousePos = Input.mousePosition;
#else

#endif
    }
}