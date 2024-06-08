using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<StatsSword> swordsInventory;
    public int activeWeapon = 0;
    public StatsSword brokenSword;
    public StatsSword metalSword;

    // Start is called before the first frame update
    void Start()
    {
        swordsInventory.Add(brokenSword);
        swordsInventory.Add(metalSword);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (swordsInventory.Count-1 > activeWeapon) {
                activeWeapon++;
            }
            else
            {
                activeWeapon = 0;
            }
        }
    }
}
