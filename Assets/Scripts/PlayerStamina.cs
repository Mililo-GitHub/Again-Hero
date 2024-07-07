using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public int stamina = 100;
    public int maxStamina = 100;
    public Dash dash;
    private float lastPressTime = 0f;
    public float timeThreshold = 3f; // Time in seconds to check for inactivity
    private bool isStaminaInsufficient;
    public float reloadSpeed = 0.1f;
    private bool shouldReload;
    public bool isReloading;

    void Update()
    {
        if (stamina == maxStamina)
        {
            lastPressTime = Time.time;
            isReloading = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            
            RightClick();
        }

        if (Time.time - lastPressTime > timeThreshold && isReloading == false)
        {
            // The right mouse button hasn't been pressed for the specified timeThreshold
            StartCoroutine(ReloadStamina());
            isReloading = true;
           
        }

        if (stamina > dash.dashCost || stamina == dash.dashCost)
        {
            isStaminaInsufficient = false;
        }
        else
        {
            isStaminaInsufficient = true;
        }
    }

    private void RightClick ()
    {
        if (isStaminaInsufficient == true)
        {
            Debug.Log("Insufficient Stamina for dash");
            return;
           
        }
        else
        {
            shouldReload = false;
            // Update lastPressTime to the current time
            lastPressTime = Time.time;
        }
       
    }

    private IEnumerator ReloadStamina ()
    {
        shouldReload = true;
        while (stamina < maxStamina && shouldReload == true)
        {
            stamina++;
            yield return new WaitForSeconds(reloadSpeed);
        }
        if (stamina == maxStamina)
        {
            lastPressTime = Time.time;
            isReloading = false;
        }

        if (shouldReload == false) 
        {
            lastPressTime = Time.time;
            isReloading = false;
           
        }
    }
  
}