using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class ProgressUnit : MonoBehaviour
    {
        [SerializeField] private Image unitImage;
        [SerializeField] private Color successColor;

        private int _unitIndex = -1;
        private void OnValidate()
        {
            unitImage = GetComponent<Image>();
        }

        public void InitializeSelf(float animTime, int index)
        {
            _unitIndex = index;
        }

        public void SetUnitCompleted()
        {
            unitImage.color = successColor;
            AnimateUnit();
        }

        private void AnimateUnit()
        {
        }
    }
}
