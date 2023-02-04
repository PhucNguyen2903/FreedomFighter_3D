using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Singleton<ZombieController>
{
    public EnemySpawner enemySpawner;
    public DropItemSpawner dropItemSpawner;
    public ZombieManager zombieManager;
}
