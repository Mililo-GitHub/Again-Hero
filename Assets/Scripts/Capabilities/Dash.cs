using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public Move move;
    public float dashSpeed;
    public bool isDashing;
    public PlayerStamina player;
    public int dashCost;
    public GameObject hurtBox;
    public float iFramesDash = 1f;
    public Ground ground;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isDashing == false  )
        {
            Debug.Log("Right click and Player isn't dashing");
            if ( ground.onGround == true)
            {
                Debug.Log("RightClick and player is grounded");
                if (player.stamina > dashCost || player.stamina == dashCost)
                {
                    Debug.Log("RightClick and player has enough stamina");
                    StartCoroutine(DashStart());
                    StopCoroutine(MakeInvincible());
                    StartCoroutine(MakeInvincible());
                }
            }


          
        }
       
    }

    public IEnumerator DashStart()
    {
        player.stamina -= dashCost;
        isDashing = true;
        move.maxSpeed = dashSpeed;
        yield return new WaitForSeconds(0.85f);
        move.maxSpeed = 9.1f;
        isDashing = false;
    }

    private IEnumerator MakeInvincible()
    {
        hurtBox.SetActive(false);
        yield return new WaitForSeconds(iFramesDash);
        hurtBox.SetActive(true);
    }

    
}
