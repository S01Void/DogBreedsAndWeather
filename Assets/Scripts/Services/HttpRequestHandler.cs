using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class HttpRequestHandler
{
    public async Task<string> GetAsync(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
                return request.downloadHandler.text;

            throw new Exception(request.error);
        }
    }
}