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

    //public delegate void PlayerEnteredDoor(Door EnteredDoor);
    //public static event PlayerEnteredDoor OnPlayerEnteredDoor;

    public delegate void PlayerTransformed();
    public static event PlayerTransformed OnPlayerTransformed;

    private Rigidbody2D m_RB;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;

    [SerializeField] private float m_Speed;
    private float m_HorizontalMove;
    private bool m_Running;

    private Color m_WhiteColor;
    private Color m_BlackColor;

   // public bool m_OnDoor;
    //public Door m_CollidingDoor;
    private bool m_EnteringDoor;

	private LayerMask m_TileLayer;

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

    private void OnEnable()
    {
        Door.OnDoorEntered += MoveToNewRoom;
    }

    private void OnDisable()
    {
        Door.OnDoorEntered -= MoveToNewRoom;
    }

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();

        m_WhiteColor = new Color(255f, 255, 255f, 255f);
        m_BlackColor = new Color(0f, 0f, 0f, 255f);

        m_Animator.SetFloat("Running", m_Speed);
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnPlayerTransformed?.Invoke();
        }
    }

    void FixedUpdate()
    {

        m_HorizontalMove = Input.GetAxisRaw("Horizontal") * m_Speed;

        if (m_HorizontalMove < 0)
        {
            m_SpriteRenderer.flipX = true;
            m_RB.velocity = new Vector2(-m_Speed, m_RB.velocity.y);
        }
        if (m_HorizontalMove > 0)
        {
            m_SpriteRenderer.flipX = false;
            m_RB.velocity = new Vector2(m_Speed, m_RB.velocity.y);
        }

        m_Animator.SetFloat("Speed", Mathf.Abs(m_HorizontalMove));
    }

    private void MoveToNewRoom(Door m_EnteredDoor)
    {
        m_EnteringDoor = true;

        float xPos = m_EnteredDoor.LinkedDoor.transform.position.x;
        float yPos = m_EnteredDoor.LinkedDoor.transform.position.y;

        this.transform.position = new Vector2(xPos, yPos);
        m_EnteredDoor = m_EnteredDoor.LinkedDoor;
        StartCoroutine("WaitBeforeEnterRoom");
    }

    private IEnumerator WaitBeforeEnterRoom()
    {
        yield return null;
        m_EnteringDoor = false;
    }

	public GameObject FindStandingTile()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_TileLayer);
		return hit.collider.gameObject;
	}

	//private void PlayerOnDoor()
	//{
	//    m_OnDoor = true;
	//   // m_SpriteRenderer.color = m_WhiteColor;
	//}

	//private void PlayerNotOnDoor()
	//{
	//    m_OnDoor = false;
	//   // m_SpriteRenderer.color = m_BlackColor;
	//}

	//private void OnTriggerEnter2D(Collider2D other)
	//{
	//    if (other.CompareTag("Door"))
	//    {
	//        m_CollidingDoor = other.GetComponent<Door>();
	//        PlayerOnDoor();
	//    }
	//}

	//private void OnTriggerExit2D(Collider2D other)
	//{
	//    if (other.CompareTag("Door"))
	//    {
	//        if (m_EnteringDoor == false)
	//        {
	//            PlayerNotOnDoor();
	//        }
	//    }
	//}

	//private void ChangeColor(DimensionData newState, DimensionData oldState)
	//{
	//    if (m_OnDoor == true)
	//    {
	//        PlayerNotOnDoor();
	//    }
	//}
}
