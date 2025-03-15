using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
    [Tooltip("Скорость вращения индикатора в градусах в секунду.")]
    [SerializeField] private float rotationSpeed = 180f;
    
    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
    
}