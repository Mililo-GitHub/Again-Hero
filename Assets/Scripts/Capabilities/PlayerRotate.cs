using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public Transform transform;
    public Sword Sword;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Sword.isAttacking ==false) {

            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            }
        }
        else
        {
            return;
        }
        
    }
}
