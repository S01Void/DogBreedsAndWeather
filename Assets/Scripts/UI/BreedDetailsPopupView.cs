using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreedDetailsPopupView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI breedNameText;
    [SerializeField] private TextMeshProUGUI breedDescriptionText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);
    }

    public void Setup(BreedData data)
    {
        breedNameText.text = data.attributes.name;
        breedDescriptionText.text = string.IsNullOrEmpty(data.attributes.description)
            ? "No description"
            : data.attributes.description;
    }
    
    public void Hide() => gameObject.SetActive(false);
    
    private void OnDestroy()
    {
        if (closeButton != null)
            closeButton.onClick.RemoveListener(Hide);
    }
}