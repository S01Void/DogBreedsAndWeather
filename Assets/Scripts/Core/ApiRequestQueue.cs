using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ApiRequestQueue
{
    private Queue<Func<Task>> _requestQueue = new();
    private bool _isProcessing = false;

    public void Enqueue(Func<Task> request)
    {
        _requestQueue.Enqueue(request);
        if (!_isProcessing)
            ProcessNext();
    }

    private async void ProcessNext()
    {
        if (_requestQueue.Count == 0)
        {
            _isProcessing = false;
            return;
        }

        _isProcessing = true;

        var request = _requestQueue.Dequeue();
        await request();

        // Рекурсивно обрабатываем следующий запрос.
        ProcessNext();
    }
}