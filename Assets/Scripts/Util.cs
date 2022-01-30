using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util 
{

    public static bool isPlayerOnDistance(Transform _from, Transform _toSee)
    {

       
        float targetDistance = Vector2.Distance(_toSee.transform.position, _from.transform.position);
        Debug.Log("DISTANCIAS :" + targetDistance);
        if (targetDistance < 10f)
            return true;

        return false;
    }


    public static bool isPlayerOnDistance(Transform _from, Transform _toSee,float _distance)
    {


        float targetDistance = Vector2.Distance(_toSee.transform.position, _from.transform.position);
        Debug.Log("DISTANCIAS :" + targetDistance);
        if (targetDistance < _distance)
            return true;

        return false;
    }


    public static bool isPlayerOnDistance(Vector3 _from, Vector3 _to)
    {

        Debug.Log("DISTANCIA!!! " + Vector3.Distance(_from, _to));
        if (Vector3.Distance(_from, _to) <= 30f)
            return true;

        return false;

    }

    public static bool isPlayerOnDistance(Vector3 _from, Vector3 _to, float _distance)
    {

        Vector3 res = _from - _to;
        bool bres = false;
        Debug.Log("DISTANCIA!!! " + res.x + " MAG " + res.magnitude);


        if (res.magnitude <= _distance)
            bres = true;


        return bres;

    }


    public static bool isPlayerBetween(Vector3 _from, Vector3 _to, float _distance1,float _distance2)
    {
        if (Vector3.Distance(_from, _to) >= _distance1 &&
            Vector3.Distance(_from, _to) <  _distance2)
            return true;

        return false;
    }

    public static bool isPayerAlive(GameObject _toSee)
    {

         PlayerController _comp = _toSee.GetComponent<PlayerController>();
         if(_comp.getImDead())
            return false;

        return true;
    }

    public static System.Boolean isPLayerNear()
    {

       


        return false;
    }

    public  static GameObject findPlayer(GameObject[] transformArray)
    {
        

        foreach (GameObject _object in transformArray)
        {
            if (_object.tag.Equals("Player"))
            {
                return _object;
            }
        }

        return null;
    }

    public static void checkForRemoveBullets(List<BulletsTime> _bulletsFired)
    {
        try
        {
            foreach (BulletsTime _bitems in _bulletsFired)
            {
                System.TimeSpan now = System.DateTime.Now.TimeOfDay -
                                      _bitems.StartTime;
                if (now.Seconds >= _bitems.Delay)
                {
                    Object.Destroy(_bitems.Bullet);
                    _bulletsFired.Remove(_bitems);
                }
            }
        } catch (System.Exception e)
        {

        }
    }


}
