using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public Slider easeStaminaSlider;
    public PlayerStamina player;
    public float lerpSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        player.stamina = player.maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaSlider.value != player.stamina)
        {
            staminaSlider.value = player.stamina;
        }

        if (staminaSlider.value != easeStaminaSlider.value)
        {
            easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, player.stamina, lerpSpeed);
        }
    }
}
