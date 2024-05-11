using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour
{
    [SerializeField] private Image feedbackPrefab;
    [SerializeField] private Sprite trialSuccessCircle;
    [SerializeField] private Sprite trialFailedCircle;
    [SerializeField] private LevelProgressUIHelper progressUIHelper;
    [SerializeField] private HealthController healthController;

    [SerializeField] private ParticleSystem[] successConfetti;

    private ModifiableController _modifiableController;

    private List<Image> _spawnedFeedbackIcons = new List<Image>();

    public void InjectModifiableController(ModifiableController controller)
    {
        _modifiableController = controller;
        progressUIHelper.InjectFeedbackController(this);
        healthController.InjectFeedbackController(this);
        healthController.SpawnHearts();
    }
    public void GiveFeedback(Modifiable modifiable)
    {
        var slots = modifiable.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            var feedback = Instantiate(feedbackPrefab, slots[i].transform.parent);
            if (modifiable.IsBackground)
            {
                feedback.GetComponent<RectTransform>().anchoredPosition = modifiable.GetClickPos();
            }
            else
            {
                feedback.GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
            }
            _spawnedFeedbackIcons.Add(feedback);
            feedback.sprite = modifiable.GetModifiedStatus() ? trialSuccessCircle : trialFailedCircle;
            if (modifiable.GetModifiedStatus())
            {
                progressUIHelper.CompleteUnitByIndex(_modifiableController.GetProgressIndex());
            }
            else
            {
                healthController.DecrementHeart();
            }
            
        }
    }

    public void SpawnProgressUnits(int count)
    {
        progressUIHelper.SpawnUnits(count);
    }

    public void HandleOnLevelFinished(bool isSuccess)
    {
        if (isSuccess)
        {
            for (int i = 0; i < successConfetti.Length; i++)
            {
                successConfetti[i].Play();
            } 
        }

        DOVirtual.DelayedCall(1.5f, () =>
        {
            _modifiableController.RestartGame();
        });
    }

    public void RestartFeedbackFields()
    {
        progressUIHelper.ResetUnits();
        healthController.DestroyHearts();
        foreach (var feedback in _spawnedFeedbackIcons)
        {
            Destroy(feedback.gameObject);
        }

        _spawnedFeedbackIcons = new List<Image>();
    }
}
