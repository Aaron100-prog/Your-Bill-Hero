using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private new Camera camera;
    private Vector3 letztemausposition;
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
        transform.position = pos;
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize -= zoom * 30f * Time.deltaTime;

        if(Input.GetMouseButtonDown(2))
        {
            letztemausposition = camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 veraenderung = camera.ScreenToWorldPoint(Input.mousePosition) - letztemausposition;
            transform.Translate(veraenderung.x * -1f, veraenderung.y * -1f, 0f);
        }
        if (Input.GetMouseButtonUp(2))
        {
            letztemausposition = transform.position;
        }
        letztemausposition = camera.ScreenToWorldPoint(Input.mousePosition);

    }
}
