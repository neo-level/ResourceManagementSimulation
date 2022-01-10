using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

/// <summary>
/// Base class for building on the map that hold a Resource inventory and that can be interacted with by Unit.
/// This Base class handle modifying the inventory of resources.
/// </summary>
public abstract class Building : MonoBehaviour,
    UIMainScene.IUIInfoContent
{
    //need to be serializable for the save system, so maybe added the attribute just when doing the save system
    [Serializable]
    public class InventoryEntry
    {
        public string resourceId;
        public int count;
    }

    [Tooltip("-1 is infinite")] public int inventorySpace = -1;

    protected List<InventoryEntry> m_Inventory = new List<InventoryEntry>();
    public List<InventoryEntry> Inventory => m_Inventory;

    protected int MCurrentAmount;

    //return 0 if everything fit in the inventory, otherwise return the left over amount
    public int AddItem(string resourceId, int amount)
    {
        //as we use the shortcut -1 = infinite amount, we need to actually set it to max value for computation following
        int maxInventorySpace = inventorySpace == -1 ? Int32.MaxValue : inventorySpace;

        if (MCurrentAmount == maxInventorySpace)
            return amount;

        int found = m_Inventory.FindIndex(item => item.resourceId == resourceId);
        int addedAmount = Mathf.Min(maxInventorySpace - MCurrentAmount, amount);

        //couldn't find an entry for that resource id so we add a new one.
        if (found == -1)
        {
            m_Inventory.Add(new InventoryEntry()
            {
                count = addedAmount,
                resourceId = resourceId
            });
        }
        else
        {
            m_Inventory[found].count += addedAmount;
        }

        MCurrentAmount += addedAmount;
        return amount - addedAmount;
    }

    //return how much was actually removed, will be 0 if couldn't get any.
    public int GetItem(string resourceId, int requestAmount)
    {
        int found = m_Inventory.FindIndex(item => item.resourceId == resourceId);

        //couldn't find an entry for that resource id so we add a new one.
        if (found != -1)
        {
            int amount = Mathf.Min(requestAmount, m_Inventory[found].count);
            m_Inventory[found].count -= amount;

            if (m_Inventory[found].count == 0)
            {
                //no more of that resources, so we remove it
                m_Inventory.RemoveAt(found);
            }

            MCurrentAmount -= amount;

            return amount;
        }

        return 0;
    }

    public virtual string GetName()
    {
        return gameObject.name;
    }

    public virtual string GetData()
    {
        return "";
    }

    public void GetContent(ref List<InventoryEntry> content)
    {
        content.AddRange(m_Inventory);
    }
}