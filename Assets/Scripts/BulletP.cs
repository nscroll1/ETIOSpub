using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" TRIGGER!!!!!!!!!!!!! " + other.tag);
        if (other.tag.Equals("Enemigo") || other.tag.Equals("Portal"))
        {
            // Add a hit to the enemy
            //GameObject _player = GameObject.Find("PlayerC");
            GameObject _player = GameObject.FindGameObjectWithTag("Player");
            PlayerController _playerc = _player.GetComponent<PlayerController>();
            Debug.Log(" HIT!!!!!!! " + other.name);
            if (other.tag.Equals("Enemigo"))
            {
                Enemy _enemigo = other.GetComponent<Enemy>();
                if (_enemigo == null)
                    _enemigo = other.transform.root.GetComponent<Enemy>();
                Animator _eanim = _enemigo.getAnimator();
                Debug.Log(" GAME OBJECT " + _enemigo.getHits() + " MAX_HITS " + _enemigo.getMaxHits());
                _enemigo.addHit();
                _enemigo.setSquareColor();
                if (_enemigo.getHits() > _enemigo.getMaxHits() && !_enemigo.isDead())
                {
                    Debug.Log("SI SE MURIO " + other.name);
                    _enemigo.setImDead();
                    _enemigo.hideWeapon();
                    _playerc.Points += _enemigo.getValue();

                    //Debug.Log(" IS PLAING "+_eanim.isPlaying("Base Layer.die"));
                    //Destroy(other.gameObject); //Destroy the enemy

                }
            }
            //if(!_enemigo.isDead())
           
            // Destroy the bullet
        }
        Destroy(this.gameObject);
        /*else if(other.tag.Equals("Player")) {
            Debug.Log(" HIT!!!!! " + other.name);
            PlayerController _player = other.GetComponent<PlayerController>();
            _player.addHit();
            if(_player.getHits() > _player.getMaxHits())
            {
                _player.setImDead();
            }
            Destroy(this.gameObject);
        }*/
    }


}