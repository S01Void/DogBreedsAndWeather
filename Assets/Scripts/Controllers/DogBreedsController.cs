using System;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class DogBreedsController
{
    private readonly DogBreedsService _dogBreedsService;
    private readonly ApiRequestQueue _apiQueue;
    private readonly DogBreedsView _dogBreedsView;
    private readonly PopupManager _popupManager;

    private CancellationTokenSource _cancellationTokenSource;

    [Inject]
    public DogBreedsController(
        DogBreedsService dogBreedsService,
        ApiRequestQueue apiQueue,
        DogBreedsView dogBreedsView,
        PopupManager popupManager)
    {
        _dogBreedsService = dogBreedsService;
        _apiQueue = apiQueue;
        _dogBreedsView = dogBreedsView;
        _popupManager = popupManager;
    }

    public void Activate()
    {
        CancelActiveRequests();
        _cancellationTokenSource = new CancellationTokenSource();

        _apiQueue.Enqueue(async () =>
        {
            try
            {
                _popupManager.ShowLoading(true);

                List<BreedData> breeds = await _dogBreedsService.GetBreeds();
                _dogBreedsView.UpdateBreeds(breeds, OnBreedSelected);
            }
            catch (Exception ex)
            {
                Debug.LogError("Ошибка получения пород: " + ex.Message);
            }
            finally
            {
                _popupManager.ShowLoading(false);
            }
        });
    }

    public void Deactivate()
    {
        CancelActiveRequests();
        DeactivatePopup();
    }

    private void DeactivatePopup()
    {
        _popupManager.ShowLoading(false);
        _popupManager.HideBreedDetails();
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

    private void OnBreedSelected(BreedData breed)
    {
        CancelActiveRequests();
        _cancellationTokenSource = new CancellationTokenSource();

        _apiQueue.Enqueue(async () =>
        {
            try
            {
                _popupManager.ShowLoading(true);

                var details = await _dogBreedsService.GetBreedDetails(breed.id);
                if (details != null)
                {
                    _popupManager.ShowBreedDetails(details);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Ошибка получения деталей породы: " + ex.Message);
            }
            finally
            {
                _popupManager.ShowLoading(false);
            }
        });
    }
}