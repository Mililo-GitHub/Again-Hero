using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatsSword : ScriptableObject
{
    public float damage;
    public float criticalDamage;
    public float knockback;
    public float criticalKnockback;
    public Sprite swordSprite;
}
