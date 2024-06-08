using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public SpriteRenderer playerRend;
    public bool isAttacking;
    public Animation anim;
    public AnimationClip SwordAttack1;
    public AnimationClip SwordAttack2;
    public AnimationClip SwordAttack3;
    private bool attack1done;  
    private bool attack2want;
    public bool attack2done;
    private bool attack3want;
    public bool attack3done;
    public float waitBuffer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (attack1done == true)
            {
                attack2want = true;
            }

            if (attack1done== false)
            {
                StartCoroutine(Attack());
            }

            if (attack2done == true)
            {
                attack3want = true;
            }

        }



       
    }

   public IEnumerator Attack()
   {
        isAttacking = true;
        StartCoroutine(ChangeColorOverTime(playerRend.material, Color.red));
        anim.clip = SwordAttack1;
        anim.Play();
        attack1done = true;
        yield return new WaitForSeconds(1f);
        
        if (attack2want == false)
        {
            StartCoroutine(ChangeColorOverTime(playerRend.material, Color.white));
            isAttacking = false;
        }

        

        if (attack2want == true)
         {
           waitBuffer = 0;
        }

        // Waiting period with dynamic check for attack2want change
        float elapsedTime = 0f;
        while (elapsedTime < waitBuffer)
        {
            if (attack2want)
            {
                break; // Skip the waitBuffer and proceed to next attack
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        waitBuffer = 1.6f;
        if (attack2want == true)
        {
            isAttacking = true;
            StartCoroutine(ChangeColorOverTime(playerRend.material, Color.red));
            anim.clip = SwordAttack2;
            anim.Play();
            attack2done = true;
            Debug.Log("Second attack initiated");
        }
        else
        {
            Debug.Log("Stopped attack because no additional input");
            StopAttack();
            yield break; // Exit the coroutine
        }
        yield return new WaitForSeconds(0.5f);
        
        if (attack3want == false)
        {
            StartCoroutine(ChangeColorOverTime(playerRend.material, Color.white));
            isAttacking = false;
        }

        
        if (attack3want == true)
        {
               waitBuffer = 0;
         }


        // Waiting period with dynamic check for attack3want change
        elapsedTime = 0f;
        while (elapsedTime < waitBuffer)
        {
            if (attack3want)
            {
                break; // Skip the waitBuffer and proceed to next attack
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        waitBuffer = 1.6f;
        if (attack3want == true)
        {
            isAttacking = true;
            StartCoroutine(ChangeColorOverTime(playerRend.material, Color.red));
            anim.clip = SwordAttack3;
            anim.Play();
            attack3done = true;
            Debug.Log("Third attack initiated");
        }
        else
        {
            Debug.Log("Stopped attack because no additional input");
            StopAttack();
            yield break; // Exit the coroutine
        }
        yield return new WaitForSeconds(1);
        StopAttack();


        yield return null;
   }

    public void StopAttack()
    {
        isAttacking = false;
        StartCoroutine(ChangeColorOverTime(playerRend.material, Color.white));
        attack1done = false;
        attack2want = false;
        attack2done = false;
        attack3want = false;
        attack3done = false;
        waitBuffer = 1.6f;
    }

    private IEnumerator ChangeColorOverTime(Material material, Color targetColor)
    {
        Color startColor = material.color;
        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            material.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the target color is set at the end
        material.color = targetColor;
    }
}
