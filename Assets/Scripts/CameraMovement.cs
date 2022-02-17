using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("test");
        Vector3 pos = transform.position;
        if(Input.GetKey(KeyCode.W))
        {
            pos.y += 2f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= 2f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += 2f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= 2f * Time.deltaTime;
        }

        transform.position = pos;
    }
}
