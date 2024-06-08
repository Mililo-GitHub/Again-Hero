using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnvoirementTrigger : MonoBehaviour
{
    public Light2D light;
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
        if (other.tag == "Hurtbox")
        {
            Debug.Log("Exited Cave");
            light.color = Color.white;
        }
    }
}
