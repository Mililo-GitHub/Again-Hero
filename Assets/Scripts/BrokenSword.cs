using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SwordStats : ScriptableObject
{
    public float damage;
    public float criticalDamage;
    public float knockback;
    public float criticalKnockbac;
    public Sprite swordSprite;
}
