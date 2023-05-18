using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Start()
    {
        totalHealthbar.fillAmount = playerStats.CurrentHealth / 10f;
    }

    private void Update()
    {
        currentHealthbar.fillAmount = playerStats.CurrentHealth / 10f;
    }
}
