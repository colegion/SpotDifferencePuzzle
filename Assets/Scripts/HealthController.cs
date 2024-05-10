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

    private void OnValidate()
    {
        heartParent = GetComponent<HorizontalLayoutGroup>();
    }

    private void Start()
    {
        SpawnHearts();
    }

    private void SpawnHearts()
    {
        for (int i = 0; i < HeartCount; i++)
        {
            var tempHeart = Instantiate(heart, heartParent.transform);
            DOVirtual.DelayedCall(i * SpawnDuration, () =>
            {
                tempHeart.transform.DOScale(new Vector3(1f, 1f, 1f), .9f).SetEase(Ease.OutBounce);
            });
            _spawnedHearts.Add(tempHeart);
        }
    }

    public void DecrementHeart()
    {
        var heartToCrack = _spawnedHearts[^1];
        heartToCrack.transform.DOShakeScale(1f, 1f).OnComplete(() =>
        {
            heartToCrack.DOColor(Color.white, .8f).OnComplete(() =>
            {
               heartToCrack.gameObject.SetActive(false);
               _spawnedHearts.Remove(heartToCrack);
            });
        });
    }
}