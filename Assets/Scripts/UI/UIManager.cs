using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    [Inject] private MainMenuView _mainMenuView;
    [Inject] private WeatherView _weatherView;
    [Inject] private DogBreedsView _dogBreedsView;
    [Inject] private WeatherController _weatherController;
    [Inject] private DogBreedsController _dogBreedsController;

    private void Start()
    {
        _mainMenuView.OnShowWeather += ActivateWeather;
        _mainMenuView.OnShowDogBreeds += ActivateBreeds;

        ActivateWeather();
    }

    private void ActivateWeather()
    {
        _dogBreedsController.Deactivate();
        _weatherController.Activate();
    }

    private void ActivateBreeds()
    {
        _weatherController.Deactivate();
        _dogBreedsController.Activate();
    }
    
    private void OnDestroy()
    {
        _mainMenuView.OnShowWeather -= ActivateWeather;
        _mainMenuView.OnShowDogBreeds -= ActivateBreeds;
    }
}