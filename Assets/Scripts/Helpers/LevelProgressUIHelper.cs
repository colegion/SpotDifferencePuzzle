using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class LevelProgressUIHelper : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup unitParent;
        [SerializeField] private ProgressUnit unitPrefab;

        private const float SpawnDuration = .5f;
        
        private void OnValidate()
        {
            unitParent = GetComponent<HorizontalLayoutGroup>();
        }

        public void SpawnUnits(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var tempUnit = Instantiate(unitPrefab, unitParent.transform);
                tempUnit.InitializeSelf(i * SpawnDuration, i);
            }
        }
    }
}
