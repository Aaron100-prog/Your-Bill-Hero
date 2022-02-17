using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
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
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize -= zoom * 30f * Time.deltaTime;

        transform.position = pos;
    }
}
