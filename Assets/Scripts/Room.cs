using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Room : MonoBehaviour
{
    private float m_Width;
    private float m_Height;

    [SerializeField] private GameObject m_BackgroundObject;
    private SpriteRenderer m_BackgroundRenderer;

    private Transform m_BackgroundTransform;

    [SerializeField] private BoxCollider2D m_FloorCollider;
    [SerializeField] private BoxCollider2D m_LeftCollider;
    [SerializeField] private BoxCollider2D m_RightCollider;

    private Color m_WhiteColor;
    private Color m_BlueColor;

    [SerializeField] private GameObject m_ItemsParent;

    private Dictionary<DimensionState, Item> m_ItemDictionary = new Dictionary<DimensionState, Item>();

    private void OnEnable()
    {
        DimensionManager.OnDimensionChanged += TransformRoom;
    }

    private void OnDisable()
    {
        DimensionManager.OnDimensionChanged -= TransformRoom;
    }

    private void Start()
    {
        InitializeRoom();
        CreateDimensionLists();
    }

    private void InitializeRoom()
    {
        m_BackgroundTransform = m_BackgroundObject.GetComponent<Transform>();
        m_BackgroundRenderer = m_BackgroundObject.GetComponent<SpriteRenderer>();

        m_Width = m_BackgroundTransform.localScale.x;
        m_Height = m_BackgroundTransform.localScale.y;

        m_FloorCollider.size = new Vector2(m_Width, 1f);
        m_LeftCollider.offset = new Vector2(((m_Width / 2f) + 0.5f) * -1f, -((m_Height / 2f) - 0.5f));
        m_RightCollider.offset = new Vector2(((m_Width / 2f) + 0.5f), -((m_Height / 2f) - 0.5f));
        m_FloorCollider.offset = new Vector2(0f, -((m_Height / 2f) + 0.5f));

        m_WhiteColor = new Color(255f, 255f, 255f, 255f);
        m_BlueColor = new Color(36f, 159, 222f, 255f);
    }

    private void CreateDimensionLists()
    {
        foreach (Transform child in m_ItemsParent.transform)
        {
            Item itemScript = child.GetComponent<Item>();

            itemScript.MyRoom = this;
            if (itemScript.DimensionData.Dimension != null)
            {
                m_ItemDictionary.Add(itemScript.DimensionData.Dimension, itemScript);
            }
            else
            {
                Debug.Log("Item wasn't assigned a dimension : " + child.gameObject.name);
                return;
            }
        }   
    }

    private void TransformRoom(DimensionData newData, DimensionData oldData)
    {
        List<Item> m_NewItems = new List<Item>();
        List<Item> m_OldItems = new List<Item>();

        m_NewItems.Add(m_ItemDictionary[newData.Dimension]);
        m_OldItems.Add(m_ItemDictionary[oldData.Dimension]);

        foreach (Item oldItem in m_OldItems)
        {
            oldItem.gameObject.SetActive(false);
        }

        m_BackgroundRenderer.color = newData.Color;

        foreach (Item newItem in m_NewItems)
        {
            newItem.gameObject.SetActive(true);
        }
    }

}
