using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button weatherButton;
    [SerializeField] private Button dogBreedsButton;
    [SerializeField] private GameObject weatherPanel;
    [SerializeField] private GameObject dogBreedsPanel;

    public event Action OnShowWeather;
    public event Action OnShowDogBreeds;

    private void Start()
    {
        if (weatherButton != null)
            weatherButton.onClick.AddListener(ShowWeather);

        if (dogBreedsButton != null)
            dogBreedsButton.onClick.AddListener(ShowDogBreeds);

        ShowWeather();
    }

    private void ShowWeather()
    {
        if (weatherPanel != null)
            weatherPanel.SetActive(true);

        if (dogBreedsPanel != null)
            dogBreedsPanel.SetActive(false);

        OnShowWeather?.Invoke();
    }

    private void ShowDogBreeds()
    {
        if (dogBreedsPanel != null)
            dogBreedsPanel.SetActive(true);

        if (weatherPanel != null)
            weatherPanel.SetActive(false);

        OnShowDogBreeds?.Invoke();
    }

    private void OnDestroy()
    {
        weatherButton.onClick.RemoveListener(ShowWeather);
        dogBreedsButton.onClick.RemoveListener(ShowDogBreeds);
    }
}