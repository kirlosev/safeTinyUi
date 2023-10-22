using Bottle.Scripts;
using UnityEngine;
using Zenject;

namespace Installers.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private BottleService _bottleService;

        public override void InstallBindings()
        {
            Container
                .Bind<BottleService>()
                .FromComponentInNewPrefab(_bottleService)
                .WithGameObjectName(nameof(BottleService))
                .AsSingle()
                .NonLazy();
        }
    }
}
