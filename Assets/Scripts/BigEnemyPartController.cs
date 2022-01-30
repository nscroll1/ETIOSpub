using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigEnemyPartController : MonoBehaviour
{

    private BigEnemy me;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Awake()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(" le pego A UNA EXTREMIDAD COLisioN [" + name + "] : " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (me == null)
            me = transform.root.GetComponent<BigEnemy>();
        if(other.tag.Equals("Wall"))
        {
           me.Wall = true;
        } 
        else if(other.tag.Equals("Player") && (name.Equals("Ctrl_Hand_IK_Left") || name.Equals("Ctrl_Hand_IK_Right")))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            Rigidbody playerr = other.transform.Find("ETIOS").GetComponent<Rigidbody>();

            //other.transform.DOShakePosition(25);
            playerr.AddForce(new Vector3(other.transform.position.x + 10, other.transform.position.y,other.transform.position.z+10),ForceMode.Impulse);
            player.addHit(10);

        }
        Debug.Log(" le pego A UNA EXTREMIDAD [" + name + "] : " + other.tag );
    }
}
