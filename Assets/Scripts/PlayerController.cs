using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController m_Instance;
    public static PlayerController Instance
    {
        get { return m_Instance; }
    }

    public delegate void PlayerEnteredDoor(Door EnteredDoor);
    public static event PlayerEnteredDoor OnPlayerEnteredDoor;

    public delegate void PlayerTransformed();
    public static event PlayerTransformed OnPlayerTransformed;

    private Rigidbody2D m_RB;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private float m_Speed;

    private Color m_WhiteColor;
    private Color m_BlackColor;

    public bool m_OnDoor;
    public Door m_CollidingDoor;
    private bool m_EnteringDoor;

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_WhiteColor = new Color(255f, 255, 255f, 255f);
        m_BlackColor = new Color(0f, 0f, 0f, 255f);
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.W) && m_OnDoor)
        {
            m_EnteringDoor = true;
            OnPlayerEnteredDoor?.Invoke(m_CollidingDoor);
            MoveToNewRoom();
        }    
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnPlayerTransformed?.Invoke();
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_RB.velocity = new Vector2(-m_Speed, m_RB.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_RB.velocity = new Vector2(m_Speed, m_RB.velocity.y);
        }
    }

    private void MoveToNewRoom()
    {
        float xPos = m_CollidingDoor.LinkedDoor.transform.position.x;
        float yPos = m_CollidingDoor.LinkedDoor.transform.position.y;

        this.transform.position = new Vector2(xPos, yPos);
        m_CollidingDoor = m_CollidingDoor.LinkedDoor;
        StartCoroutine("WaitForDoor");
    }

    private IEnumerator WaitForDoor()
    {
        yield return null;
        m_EnteringDoor = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            m_CollidingDoor = other.GetComponent<Door>();
            m_OnDoor = true;
            m_SpriteRenderer.color = m_WhiteColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            if (m_EnteringDoor == false)
            {
                m_OnDoor = false;
                m_SpriteRenderer.color = m_BlackColor;
            }
        }
    }
}
