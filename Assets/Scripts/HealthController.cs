using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static Helpers.LevelProgressUIHelper;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup heartParent;
    [SerializeField] private Image heart;

    private const int HeartCount = 3;
    private List<Image> _spawnedHearts = new List<Image>();
    private FeedbackController _feedbackController;

    private void OnValidate()
    {
        heartParent = GetComponent<HorizontalLayoutGroup>();
    }
    
    public void InjectFeedbackController(FeedbackController controller)
    {
        _feedbackController = controller;
    }

    public void SpawnHearts()
    {
        for (int i = 0; i < HeartCount; i++)
        {
            DOVirtual.DelayedCall(i * SpawnDuration, () =>
            {
                var tempHeart = Instantiate(heart, heartParent.transform);
                tempHeart.transform.DOScale(new Vector3(1f, 1f, 1f), 2f).SetEase(Ease.OutBounce);
                _spawnedHearts.Add(tempHeart);
            });
        }
    }

    public void DecrementHeart()
    {
        var heartToCrack = _spawnedHearts[^1];
        heartToCrack.transform.DOShakeScale(1f, .65f).OnComplete(() =>
        {
            heartToCrack.DOColor(Color.white, .6f).OnComplete(() =>
            {
                heartToCrack.gameObject.SetActive(false);
                _spawnedHearts.Remove(heartToCrack);
                if(_spawnedHearts.Count == 0)
                    _feedbackController.HandleOnLevelFinished(false);
            });
        });
    }

    public void DestroyHearts()
    {
        foreach (var helper in _spawnedHearts)
        {
            Destroy(helper.gameObject);
        }

        _spawnedHearts = new List<Image>();
    }
}
