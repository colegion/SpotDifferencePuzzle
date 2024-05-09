using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ModifiableController : MonoBehaviour
{
    [SerializeField] private RectTransform upperParent;
    [SerializeField] private RectTransform lowerParent;
    [SerializeField] private Slot slotPrefab;
    [SerializeField] private Modifiable[] modifiables;
    [SerializeField] private FeedbackController feedbackController;

    private List<Modifiable> _modifiedSlots = new List<Modifiable>();

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void Start()
    {
        DecideModifiableStatus();
        for (int i = 0; i < modifiables.Length; i++)
        {
            var modifiable = modifiables[i];
            var tempSlot = Instantiate(slotPrefab, upperParent);
            var tempPair = Instantiate(slotPrefab, lowerParent);
            modifiable.SetSlots(tempSlot, tempPair);
            modifiable.ConfigureSlots();
        }
    }
    
    private void DecideModifiableStatus()
    {
        for (int i = 0; i < modifiables.Length; i++)
        {
            if (ShouldChange())
            {
                _modifiedSlots.Add(modifiables[i]);
                modifiables[i].SetHasModified(true);
            }
        }
    }

    private void HandleModifiableClick(Modifiable modifiable)
    {
        feedbackController.GiveFeedback(modifiable);
        if (_modifiedSlots.Contains(modifiable))
        {
            _modifiedSlots.Remove(modifiable);
            modifiable.SetHasModified(false);
        }
    }
    
    private bool ShouldChange()
    {
        return Random.Range(0, 2) == 0;
    }

    private void AddListeners()
    {
        Modifiable.OnSlotClicked += HandleModifiableClick;
    }

    private void RemoveListeners()
    {
        Modifiable.OnSlotClicked -= HandleModifiableClick;
    }
}