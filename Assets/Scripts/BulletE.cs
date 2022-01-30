/***
 *  <sumary>
 *  ETIOS GAME
 *  Copyright © 2021
 *  
 *  Rodrigo Emilio Perusquia Montes  <rodrigo.perusquia@xadglobal.com>
 *  Gerardo Emilio Perusquia Montes  <gerardo.perusquia@xadglobal.com>
 * 
 *  BulletE : Controller for behavior of the bullet of the Enemy Weapon
 *  </sumary>
 * 
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletE : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" HIT!!!!! " + other.tag);
        if (other.tag.Equals("Player") || 
           other.tag.Equals("Wall")) {
            if (other.tag.Equals("Player"))
            {
                PlayerController _player = other.GetComponent<PlayerController>();
                _player.addHit();
                other.transform.DOShakePosition(0.05f, 0.2f, 2);
                if (_player.getHits() > _player.getMaxHits())
                {
                    _player.setImDead();
                    _player.hideWeapon();
                }
            }
            Destroy(this.gameObject);
        }
        
    }

}