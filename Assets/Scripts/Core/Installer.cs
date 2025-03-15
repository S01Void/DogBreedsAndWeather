using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    [SerializeField] private DogBreedItemView dogBreedItemPrefab;
    [SerializeField] private Transform breedsContent; 
    public override void InstallBindings()
    {
        // Core
        Container.Bind<ApiRequestQueue>().AsSingle();

        // Сервисы
        Container.Bind<HttpRequestHandler>().AsSingle();
        Container.Bind<WeatherService>().AsSingle();
        Container.Bind<DogBreedsService>().AsSingle();

        // Контроллеры
        Container.Bind<WeatherController>().AsSingle();
        Container.Bind<DogBreedsController>().AsSingle();

        // UI-компоненты
        Container.Bind<WeatherView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DogBreedsView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MainMenuView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BreedDetailsPopupView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LoadingIndicator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PopupManager>().FromComponentInHierarchy().AsSingle();
        
        Container.BindFactory<DogBreedItemView, DogBreedItemView.Factory>()
            .FromComponentInNewPrefab(dogBreedItemPrefab)
            .UnderTransform(breedsContent);
    }
}
