/***
 *  ETIOS GAME
 *  Copyright © 2021
 *  
 *  Rodrigo Emilio Perusquia Montes  <rodrigo.perusquia@xadglobal.com>
 *  Gerardo Emilio Perusquia Montes  <gerardo.perusquia@xadglobal.com> 
 * 
 *  EnergyItem : Controller for enegry item, use an characteristics
 *
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyItem : MonoBehaviour, Item
{
    [Tooltip("The health level to increase to the player")]
    public int healthLevel = 20; // Health level to increase to the Player

    public string getName()
    {
        return "EnergyItem";
    }

    public string getType()
    {
        return "Energy";
    }
    public string getValue()
    {
        return healthLevel.ToString();

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
