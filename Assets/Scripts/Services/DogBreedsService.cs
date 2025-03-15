using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Serialization;
using Zenject;

public class DogApiV2Response
{
    public List<BreedData> Data;
}

[Serializable]
public class DogApiV2SingleResponse
{
    public BreedData data;
}

[Serializable]
public class BreedData
{
    public string id;
    public BreedAttributes attributes;
}

[Serializable]
public class BreedAttributes
{
    public string name;
    public string description;
}

public class DogBreedsService
{
    [Inject]
    private readonly HttpRequestHandler _httpHandler;

    public async Task<List<BreedData>> GetBreeds()
    {
        string url = "https://dogapi.dog/api/v2/breeds";
        string response = await _httpHandler.GetAsync(url);
        
        var apiResponse = JsonConvert.DeserializeObject<DogApiV2Response>(response);
        if (apiResponse?.Data == null)
            throw new Exception("No breed data found in response.");

        return apiResponse.Data.Take(10).ToList();
    }

    public async Task<BreedData> GetBreedDetails(string breedId)
    {
        string url = $"https://dogapi.dog/api/v2/breeds/{breedId}";
        string response = await _httpHandler.GetAsync(url);

        var singleResponse = JsonConvert.DeserializeObject<DogApiV2SingleResponse>(response);
        return singleResponse.data;
    }
}