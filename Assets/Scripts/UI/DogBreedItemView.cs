using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Zenject;


public class DogBreedItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI breedNameText;
    [SerializeField] private Button button;

    private Action _onClick;
    
    public class Factory : PlaceholderFactory<DogBreedItemView> { }

    public void Init(string breedName, Action onClickCallback)
    {
        breedNameText.text = breedName;
        _onClick = onClickCallback;
    }

    private void Awake()
    {
        if (button != null)
            button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        _onClick?.Invoke();
    }
    
    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(HandleClick);
    }
}