using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weatherText;
    [SerializeField] private Image weatherIcon;

    public void UpdateWeather(string weatherData)
    {
        if (weatherText != null)
        {
            weatherText.gameObject.SetActive(true);
            weatherText.enabled = true;
            weatherText.text = weatherData;
        }
    }

    public void UpdateIcon(Sprite sprite)
    {
        if (weatherIcon != null)
        {
            weatherIcon.gameObject.SetActive(true);
            weatherIcon.sprite = sprite;
            weatherIcon.enabled = sprite != null;
        }
    }

    public void DeactivateUIElements()
    {
        weatherText.gameObject.SetActive(false);
        weatherIcon.gameObject.SetActive(false);
    }
}