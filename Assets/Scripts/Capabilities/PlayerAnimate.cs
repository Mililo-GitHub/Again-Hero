using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [SerializeField]public Animator anim;

    float horizontalMove = 0f;
    public Ground ground;
    public float jumpDebug;
    public Dash dash;
    private bool isAttacking2;
    private bool isAttacking3;
    private bool isAttacking;
    private bool isDashing;
    public PlayerStamina player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        horizontalMove = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(JumpAnim());
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && isDashing == false )
        {
            if (player.stamina == dash.dashCost || player.stamina > dash.dashCost)
            {
                StartCoroutine(DashAnim());
                anim.SetBool("isDashing", true);
                isDashing = true;
            }
            
        }
        

    }
    

   public IEnumerator JumpAnim()
   {
        anim.SetBool("isJumping", true);
        while (Input.GetKey(KeyCode.Space) && jumpDebug < 0.5f)
        {
            jumpDebug += Time.deltaTime;
            yield return 0;
        }
       
        while (jumpDebug < 0.4f)
        {
            jumpDebug += Time.deltaTime;
            yield return 0;
        }

            jumpDebug = 0f;
            anim.SetBool("isJumping", false);

            anim.SetBool("isFalling", true);
            while (ground.onGround == false)
            {
                yield return 0;
            }

            anim.SetBool("isFalling", false);
        
        
   }

    public IEnumerator DashAnim()
    {
        yield return new WaitForSeconds(0.85f);
        anim.SetBool("isDashing", false);
        isDashing = false;

    }
   




}

