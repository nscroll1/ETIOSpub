using System;
using UnityEngine;

public interface Enemy
{
    public void setSquareColor();
    public void addHit();
    public int getHits();
    public bool isDead();

    public int getMaxHits();

    public void setImDead();

    public void hideWeapon();

    public Animator getAnimator();

    public int getValue();

}
