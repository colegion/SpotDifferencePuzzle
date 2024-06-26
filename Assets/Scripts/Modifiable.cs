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
    public bool IsBackground;

    private bool _hasModified;
    private Vector2 _clickPosition;
    
    public static event Action<Modifiable> OnSlotClicked;

    public void ConfigureSlots()
    {
        if (_hasModified)
        {
            var itemTuple = ItemHolder.GetOppositeItems();
            slots[0].SetSlot(itemTuple.Item1, this);
            slots[1].SetSlot(itemTuple.Item2, this);
            Debug.Log("this is one of the modified ones" , slots[0].gameObject);
        }
        else
        {
            var item = ItemHolder.GetRandomItem();
            slots[0].SetSlot(item, this);
            slots[1].SetSlot(item, this);
        }
    }

    public void SetSlots(Slot slot, Slot pairSlot)
    {
        slots = new[] { slot, pairSlot };
    }

    public void ResetSlots()
    {
        slots[0].ResetSelf();
        slots[1].ResetSelf();
    }

    public void HandleOnSlotClick(Vector2 clickPos)
    {
        if (!IsBackground)
        {
            slots[0].ToggleInteractable(false);
            slots[1].ToggleInteractable(false);
        }

        _clickPosition = clickPos;
        OnSlotClicked?.Invoke(this);
    }

    public Vector2 GetClickPos()
    {
        return _clickPosition;
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