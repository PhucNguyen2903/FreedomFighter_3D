using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Health : MonoBehaviour
{

    private static Health instance;
    public static Health Instance { get => instance; }

    public int maxHealthPoint;
    public Animator anim;
    public PhotonView PV;
    public UnityEvent onDie;
    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent onTakeDamge;

    public int death;
    public string itemSpawnerName;
    public string zombieName;


    private int _healthPointValue;

    public int HealthPoint
    {
        get => _healthPointValue;
        set
        {
            _healthPointValue = value;
            onHealthChanged.Invoke(_healthPointValue, maxHealthPoint);
        }

    }

    private bool IsDead => HealthPoint <= 0;



    private void Awake()
    {
        Health.instance = this;
    }
    private void Start()
    {
        HealthPoint = maxHealthPoint;
    }
    private void Update()
    {

    }


    [PunRPC]
    public void TakeDamage(int damage)
    {

        Debug.Log("==================== Take Damage on Zombie health");
        if (IsDead) return;
        HealthPoint -= damage;
        onTakeDamge.Invoke();
        if (IsDead)
        {
            Die();
            onDie.Invoke();
        }
    }

    private void Die()
    {
        death++;
        StartCoroutine(WaittoDisableI());

    }

    IEnumerator WaittoDisableI()
    {
        spawedItemOnDead();
        yield return new WaitForSeconds(5f);
        PoolZombie.Instance.ReturnPool(this.zombieName);
        ZombieController.Instance.enemySpawner.Despawn(transform);

    }

    public void CheckNameZombieDie()
    {
        if (zombieName == "ZombieNormal")
        {
            itemSpawnerName = ZombieController.Instance.dropItemSpawner.newHealthBox;
        }
        else if (zombieName == "ZombieAdvance")
        {
            itemSpawnerName = ZombieController.Instance.dropItemSpawner.newHealthBox;
        }
        else if (zombieName == "ZombieBoom")
        {
            itemSpawnerName = ZombieController.Instance.dropItemSpawner.bullet;
        }
        else
        {
            itemSpawnerName = "";
        }


    }

    public void spawedItemOnDead()
    {
        CheckNameZombieDie();
        if (itemSpawnerName == "") return;
        int random = Random.Range(0, 9);
        if (random == 5)
        {
            PV.RPC("Rpc_SpawnItem", RpcTarget.All);
        }

    }

    [PunRPC]
    public void Rpc_SpawnItem()
    {
        Transform newItem = ZombieController.Instance.dropItemSpawner.Spawn(itemSpawnerName, transform.position, transform.rotation);
        if (newItem == null) return;
        newItem.gameObject.SetActive(true);
    }

}
