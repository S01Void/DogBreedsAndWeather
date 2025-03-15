using UnityEngine;
using Zenject;

public class PopupManager : MonoBehaviour
{
    [Inject] 
    private BreedDetailsPopupView _breedDetailsPopupView;
    [Inject] 
    private LoadingIndicator _loadingIndicator;

    public void ShowLoading(bool show)
    {
        if (_loadingIndicator == null) 
            return;
        
        if(show)
            _loadingIndicator.Show();
        else
            _loadingIndicator.Hide();

    }

    public void ShowBreedDetails(BreedData details)
    {
        if (_breedDetailsPopupView != null)
        {
            _breedDetailsPopupView.gameObject.SetActive(true);
            _breedDetailsPopupView.Setup(details);
        }
    }

    public void HideBreedDetails()
    {
        _breedDetailsPopupView.Hide();
    }
}