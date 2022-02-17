using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private new Camera camera;
    public float randdicke = 10f;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - randdicke)
        {
            pos.y += 2f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= randdicke)
        {
            pos.y -= 2f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - randdicke)
        {
            pos.x += 2f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= randdicke)
        {
            pos.x -= 2f * Time.deltaTime;
        }
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize -= zoom * 30f * Time.deltaTime;

        transform.position = pos;
    }
}
