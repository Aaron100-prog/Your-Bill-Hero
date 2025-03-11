using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playercontroller : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    private float targetzoom = 5f;
    public Camera playercamera;

    public Sprite CharacterFrontSprite;
    public Sprite CharacterBackSprite;
    public SpriteRenderer CharacterSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-2, 2f, 1f);
        }
        else if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(2, 2f, 1f);
        }

        if (rb.velocity.y < 0)
        {
            CharacterSpriteRenderer.sprite = CharacterFrontSprite;
        }
        else if (rb.velocity.y > 0)
        {
            CharacterSpriteRenderer.sprite = CharacterBackSprite;
        }

        //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 5f;
        }
        else
        {
            speed = 2f;
        }

        //Zoom
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel") * 5;
            targetzoom = targetzoom - zoom;
        }
        if (targetzoom < 2f)
        {
            targetzoom = 2f;
        }
        if (targetzoom > 20)
        {
            targetzoom = 20;
        }
        playercamera.orthographicSize = Mathf.Lerp(playercamera.orthographicSize, targetzoom, Time.deltaTime * 7.5f);

        //Interaction
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = playercamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Debug.Log(hit);
            if (hit.collider != null)
            {
                Clickable clickablescript = hit.transform.gameObject.GetComponent<Clickable>();
                Debug.Log(clickablescript);
                if (clickablescript != null)
                {
                    clickablescript.Click(true);
                }
            }
        }


        //Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(InventoryStorageManager.instance.Personalinventoryentries.Count);
            for (int i = 0; i < InventoryStorageManager.instance.Personalinventoryentries.Count; i++)
            {
                Debug.Log(InventoryStorageManager.instance.Personalinventoryentries[i].identifier + ": " + InventoryStorageManager.instance.Personalinventoryentries[i].amount);
            }
            
        }
    }


    public static playercontroller instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
