using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class SlotPool : MonoBehaviour
    {
        [SerializeField] private RectTransform poolParent;
        [SerializeField] private Slot objectToPool;

        public static SlotPool Instance;
        private List<Slot> _pooledSlots;
        
        private int _poolAmount = 24;

        public static event Action OnPoolReady;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void OnValidate()
        {
            poolParent = GetComponent<RectTransform>();
        }

        private void Start()
        {
            PoolSlots();
        }

        private void PoolSlots()
        {
            _pooledSlots = new List<Slot>();
            for (int i = 0; i < _poolAmount; i++)
            {
                var tempSlot = Instantiate(objectToPool, poolParent.transform);
                _pooledSlots.Add(tempSlot);
            }
            
            OnPoolReady?.Invoke();
        }

        public RectTransform GetPoolTransform()
        {
            return poolParent;
        }

        public Slot GetAvailableSlot()
        {
            foreach (var slot in _pooledSlots)
            {
                if (!slot.GetActiveStatus())
                {
                    slot.SetIsActive(true);
                    return slot;
                }
            }

            throw new ArgumentNullException();
        }
    }
}
