using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour
{
    [SerializeField] private Image feedbackPrefab;
    [SerializeField] private Sprite trialSuccessCircle;
    [SerializeField] private Sprite trialFailedCircle;

    public void GiveFeedback(Modifiable modifiable)
    {
        var slots = modifiable.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            var feedback = Instantiate(feedbackPrefab, slots[i].transform.parent);
            feedback.sprite = modifiable.GetModifiedStatus() ? trialSuccessCircle : trialFailedCircle;
            feedback.GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
