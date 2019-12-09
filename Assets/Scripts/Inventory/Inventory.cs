using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : SlotsHolder
{
    private static Inventory m_Instance;
    public static Inventory Instance
    {
        get { return m_Instance; }
    }

    public delegate void ItemDropped(ItemData itemInfo);
    public static event ItemDropped OnItemDropped;

    [SerializeField] private GameObject m_InventorySlotPrefab;
    [SerializeField] private int m_InventorySlotAmount;

    [SerializeField] private List<InventorySlot> m_InventoryList = new List<InventorySlot>();
    public List<InventorySlot> InventoryList
    {
        get { return m_InventoryList; }
        set { m_InventoryList = value; }
    }

    private InventorySlot m_SelectedSlot;
    public InventorySlot SelectedSlot
    {
        get { return m_SelectedSlot; }
        set { m_SelectedSlot = value; }
    }

    private KeyCode[] m_KeyCodes =
    {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8
     };


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
        SpawnInventorySlots();
    }

    private void SpawnInventorySlots()
    {
        for (int i = 0; i < m_InventorySlotAmount; i++)
        {
            GameObject slotPrefab = Instantiate(m_InventorySlotPrefab, this.transform);
            InventorySlot inventorySlot = slotPrefab.transform.GetChild(0).GetComponent<InventorySlot>();

            // Also add it to the slotsholder
            m_SlotList.Add(inventorySlot);

            inventorySlot.HotKeyText.text = (i + 1).ToString();
            m_InventoryList.Add(inventorySlot);
        }

        m_SelectedSlot = m_InventoryList[0]; // Select the first item
        m_SelectedSlot.SelectSlot(true);
    }

    private void Update()
    {
        CheckForKeyInput();
    }

    private void CheckForKeyInput()
    {
        for (int i = 0; i < m_KeyCodes.Length; i++)
        {
            if (Input.GetKeyDown(m_KeyCodes[i]))
            {
                int numberPressed = i + 1;

                SetSlotSelected(m_InventoryList[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DropItem(m_SelectedSlot);
        }
    }

    private void DropItem(InventorySlot slot)
    {
        if (!slot.SlotIsTaken)
        {
            return;
        }
        else
        {
            //GameObject tile = PlayerController.Instance.FindStandingTile();
            //float height = tile.GetComponent<Renderer>().bounds.size.y;
            //Instantiate(slot.ObjectData.Prefab, new Vector3(PlayerController.Instance.GetPlayerPosition().position.x, tile.transform.position.y + height / 2, tile.transform.position.z), transform.rotation);
            //OnItemDropped?.Invoke(slot.ItemData);
            //RemoveItem(m_SelectedSlot);
        }
    }

    public void SetSlotSelected(InventorySlot slot)
    {
        m_SelectedSlot.SelectSlot(false); //  Deselect old slot
        m_SelectedSlot = slot; // Set new slot
        m_SelectedSlot.SelectSlot(true); // Select new slot
    }

    protected override void FillSlot(ItemData itemInfo, int amount)
    {
        base.FillSlot(itemInfo, amount);
    }
}
