using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;
    public bool getKnockback;
    public bool dmgPlayer;
    public int dmgToPlayerTouch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( health < 1)
        {
            Destroy(gameObject);
        }
    }

    

}
