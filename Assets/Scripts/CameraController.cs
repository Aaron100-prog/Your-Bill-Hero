using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private new Camera camera;
    private Vector3 letztemausposition;
    private float targetzoom;
    [HideInInspector]
    public bool following;
    public GameObject followtarget;
    public float followsmooth = 2f;
    // Start is called before the first frame update

    public static CameraController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        camera = GetComponent<Camera>();
        targetzoom = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel") * 5;
            targetzoom = targetzoom - zoom;
        }
        if (targetzoom < 0.5f)
        {
            targetzoom = 0.5f;
        }
        if (targetzoom > 20)
        {
            targetzoom = 20;
        }
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetzoom, Time.deltaTime * 7.5f);
        if(Input.GetKeyDown(KeyCode.F1))
        {
            UIManager.instance.NonContextMenu.SetActive(!UIManager.instance.NonContextMenu.activeSelf);
        }

        if (!following)
        {
            Vector3 pos = transform.position;
            if (Input.GetKey(KeyCode.W))
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


            if (Input.GetMouseButtonDown(2))
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
        else
        {
            if(followtarget != null)
            {
                float target_x = followtarget.transform.position.x;
                float target_y = followtarget.transform.position.y;
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, target_x, followsmooth * Time.deltaTime), Mathf.Lerp(transform.position.y, target_y, followsmooth * Time.deltaTime), transform.position.z);
                //transform.position = Vector3.Lerp(transform.position, followtarget.transform.position, Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    following = false;
                }
            }
            else
            {
                following = false;
            }
        }
    }

    
}
