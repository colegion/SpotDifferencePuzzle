using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Button slotButton;

    private Modifiable _modifiableParent;
    private void OnValidate()
    {
        rectTransform = GetComponent<RectTransform>();
        slotImage = GetComponent<Image>();
        slotButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    public void SetSlot(Item item, Modifiable modifiable)
    {
        _modifiableParent = modifiable;
        slotImage.sprite = item.ItemSprite;
        rectTransform.anchoredPosition = item.ItemPosition;
        rectTransform.sizeDelta = item.ItemSize;
    }

    private void HandleOnClick()
    {
        _modifiableParent.HandleOnSlotClick();
    }

    private void AddListeners()
    {
        slotButton.onClick.AddListener(HandleOnClick);
    }

    private void RemoveListeners()
    {
        slotButton.onClick.RemoveListener(HandleOnClick);
    }
}
