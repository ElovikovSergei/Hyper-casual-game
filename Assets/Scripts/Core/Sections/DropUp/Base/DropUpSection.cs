using System.Collections;
using Core.Inventory;
using UnityEngine;
using Tween;

namespace Core.Sections
{
    public abstract class DropUpSection : Section
    {
        public ItemType ItemType { get; private set; }

        private WaitForSeconds _dropDelay;

        public abstract void Initialize();
        protected abstract void OnDropItems(int dropItemsAmount);

        protected void Initialize(ItemType itemType)
        {
            ItemType = itemType;
            _dropDelay = new WaitForSeconds(0.025f);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (!other.TryGetComponent<InventoryController>(out var inventoryController))
                return;

            if (!inventoryController.Inventory.HasItems(ItemType))
                return;

            StartCoroutine(DropUpRoutine(inventoryController));
        }

        private IEnumerator DropUpRoutine(InventoryController inventoryController)
        {
            var dropItemsAmount = 0;

            while (inventoryController.Inventory.HasItems(ItemType))
            {
                var removingResult = inventoryController.Inventory.RemoveItems(ItemType, 1);

                if (!removingResult.IsSuccess)
                    continue;

                var transform = this.transform; // TO DO: get object from pool
                var time = Random.Range(0.4f, 0.6f);

                transform.DOScale(0f, time).SetEasing(Ease.Type.SineIn);
                transform.DOBezierFollow(this.transform, Random.Range(5f, 10f), Random.Range(-1f, 1f), Random.Range(0.4f, 0.6f))
                    .SetEasing(Ease.Type.SineIn)
                    .OnComplete(delegate
                    {
                        transform.localScale = Vector3.one;
                    });

                ++dropItemsAmount;

                yield return _dropDelay;
            }

            OnDropItems(dropItemsAmount);

            yield break;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}