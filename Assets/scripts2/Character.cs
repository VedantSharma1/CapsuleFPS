using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public static string name;
    public static int speed;
    
    public virtual void PrintStatsInfo()
    {
        Debug.LogFormat("Enemy: {0} - , speed: {1}", name, speed);
    }
}

public class Enemy1: Character
{   
    
    public override void PrintStatsInfo()
    {
        Debug.LogFormat("Collide with enemy1");
    }
}

public class Enemy2 : Character
{
    public override void PrintStatsInfo()
    {
        Debug.LogFormat("Collide with enemy2");
    }
}

