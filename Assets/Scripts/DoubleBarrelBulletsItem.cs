using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelBulletsItem : MonoBehaviour, Item
{

    public int value = 1;

    public string getName()
    {
        return "DoubleBarrelBullets";
    }

    public string getType()
    {
        return "Bullet";
    }


    public string getValue()
    {
        return this.value.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
