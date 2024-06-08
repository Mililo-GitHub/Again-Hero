using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public PlayerHealth player;
    private bool isInEnemy;
   private Coroutine damageCoroutine = null;
    public float iFrames;
    public CircleCollider2D playerHurtBox;
    public Rigidbody2D rb;
    public float playerKnockback = 2f;
    public float upwardsKnockback = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamagePlayer(other));
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
            isInEnemy = false;

        }
    }


    private IEnumerator DamagePlayer(Collider2D other)
    {
        yield return null;
        yield return null;
        yield return null;
        isInEnemy = true;
        if (other.gameObject.GetComponent<EnemyHealth>().dmgPlayer == true)
        {
                player.health -= other.gameObject.GetComponent<EnemyHealth>().dmgToPlayerTouch;
               
                
        }
        else
        {
            yield return null;
        }
        yield return new WaitForSeconds(iFrames);
        if (isInEnemy )
        {
            damageCoroutine = StartCoroutine(DamagePlayer(other));
            yield break;
        }
    }

   


    
}
