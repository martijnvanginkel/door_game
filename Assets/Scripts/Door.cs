using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
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

    [SerializeField] private Sprite m_OpenDoor;
    [SerializeField] private Sprite m_ClosedDoor;
    [SerializeField] private GameObject m_TextObject;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_DoorCollider = GetComponent<BoxCollider2D>();
        m_SpriteRenderer.sprite = m_ClosedDoor;
        m_TextObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_SpriteRenderer.sprite = m_OpenDoor;
            m_TextObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_SpriteRenderer.sprite = m_ClosedDoor;
            m_TextObject.SetActive(false);
        }
    }
}
