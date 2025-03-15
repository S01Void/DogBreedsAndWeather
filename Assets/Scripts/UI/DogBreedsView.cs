using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DogBreedsView : MonoBehaviour
{
    [SerializeField] private Transform breedsContent;
    
    [Inject] 
    private DogBreedItemView.Factory _dogBreedItemFactory;

    public void UpdateBreeds(List<BreedData> breeds, System.Action<BreedData> onBreedSelected)
    {
        foreach (Transform child in breedsContent)
            Destroy(child.gameObject);

        foreach (var breed in breeds)
        {
            var item = _dogBreedItemFactory.Create();
            item.transform.SetParent(breedsContent, false);
            item.Init(breed.attributes.name, () => onBreedSelected?.Invoke(breed));
        }
    }
}