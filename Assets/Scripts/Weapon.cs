/***
 *  ETIOS GAME
 *  Copyright © 2021
 *  
 *  Rodrigo Emilio Perusquia Montes  nscroll1@gmail.com
 *  Gerardo Emilio Perusquia Montes  
 * 
 *  Weapon : Generic interface for player weapon
 *
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon     
{
    public void Fire();

    public void ChangeMag();


    public int getNumberOfBullets(); // Get the quantity of bullets you own

    public List<BulletsTime> getBulletsFired();
}
