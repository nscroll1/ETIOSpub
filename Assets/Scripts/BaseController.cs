/***
 *  ETIOS GAME
 *  Copyright © 2021
 *  
 *  Rodrigo Emilio Perusquia Montes  <rodrigo.perusquia@xadglobal.com>
 *  Gerardo Emilio Perusquia Montes  <gerardo.perusquia@xadglobal.com>
 * 
 *  BaseController : Controller for base (items) interaction and behavior
 *
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseController : MonoBehaviour
{

    [Tooltip("The item that will be shown in the base")]
    public GameObject[] items;
    [Tooltip("The place in wich the base will appear")]
    public GameObject baseSpawnPosition;
    [Tooltip("The item will be shown only once in the game")]
    public bool showOnlyOnce; // the item will be shown only once in the game
    [Tooltip("Time lapse in seconds to show the next item")]
    public int seconds = 10; // the item will be shown only once in the game



    private GameObject internItem;
    private Vector3 baseItemPosition;
    private System.TimeSpan now;
    private System.TimeSpan then;
    private Item _peitem;
    private int indexItem = 0;
  


    // Start is called before the first frame update
    void Start()
    {        
        //item.AddComponent<Halo>();   
    }

    private void Awake()
    {
        ShowItems();
    }


    private void ShowItems()
    {
        baseItemPosition = GameObject.Find("baseSpawnItem").transform.position;
        if (baseSpawnPosition != null)
            transform.position = baseSpawnPosition.transform.position;
        internItem = Instantiate(items[indexItem]);
        internItem.SetActive(true);
        //internItem.transform.position = transform.position;// baseItemPosition;
        internItem.transform.position = new Vector3(transform.position.x,
                                                    transform.position.y + 0.5f,
                                                    transform.position.z);
        Debug.Log(" ITEM " + internItem.transform.position + " BASE " + baseItemPosition +
                  " PLATFORM " + this.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        then = System.DateTime.Now.TimeOfDay - now;

        if (!internItem.activeSelf && !showOnlyOnce && then.Seconds > seconds)
        {

            indexItem = Random.Range(0, items.Length);
            Debug.Log(" INDEXITEM " + indexItem);

            ShowItems();

            internItem.SetActive(true);
        }
        internItem.transform.DORotate(Vector3.up,0.10f,RotateMode.WorldAxisAdd);
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && internItem.activeSelf) {  // Only the players can take the items in the base
            Debug.Log("ENTRO AL TRIGEER DE BASE CONTROLLER ");
            now = System.DateTime.Now.TimeOfDay;
            _peitem = internItem.GetComponent<Item>();
            if (items[indexItem].name.Equals("Botiquin"))
            {
                Debug.Log(" SE AGREGA HEALTH " + _peitem.getValue());
                addEnegyToPlayer((EnergyItem)_peitem, other.gameObject);
            }
            else if (items[indexItem].name.Equals("MinigunAmmo") && internItem.activeSelf)
            {
                addBiggunAmmo((MinigunBulletsItem)_peitem, other.gameObject);
            }
            else if (items[indexItem].name.Equals("ShotGunnAmmo") && internItem.activeSelf)
            {
                addShotgunAmmo((DoubleBarrelBulletsItem)_peitem, other.gameObject);
            }
            internItem.SetActive(false);
            // Add the item to the player
         }
    
    }

    private void addEnegyToPlayer(EnergyItem _eitem,GameObject _player)
    {
        PlayerController _playerC = _player.GetComponent<PlayerController>();

        if (_playerC != null)
        {
            _playerC.addItem("Health",_eitem.healthLevel);
        }

    }

    private void addBiggunAmmo(MinigunBulletsItem _item, GameObject _player)
    {
        PlayerController _playerC = _player.GetComponent<PlayerController>();
        
        _playerC.addItem("BigBarrelBullets", _item.value);
    }

    private void addShotgunAmmo(DoubleBarrelBulletsItem _item, GameObject _player)
    {
        PlayerController _playerC = _player.GetComponent<PlayerController>();

        _playerC.addItem("DoubleBarrelBullets", _item.value);
    }


}
