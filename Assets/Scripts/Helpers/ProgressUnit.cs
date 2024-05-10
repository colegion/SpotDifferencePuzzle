using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class ProgressUnit : MonoBehaviour
    {
        [SerializeField] private Image unitImage;
        [SerializeField] private Color successColor;
        
        private void OnValidate()
        {
            unitImage = GetComponent<Image>();
        }

        public void InitializeSelf(float delay)
        {
            DOVirtual.DelayedCall(delay, () =>
            {
                transform.DOScale(new Vector3(1f, 1f, 1f), 2f).SetEase(Ease.OutBounce);
            });
        }

        public void SetUnitCompleted()
        {
            unitImage.color = successColor;
            AnimateUnit();
        }

        private void AnimateUnit()
        {
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .5f).SetEase(Ease.OutBack).SetLoops(2, LoopType.Yoyo);
        }
    }
}
