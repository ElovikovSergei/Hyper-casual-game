using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Collections;
using Core.Inventory;
using UnityEngine;
using System;
using Tween;

namespace Core.Sections
{
    public abstract class PickUpSection : Section
    {
        public ItemType ItemType { get; private set; }
        public Guid Id { get; private set; }

        [SerializeField] private Transform[] _stackElements;
        private List<Transform> _activeItems;
        private WaitForSeconds _spawnDelay;

        private readonly Vector3 _defaultItemSize = new Vector3(1, 1.5f, 1);
        private int _collectedItemsAmount;
        private bool _isAnimationPlaying;

        public abstract void Initialize();
        protected abstract void OnPickUp(int collectedItems);

        public void AddItems(int amount)
        {
            if (amount <= 0)
                return;

            _isAnimationPlaying = true;
            _collectedItemsAmount += amount;

            StartCoroutine(SpawnItemsRoutine(amount));
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (_isAnimationPlaying || _collectedItemsAmount == 0)
                return;

            if (!other.gameObject.TryGetComponent<InventoryController>(out var inventory))
                return;

            GetItems(inventory);
        }

        protected void Initialize(ItemType itemType)
        {
            Id = Guid.NewGuid();
            ItemType = itemType;
            _spawnDelay = new WaitForSeconds(0.025f);
        }

        private IEnumerator SpawnItemsRoutine(int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                if (_activeItems.Count >= _stackElements.Length)
                    break;

                var item = FactoriesController.Instance.ItemFactory.GetInstance(ItemType);

                item.transform.ResetLocal();

                item.transform.position = _stackElements[_collectedItemsAmount].position;
                item.transform.rotation = _stackElements[_collectedItemsAmount].rotation;
                item.transform.localScale = Vector3.zero;

                item.gameObject.SetActive(true);
                item.transform.DOScale(_defaultItemSize, 0.1f).SetEasing(Ease.Type.BackOut);

                ++_collectedItemsAmount;
                _activeItems.Add(item.transform);

                yield return _spawnDelay;
            }

            _isAnimationPlaying = false;

            yield break;
        }

        private void GetItems(InventoryController inventory)
        {
            var collectedItemsAmount = _collectedItemsAmount;
            var activeItems = _activeItems;

            for (var i = collectedItemsAmount - 1; i >= 0; --i)
            {
                var transform = activeItems[i];
                var time = Random.Range(0.4f, 0.6f);

                transform.DOScale(0f, time).SetEasing(Ease.Type.SineIn);
                transform.DOBezierFollow(inventory.ItemsInstanceParent, Random.Range(5f, 10f), Random.Range(-1f, 1f), time)
                    .SetEasing(Ease.Type.SineIn)
                    .OnComplete(delegate
                    {
                        transform.gameObject.SetActive(false);
                        transform.ResetLocal();

                        inventory.Inventory.AddItems(ItemType, 1);
                    });
            }

            OnPickUp(_collectedItemsAmount);
            ResetData();
        }

        private void ResetData()
        {
            _collectedItemsAmount = 0;
            _activeItems.Clear();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}