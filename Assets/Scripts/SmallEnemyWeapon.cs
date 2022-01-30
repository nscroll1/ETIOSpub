using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallEnemyWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 300;
    public float lifeTime = 0.5f;
    public float burst = 3;

    public bool fire = true;

    private System.TimeSpan now = System.DateTime.Now.TimeOfDay;

    private int bulletNum = 0;

    private GameObject bulletPreres;


    // Start is called before the first frame update
    void Start()
    {
        fire = true;
        bulletPreres = bulletPrefab;
    }

   
    private void Awake()
    {
        fire = true;
    }

    // Update is called once per frame
    void Update()
    {

        Fire();
    }

    private void Fire()
    {
        
        GameObject _player  = Util.findPlayer(FindObjectsOfType<GameObject>());
        SmallEnemy _ecomp = GetComponent<SmallEnemy>();
        GameObject _eweap = _ecomp.weapon;
        if (_player != null)
        {
            PlayerController _opc = _player.GetComponent<PlayerController>();
            Debug.Log(" ENCONTRE EL PLAYER SMW " + _player);
            if (_opc.getImDead() ||
                !Util.isPlayerOnDistance(_player.transform,gameObject.transform) ||
                !_eweap.activeSelf) fire = false;
            else 
                fire = true;
        }
        else
            Debug.Log(" NO ENCONTRE ");
        


        Debug.Log(" VALOR DE FIRE " + fire);

        if (fire)
        {

            System.TimeSpan _seconds = System.DateTime.Now.TimeOfDay;
            double _actsec = (_seconds - now).TotalMilliseconds;

            if (bulletNum >= burst)
            {
                if (_actsec < 500) return;
                bulletNum = 0;
                now = System.DateTime.Now.TimeOfDay;
            }
            else
            {
                bulletNum++;
            }

            GameObject bullet1;

            try
            {
                bullet1 = Instantiate(bulletPrefab);
            }
            catch (System.Exception e)
            {
                bullet1 = Instantiate(bulletPreres);
            }


            Physics.IgnoreCollision(bullet1.GetComponent<Collider>(),
                                    bulletSpawn.GetComponent<Collider>());


            bullet1.transform.position = bulletSpawn.position;

            Vector3 rotation1 = bullet1.transform.rotation.eulerAngles;
            
            bullet1.transform.rotation = Quaternion.Euler(rotation1.x, transform.eulerAngles.y, rotation1.z);


            bullet1.GetComponent<Rigidbody>().AddRelativeForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);


            StartCoroutine(DestroyBulletAfterTime(bullet1, lifeTime));
                  

        }

    }

    private IEnumerator waitUntilNextBurst(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        
        bulletNum = 0;
    }

 
    private IEnumerator DestroyBulletAfterTime(GameObject _bullet, float _delay)
    {
        yield return new WaitForSeconds(_delay);

        Destroy(_bullet);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall"))
        {
            fire = false;
        }
    }
}
