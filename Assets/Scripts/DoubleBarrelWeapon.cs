using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoubleBarrelWeapon : MonoBehaviour, Weapon
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
    public Transform bulletSpawn1;
    public Transform bulletSpawn2;
    public float bulletSpeed = 300;
    public float lifeTime = 0.5f;
    public int burst = 3;
    public int mags = 3;
    public int bulletsPerMag = 32;
    public int maxMags = 10;    


    private int bulletNum = 0;
    private int bulletFired = 0;
    private System.TimeSpan now = System.DateTime.Now.TimeOfDay;

    

    private List<BulletsTime> bulletsFired = new List<BulletsTime>();


    public void Awake()
    {
        if (weaponPrefab == null)
            weaponPrefab = this.gameObject;
    }

    public void Start()
    {
        weaponPrefab.SetActive(false);
    }


    public List<BulletsTime> getBulletsFired()
    {
        return bulletsFired;
    }

    public void Fire()
    {

        if (bulletFired<(mags*bulletsPerMag))
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
                bulletNum+=2;
            }


            
            GameObject bullet1 = Instantiate(bulletPrefab);
            GameObject bullet2 = Instantiate(bulletPrefab);

            Physics.IgnoreCollision(bullet1.GetComponent<Collider>(),
                                    bulletSpawn1.GetComponent<Collider>());
            Physics.IgnoreCollision(bullet1.GetComponent<Collider>(),
                                    bulletSpawn2.GetComponent<Collider>());
            Physics.IgnoreCollision(bullet2.GetComponent<Collider>(),
                                   bulletSpawn1.GetComponent<Collider>());
            Physics.IgnoreCollision(bullet2.GetComponent<Collider>(),
                                    bulletSpawn2.GetComponent<Collider>());

            bullet1.transform.position = bulletSpawn2.position;
            bullet2.transform.position = bulletSpawn1.position;

            Vector3 rotation1 = bullet1.transform.rotation.eulerAngles;
            Vector3 rotation2 = bullet2.transform.rotation.eulerAngles;


            bullet1.transform.rotation = Quaternion.Euler(rotation1.x, transform.eulerAngles.y, rotation1.z);
            bullet2.transform.rotation = Quaternion.Euler(rotation2.x, transform.eulerAngles.y, rotation2.z);

            bullet1.GetComponent<Rigidbody>().AddForce(bulletSpawn2.forward * bulletSpeed, ForceMode.Impulse);
            bullet2.GetComponent<Rigidbody>().AddForce(bulletSpawn1.forward * bulletSpeed, ForceMode.Impulse);


            //bullet.GetComponent<Rigidbody>().transform.Translate(new Vector3(bulletSpawn.position.x + 7,0,0), Space.Self);
            //bullet.GetComponent<Rigidbody>().MovePosition(new Vector3(0,0,bulletSpawn.position.x + 7)); 

            bulletsFired.Add(new BulletsTime(System.DateTime.Now.TimeOfDay, bullet1,lifeTime));
            bulletsFired.Add(new BulletsTime(System.DateTime.Now.TimeOfDay, bullet2,lifeTime));


            /*
            StartCoroutine(DestroyBulletAfterTime(bullet1, lifeTime));
            StartCoroutine(DestroyBulletAfterTime(bullet2, lifeTime));
            */


            bulletFired += 2;
        }
        //else
          //  bulletNum++;

    }




    public void OnDisable()
    {
        //Util.checkForRemoveBullets(BulletsFired);

    }

    public void Reset()
    {
        
    }

    private IEnumerator DestroyBulletAfterTime(GameObject _bullet,float _delay)
    {
        Debug.Log(" ENTRA A DESTRUYE EL BULLET " + _delay);

        
        yield return new WaitForSecondsRealtime(_delay);

        Debug.Log(" SE DESRUYE EL BULLET ");
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

    public int getNumberOfBullets()
    {
        return (mags * bulletsPerMag) - bulletFired;

    }

    public void setNumberOfBullets(int _value)
    {
        if (_value > bulletFired)
            bulletFired = 0;
        else
            bulletFired -= _value;
    }
}
