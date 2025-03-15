using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using Zenject;


[Serializable]
public class WeatherResponse
{
    public WeatherProperties properties;
}

[Serializable]
public class WeatherProperties
{
    public List<WeatherPeriod> periods;
}

[Serializable]
public class WeatherPeriod
{
    public string name;
    public int temperature;
    public string temperatureUnit;
    public string icon;
}

public class WeatherController
{
    private readonly WeatherService _weatherService;
    private readonly ApiRequestQueue _apiQueue;
    private readonly WeatherView _weatherView;

    private CancellationTokenSource _cancellationTokenSource;

    [Inject]
    public WeatherController(WeatherService weatherService,
        ApiRequestQueue apiQueue,
        WeatherView weatherView)
    {
        _weatherService = weatherService;
        _apiQueue = apiQueue;
        _weatherView = weatherView;
    }

    public void Activate()
    {
        CancelActiveRequests();
        _cancellationTokenSource = new CancellationTokenSource();
        PollWeatherDataAsync(_cancellationTokenSource.Token);
    }

    public void Deactivate()
    {
        CancelActiveRequests();
        _weatherView.DeactivateUIElements();
    }

    private void CancelActiveRequests()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }

    private async void PollWeatherDataAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            _apiQueue.Enqueue(async () =>
            {
                try
                {
                    string rawData = await _weatherService.GetWeather();
                    var (displayText, iconUrl) = ParseWeatherData(rawData);
                    
                    _weatherView.UpdateWeather(displayText);
                    if (!string.IsNullOrEmpty(iconUrl))
                    {
                        _weatherView.StartCoroutine(LoadIcon(iconUrl));
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Ошибка получения погоды: " + ex.Message);
                }
            });

            try
            {
                await Task.Delay(5000, token);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }

    private IEnumerator LoadIcon(string url)
    {
        using (var request = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Texture2D tex = UnityEngine.Networking.DownloadHandlerTexture.GetContent(request);
                Sprite sprite = Sprite.Create(
                    tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f)
                );
                _weatherView.UpdateIcon(sprite);
            }
            else
            {
                Debug.LogError("Ошибка загрузки иконки: " + request.error);
            }
        }
    }

    private (string displayText, string iconUrl) ParseWeatherData(string rawData)
    {
        try
        {
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(rawData);
            if (weatherResponse?.properties?.periods != null && weatherResponse.properties.periods.Count > 0)
            {
                var period = weatherResponse.properties.periods[0];
                string displayText = $"{period.name} - {period.temperature}{period.temperatureUnit}";

                return (displayText, period.icon);
            }

            return ("No forecast data found.", string.Empty);
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка парсинга погоды: {e.Message}");
            // Вернём сырой JSON, если что-то пошло не так
            return ($"Error parsing weather data: {rawData}", string.Empty);
        }
    }
}