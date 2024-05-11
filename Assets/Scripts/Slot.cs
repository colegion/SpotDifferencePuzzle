using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using Scriptables;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image slotImage;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Button slotButton;

    private Modifiable _modifiableParent;
    private bool _isActive;
    private void OnValidate()
    {
        rectTransform = GetComponent<RectTransform>();
        slotImage = GetComponent<Image>();
        slotButton = GetComponent<Button>();
    }

    public void SetSlot(Item item, Modifiable modifiable)
    {
        _modifiableParent = modifiable;
        slotImage.sprite = item.ItemSprite;
        slotImage.color = item.ItemColor;
        rectTransform.eulerAngles = item.ItemRotation;
        rectTransform.anchoredPosition = item.ItemPosition;
        rectTransform.sizeDelta = item.ItemSize;
        gameObject.SetActive(true);
        ToggleInteractable(true);
    }

    public void ResetSelf()
    {
        _modifiableParent = null;
        slotImage.sprite = null;
        slotImage.color = Color.white;
        rectTransform.eulerAngles = new Vector3(0, 0, 0);
        transform.SetParent(SlotPool.Instance.GetPoolTransform());
        SetIsActive(false);
        gameObject.SetActive(false);
    }

    public void SetIsActive(bool toggle)
    {
        _isActive = toggle;
    }

    public void ToggleInteractable(bool toggle)
    {
        slotButton.interactable = toggle;
    }

    public bool GetActiveStatus()
    {
        return _isActive;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress && eventData.pointerPress.GetComponent<Button>())
        {
            RectTransform clickedRectTransform = eventData.pointerPress.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(clickedRectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 converted);
            _modifiableParent.HandleOnSlotClick(converted);
        }
    }
}
