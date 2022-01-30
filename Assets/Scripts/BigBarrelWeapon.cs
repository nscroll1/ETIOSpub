/***
 *  <sumary>
 *  ETIOS GAME
 *  Copyright © 2021
 *  
 *  Rodrigo Emilio Perusquia Montes  <rodrigo.perusquia@xadglobal.com>
 *  Gerardo Emilio Perusquia Montes  <gerardo.perusquia@xadglobal.com>
 * 
 *  BigBarrelWeapon : Controller for the control of the bigBarrelWeapon action
 *  </sumary>
 * 
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBarrelWeapon : MonoBehaviour, Weapon
{

    [Tooltip("The gameobject who defines the gun")]
    public GameObject weaponPrefab;
    [Tooltip("The gameobject who defines the bullet")]
    public GameObject bulletPrefab;
    [Tooltip("The place where the bullet path starts")]
    public Transform bulletSpawn1;
    [Tooltip("The velocity of the bullet")]
    public float bulletSpeed = 20;
    [Tooltip("Life time of the bullet before it destroys")]
    public float lifeTime = 0.5f;
    [Tooltip("Number of the bullets in each burst")]
    public int burst = 3;
    [Tooltip("The animation of the barrel in action")]
    public Animator animator;
    [Tooltip("The default number of the mags on start")]
    public int mags = 1;
    [Tooltip("The maximum number of mags who the player can carry")]
    public int maxMags = 1;
    [Tooltip("The maximum number of the bullets who the plauer can carry")]
    public int maxBullets = 150;
    [Tooltip("The max number of bullets in each mag")]
    public int bulletsPerMag = 150;

    private int bulletNum = 0;
    private int bulletFired = 0;
    private List<BulletsTime> bulletsFired = new List<BulletsTime>();
    private System.TimeSpan now;


    public List<BulletsTime> getBulletsFired()
    {
        return bulletsFired;
    }


    public void Fire()
    {
        animator.Play("Base Layer.fire");
        //weaponPrefab.SetActive(true);
        if (bulletFired < (mags * bulletsPerMag))
        {

            System.TimeSpan _seconds = System.DateTime.Now.TimeOfDay;
            double _actsec = (_seconds - now).TotalMilliseconds;

            if (bulletNum >= burst)
            {
                if (_actsec < 400) return;
                bulletNum = 0;
                now = System.DateTime.Now.TimeOfDay;
            }
            else
            {
                bulletNum += 1;
            }

            GameObject bullet1 = Instantiate(bulletPrefab);

            Physics.IgnoreCollision(bullet1.GetComponent<Collider>(),
                                    bulletSpawn1.GetComponent<Collider>());

            bullet1.transform.position = bulletSpawn1.position;

            Vector3 rotation1 = bullet1.transform.rotation.eulerAngles;

            bullet1.transform.rotation = Quaternion.Euler(rotation1.x, transform.eulerAngles.y, rotation1.z);

            bullet1.GetComponent<Rigidbody>().AddForce(bulletSpawn1.forward * bulletSpeed, ForceMode.Impulse);


            //bullet.GetComponent<Rigidbody>().transform.Translate(new Vector3(bulletSpawn.position.x + 7,0,0), Space.Self);
            //bullet.GetComponent<Rigidbody>().MovePosition(new Vector3(0,0,bulletSpawn.position.x + 7)); 

            //StartCoroutine(DestroyBulletAfterTime(bullet1, lifeTime));
            bulletsFired.Add(new BulletsTime(System.DateTime.Now.TimeOfDay, bullet1, lifeTime));

            bulletFired++;
        }
        else
            bulletNum++;

    }
    private IEnumerator DestroyBulletAfterTime(GameObject _bullet,float _delay)
    {
        yield return new WaitForSeconds(_delay);

        Destroy(_bullet);
    }

    private void Wait(double x)
    {
        System.DateTime t = System.DateTime.Now;
        System.DateTime tf = System.DateTime.Now.AddSeconds(x);

        while (t < tf)
        {
            t = System.DateTime.Now;
        }
    }

    public void ChangeMag()
    {
        
    }


    public void Start()
    {
        //weaponPrefab.SetActive(false);
    }


    public void Awake()
    {
        if (weaponPrefab == null)
            weaponPrefab = this.gameObject;
    }


    public int getNumberOfBullets()
    {
        return (mags * bulletsPerMag) - bulletFired;
    }



    public void setBulletsFired(int _value)
    {
        bulletFired = _value;
    }


    public void setNumberOfBullets(int _value)
    {
        if (_value > bulletFired)
            bulletFired = 0;
        else
            bulletFired -= _value; 
    }
}
