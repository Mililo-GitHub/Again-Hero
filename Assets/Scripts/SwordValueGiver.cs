using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordValueGiver : MonoBehaviour
{
    public Sword swordL;
    public SwordAttack sword;
    public SpriteRenderer renderer;
    public StatsSword swordStats;
    public PlayerInventory inventory;
    private int activeWeapon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        activeWeapon = inventory.activeWeapon;
        swordStats = inventory.swordsInventory[activeWeapon];
        renderer.sprite = swordStats.swordSprite;
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyHurtbox")
        {
            EnemyHealth hp = other.gameObject.transform.parent.GetComponent<EnemyHealth>();
            Animator eAnim = other.gameObject.transform.parent.GetComponent<Animator>();
            if (swordL.attack3done == true)
            {
                StartCoroutine(sword.DoDamage(swordStats.criticalDamage, swordStats.criticalKnockback, hp, eAnim, true));
            }
            else
            {
                StartCoroutine(sword.DoDamage(swordStats.damage, swordStats.knockback, hp, eAnim, false));
            }
        }
        else
        {
            return;
        }
    }
}
