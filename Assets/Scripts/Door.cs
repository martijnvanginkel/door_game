using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    public delegate void DoorEntered(Door EnteredDoor);
    public static event DoorEntered OnDoorEntered;

    private SpriteRenderer m_SpriteRenderer;

    private BoxCollider2D m_DoorCollider;
    public BoxCollider2D DoorCollider
    {
        get { return m_DoorCollider; }
        set { m_DoorCollider = value; }
    }

    [SerializeField] private Door m_LinkedDoor;
    public Door LinkedDoor
    {
        get { return m_LinkedDoor; }
        set { m_LinkedDoor = value; }
    }

    private bool m_PlayerOnDoor;

    [SerializeField] private Sprite m_OpenDoor;
    [SerializeField] private Sprite m_ClosedDoor;
    [SerializeField] private GameObject m_TextObject;

    private void OnEnable()
    {
        DimensionManager.OnDimensionChanged += DimensionChanged;
    }

    private void OnDisable()
    {
        DimensionManager.OnDimensionChanged -= DimensionChanged;
    }

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_DoorCollider = GetComponent<BoxCollider2D>();
        m_SpriteRenderer.sprite = m_ClosedDoor;
        m_TextObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && m_PlayerOnDoor)
        {
            EnterDoor();
        }
    }

    protected virtual void EnterDoor()
    {
        OnDoorEntered?.Invoke(this);
    }

    private void OpenDoor()
    {
        m_PlayerOnDoor = true;
        m_SpriteRenderer.sprite = m_OpenDoor;
        m_TextObject.SetActive(true);
    }

    private void CloseDoor()
    {
        m_PlayerOnDoor = false;
        m_SpriteRenderer.sprite = m_ClosedDoor;
        m_TextObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    private void DimensionChanged(DimensionData newData, DimensionData oldData)
    {
        if (m_DimensionData.Dimension == oldData.Dimension)
        {
            CloseDoor();
        }
    }
}
