/***
 *  ETIOS GAME
 *  Copyright © 2021
 *  
 *  Rodrigo Emilio Perusquia Montes  <rodrigo.perusquia@xadglobal.com>
 *  Gerardo Emilio Perusquia Montes  <gerardo.perusquia@xadglobal.com>  
 * 
 *  PlayerWeapon : Controller for player weapon interaction in the game
 *
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{

    public class MyWeapon 
    {
        public int bulletsPerMag;          // bullets in each mag 
        public int bulletType;             // type of bullets in mag
        public int mags;                   // number of mags wich ports the player
        public bool isActive = false;      // The weapon activates only when the player gets 
        public GameObject weapon;          // the weapon
    }

    private const int MAX_WEAPONS = 5;
    
    //public GameObject Arma;
    [Tooltip("The weapons of the player")]
    public GameObject[] Weapons;
    private MyWeapon[] WeaponsO = new MyWeapon[MAX_WEAPONS]; // The player can carry max MAX_WEAPONS
    //private List<MyWeapon> WeaponsO = new List<MyWeapon>(MAX_WEAPONS);
    
    private int bulletNum = 0;
    private int bulletFired = 0;
    private int weaponInUse = 0;

    // Start is called before the first frame update
    void Start()
    {
        initGuns();
    }


    private void Awake()
    {
        for (int i = 0; i < MAX_WEAPONS; i++)
        {
            if(WeaponsO[i]==null)
                WeaponsO[i] = new MyWeapon();
        }
        initGuns();
    }


    public GameObject getWeaponInUse()
    {
        return Weapons[weaponInUse];
    }

    private void input()
    {
        if(Input.GetKeyDown("1")) {
            weaponInUse = 0;
        } 
        else if(Input.GetKeyDown("2"))
        {
            weaponInUse = 1;
        }
    }


    private void initGuns()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
            WeaponsO[i].weapon = Weapons[i];
            setScale(Weapons[i],0);
        }
    }


    private void updateGUIAmmoLevel(string _value)
    {
        GameObject _gui = GameObject.Find("AmmoLevelText"); // Dock front of the GUI
        Text _textValue = _gui.GetComponent<Text>();

        _textValue.text = _value;


    }


    public MyWeapon GetMyWeapon(string _wich)
    {
        foreach(MyWeapon _we in WeaponsO)
        {
            if (_we.weapon.tag.Equals(_wich))
                return _we;
        }
        return null;
    }

    public MyWeapon[] GetPlayerWeapons()
    {
        return WeaponsO;
    }

    // Update is called once per frame
    void Update()
    {

        initGuns();
        input();
        PlayerController _pc = GetComponent<PlayerController>();
        GameObject _actw = WeaponsO[weaponInUse].weapon;
        Weapon _actweapon = _actw.GetComponent<Weapon>();
        updateGUIAmmoLevel(_actweapon.getNumberOfBullets().ToString());
        if(Input.GetKey(KeyCode.V) && !_pc.getImDead())
        {
            _actw.SetActive(true);
            if(_actw.name.Equals("Arma"))
                setScale(_actw, 100);
            else
                setScale(_actw, 1);
            _actweapon.Fire();
            //Fire();
        }

        Util.checkForRemoveBullets(_actweapon.getBulletsFired());
       
    }

    private void setScale(GameObject _obj,float _value)
    {
        _obj.transform.localScale = new Vector3(_value, _value, _value);
    }
    /*
    private void Fire()
    {
        if (bulletNum > burst*5 && (bulletFired<(mags*bulletsPerMag)))
        {
            bulletNum = 0;
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

            StartCoroutine(DestroyBulletAfterTime(bullet1, lifeTime));
            StartCoroutine(DestroyBulletAfterTime(bullet2, lifeTime));

            bulletFired += 2;
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
    */
   
}
