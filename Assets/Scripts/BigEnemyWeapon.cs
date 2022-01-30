using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn1;
    public Transform bulletSpawn2;
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

        GameObject _player = Util.findPlayer(FindObjectsOfType<GameObject>());
        if (_player != null)
        {
            PlayerController _opc = _player.GetComponent<PlayerController>();
            if (_opc.getImDead() ||
                !Util.isPlayerOnDistance(_player.transform, gameObject.transform)) fire = false;
            else
                fire = true;
        }
        

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

            GameObject bullet1,bullet2;

            try
            {
                bullet1 = Instantiate(bulletPrefab);
                bullet2 = Instantiate(bulletPrefab);
            }
            catch (System.Exception e)
            {
                bullet1 = Instantiate(bulletPreres);
                bullet2 = Instantiate(bulletPreres);

            }


            Physics.IgnoreCollision(bullet1.GetComponent<Collider>(),
                                    bulletSpawn1.GetComponent<Collider>());
            Physics.IgnoreCollision(bullet2.GetComponent<Collider>(),
                                    bulletSpawn2.GetComponent<Collider>());


            bullet1.transform.position = bulletSpawn1.position;
            bullet2.transform.position = bulletSpawn2.position;

            Vector3 rotation1 = bullet1.transform.rotation.eulerAngles;
            Vector3 rotation2 = bullet2.transform.rotation.eulerAngles;

            bullet1.transform.rotation = Quaternion.Euler(rotation1.x, transform.eulerAngles.y, rotation1.z);
            bullet2.transform.rotation = Quaternion.Euler(rotation2.x, transform.eulerAngles.y, rotation2.z);


            bullet1.GetComponent<Rigidbody>().AddRelativeForce(bulletSpawn1.forward * bulletSpeed, ForceMode.Impulse);
            bullet2.GetComponent<Rigidbody>().AddRelativeForce(bulletSpawn1.forward * bulletSpeed, ForceMode.Impulse);


            StartCoroutine(DestroyBulletAfterTime(bullet1, lifeTime));
            StartCoroutine(DestroyBulletAfterTime(bullet2, lifeTime));


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
