using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemTuple
{
    private Dimension m_InDimension;
    public Dimension InDimension
    {
        get { return m_InDimension; }
        set { m_InDimension = value; }
    }

    private Item m_MyItem;
    public Item MyItem
    {
        get { return m_MyItem; }
        set { m_MyItem = value; }
    }
}

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

    [SerializeField] private GameObject m_ItemsParent;

    private List<ItemTuple> m_ItemTupleList = new List<ItemTuple>();
    public List<ItemTuple> ItemTupleList
    {
        get { return m_ItemTupleList; }
        set { m_ItemTupleList = value; }
    }

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
        StartSituation();
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
    }

    private void CreateDimensionLists()
    {
        foreach (Transform child in m_ItemsParent.transform)
        {
            Item itemScript = child.GetComponent<Item>();
            itemScript.MyRoom = this;

            ItemTuple newItemTuple = new ItemTuple();
            newItemTuple.InDimension = itemScript.DimensionData.Dimension;
            newItemTuple.MyItem = itemScript;
            m_ItemTupleList.Add(newItemTuple);
        }   
    }

    private void TransformRoom(DimensionData newData, DimensionData oldData)
    {
        foreach (ItemTuple itemTuple in m_ItemTupleList)
        {
            if (itemTuple.InDimension == oldData.Dimension)
            {
                itemTuple.MyItem.gameObject.SetActive(false);
            }
            else if (itemTuple.InDimension == newData.Dimension)
            {
                itemTuple.MyItem.gameObject.SetActive(true);
            }
        }
        m_BackgroundRenderer.color = newData.Color;
    }

    private void StartSituation()
    {
        foreach (ItemTuple itemTuple in m_ItemTupleList)
        {
            if (itemTuple.InDimension == Dimension.Second)
            {
                itemTuple.MyItem.gameObject.SetActive(false);
            }
        }
    }

}
