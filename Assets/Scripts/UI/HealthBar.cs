using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text text;
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        text.text = health.ToString();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        text.text = health.ToString();
    }
}
