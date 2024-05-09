using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scriptables;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Modifiable
{
    private Slot[] slots;
    public SlotItems ItemHolder;

    private bool _hasModified;

    public static event Action<List<Vector2>, bool> OnTrialHappened;
    public static event Action<Modifiable> OnSlotClicked;

    public void ConfigureSlots()
    {
        if (_hasModified)
        {
            var itemTuple = ItemHolder.GetOppositeItems();
            //@todo: make this return list for iterating.
            slots[0].SetSlot(itemTuple.Item1, this);
            slots[1].SetSlot(itemTuple.Item2, this);
        }
        else
        {
            var item = ItemHolder.GetRandomItem();
            foreach (var slot in slots)
            {
                slot.SetSlot(item, this);
            }
        }
    }

    public void SetSlots(Slot slot, Slot pairSlot)
    {
        slots = new[] { slot, pairSlot };
    }

    public void HandleOnSlotClick()
    {
        OnSlotClicked?.Invoke(this);
        
        var positionList = new List<Vector2>()
        {
            slots[0].GetComponent<RectTransform>().anchoredPosition,
            slots[1].GetComponent<RectTransform>().anchoredPosition
        };
        OnTrialHappened?.Invoke(positionList, _hasModified);
    }

    public void SetHasModified(bool modify)
    {
        _hasModified = modify;
    }

    public bool GetModifiedStatus()
    {
        return _hasModified;
    }

    public Slot[] GetSlots()
    {
        return slots;
    }
}