using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Start()
    {
        totalHealthbar.fillAmount = playerHealth.CurrentHealth / 10f;
    }

    private void Update()
    {
        currentHealthbar.fillAmount = playerHealth.CurrentHealth / 10f;
    }
}