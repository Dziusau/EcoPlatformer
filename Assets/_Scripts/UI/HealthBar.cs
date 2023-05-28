using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Start()
    {
        totalHealthbar.fillAmount = GameManager.player.GetComponent<Health>().CurrentHealth / 10f;
    }

    private void Update()
    {
        if (GameManager.player != null)
            currentHealthbar.fillAmount = GameManager.player.GetComponent<Health>().CurrentHealth / 10f;
        else
            currentHealthbar.fillAmount = 0f;
    }
}
