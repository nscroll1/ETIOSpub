using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsTime
{
    System.TimeSpan startTime;
    GameObject bullet;
    float delay;

    public BulletsTime(System.TimeSpan _time, GameObject _object, float _delay)
    {
        StartTime = _time;
        Bullet = _object;
        Delay = _delay;
    }

    public System.TimeSpan StartTime { get => startTime; set => startTime = value; }
    public GameObject Bullet { get => bullet; set => bullet = value; }
    public float Delay { get => delay; set => delay = value; }
}