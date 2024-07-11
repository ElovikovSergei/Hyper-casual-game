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

            if (!other.TryGetComponent<InventoryController>(out var inventory))
                return;

            if (!inventory.Inventory.HasItems(ItemType))
                return;

            StartCoroutine(DropUpRoutine(inventory));
        }

        private IEnumerator DropUpRoutine(InventoryController inventory)
        {
            var dropItemsAmount = 0;

            while (inventory.Inventory.HasItems(ItemType))
            {
                var removingResult = inventory.Inventory.RemoveItems(ItemType, 1);

                if (!removingResult.IsSuccess)
                    continue;

                var itemController = inventory.RemoveItemController(ItemType, 1);
                var time = Random.Range(0.4f, 0.6f);

                itemController.transform.DOScale(0f, time).SetEasing(Ease.Type.SineIn);
                itemController.transform.DOBezierFollow(transform, Random.Range(5f, 10f), Random.Range(-1f, 1f), time)
                    .SetEasing(Ease.Type.SineIn)
                    .OnComplete(delegate
                    {
                        itemController.gameObject.SetActive(false);
                        itemController.transform.ResetLocal();
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