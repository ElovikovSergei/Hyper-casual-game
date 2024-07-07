using Core.Factories;

namespace Core
{
    public sealed class FactoriesController : MonoBehaviourSingleton<FactoriesController>
    {
        public ItemFactory ItemFactory { get; private set; }

        private void Initialize()
        {
            ItemFactory = new ItemFactory();
        }

        private void Awake()
        {
            Instance = this;
            Initialize();
        }
    }
}