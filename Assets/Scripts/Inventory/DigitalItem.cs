﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base class for storeslots and inventory slots
public abstract class DigitalItem : MonoBehaviour
{
    [SerializeField] protected ItemData m_ItemInfo;
    public ItemData ItemInfo
    {
        get { return m_ItemInfo; }
        set { m_ItemInfo = value; }
    }

    protected bool m_SlotIsTaken = false;
    public bool SlotIsTaken
    {
        get { return m_SlotIsTaken; }
        set { m_SlotIsTaken = value; }
    }

    protected int m_SlotAmount;
    public int SlotAmount
    {
        get { return m_SlotAmount; }
        set { m_SlotAmount = value; }
    }

    protected Image m_SlotImage;
    public Image SlotImage
    {
        get { return m_SlotImage; }
        set { m_SlotImage = value; }
    }

    [SerializeField] protected Image m_BackGroundImage;
    [SerializeField] protected TMPro.TextMeshProUGUI m_SlotAmountText;
    [SerializeField] protected GameObject m_DescriptionBox;
    [SerializeField] protected TMPro.TextMeshProUGUI m_DescriptionTitle;
    [SerializeField] protected TMPro.TextMeshProUGUI m_DescriptionText;

    protected Color m_LightUpSlotColor = new Color(178 / 255f, 106f / 255f, 63 / 255f);
    protected Color m_SlotNotTakenColor = new Color(1f, 1f, 1f, 0f);
    protected Color m_SlotTakenColor = new Color(1f, 1f, 1f, 1f);

    private void Start()
    {
        m_SlotImage = GetComponent<Image>();
    }

    public virtual void FillSlot(ItemData itemInfo, int amount)
    {
        m_ItemInfo = itemInfo;

        //Debug.Log(objectData + "objectdata fillslot");
        //m_SlotImage.color = m_SlotTakenColor;
        m_SlotImage.sprite = itemInfo.Icon;
        SetAmount(amount);
        m_SlotIsTaken = true;
    }

    public virtual void ResetSlot()
    {
        m_ItemInfo = null;
        //m_SlotImage.color = m_SlotNotTakenColor;
        SetImage(null);
        m_SlotIsTaken = false;
        SetAmount(0);
    }

    public void SetImage(Sprite image)
    {
        m_SlotImage.sprite = image;
    }

    public void SetAmount(int amount)
    {
        m_SlotAmount = amount;

        if (m_SlotAmount == 0)
        {
            m_SlotAmountText.text = "";
        }
        else
        {
            m_SlotAmountText.text = m_SlotAmount.ToString();
        }
    }

    public void IncreaseAmount(int amount)
    {
        m_SlotAmount += amount;
        m_SlotAmountText.text = m_SlotAmount.ToString();
    }

    public void DecreaseAmount(int amount)
    {
        m_SlotAmount -= amount;
        m_SlotAmountText.text = m_SlotAmount.ToString();
    }

    public void LightUpSlot(bool lightUp)
    {
        if (lightUp)
        {
            m_BackGroundImage.color = m_LightUpSlotColor;
        }
        else
        {
            m_BackGroundImage.color = m_SlotTakenColor;
        }
    }

}
