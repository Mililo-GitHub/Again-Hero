using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public PlayerHealth player;
    public float lerpSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        player.health = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != player.health) 
        {
            healthSlider.value = player.health;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
           easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, player.health, lerpSpeed);
        }
    }
}
