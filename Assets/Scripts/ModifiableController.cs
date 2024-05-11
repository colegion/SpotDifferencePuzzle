using System;
using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class ModifiableController : MonoBehaviour
{
    [SerializeField] private RectTransform upperParent;
    [SerializeField] private RectTransform lowerParent;
    [SerializeField] private Modifiable[] modifiables;
    [SerializeField] private FeedbackController feedbackController;

    private List<Modifiable> _modifiedSlots = new List<Modifiable>();
    private int _originalChangeCount;
    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void CreateLevel()
    {
        DecideModifiableStatus();
        for (int i = 0; i < modifiables.Length; i++)
        {
            var modifiable = modifiables[i];
            var tempSlot = SlotPool.Instance.GetAvailableSlot();
            tempSlot.transform.SetParent(upperParent);
            var tempPair = SlotPool.Instance.GetAvailableSlot();
            tempPair.transform.SetParent(lowerParent);
            modifiable.SetSlots(tempSlot, tempPair);
            modifiable.ConfigureSlots();
        }
        feedbackController.InjectModifiableController(this);
        feedbackController.SpawnProgressUnits(_originalChangeCount);
    }
    
    private void DecideModifiableStatus()
    {
        for (int i = 1; i < modifiables.Length; i++)
        {
            if (ShouldChange())
            {
                _originalChangeCount++;
                _modifiedSlots.Add(modifiables[i]);
                modifiables[i].SetHasModified(true);
            }
        }
    }

    private void HandleModifiableClick(Modifiable modifiable)
    {
        if (_modifiedSlots.Contains(modifiable))
        {
            _modifiedSlots.Remove(modifiable);
        }
        feedbackController.GiveFeedback(modifiable);
        modifiable.SetHasModified(false);
    }
    
    public void RestartGame()
    {
        DOTween.KillAll();
        feedbackController.RestartFeedbackFields();
        _originalChangeCount = 0;
        _modifiedSlots = new List<Modifiable>();
        foreach (var modifiable in modifiables)
        {
            modifiable.ResetSlots();
        }
        CreateLevel();
    }

    public int GetProgressIndex()
    {
        return _originalChangeCount - _modifiedSlots.Count - 1;
    }
    
    private bool ShouldChange()
    {
        return Random.Range(0, 2) == 0;
    }

    private void AddListeners()
    {
        Modifiable.OnSlotClicked += HandleModifiableClick;
        SlotPool.OnPoolReady += CreateLevel;
    }

    private void RemoveListeners()
    {
        Modifiable.OnSlotClicked -= HandleModifiableClick;
        SlotPool.OnPoolReady -= CreateLevel;
    }
}