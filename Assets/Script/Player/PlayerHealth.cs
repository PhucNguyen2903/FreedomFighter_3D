using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    //public static PlayerHealth instance;
    //public static PlayerHealth Instance { get => instance; }



    public int maxHP;
    public UnityEvent onDie;
    public UnityEvent onTakeDamage;
    public GameObject PopupGameOver;
    public UnityEvent<int, int> onHealthChanged;

    [SerializeField] public int _hp;

    private bool isDead => _hp <= 0;

    public int Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            onHealthChanged.Invoke(_hp, maxHP);
        }

    }

    

    private void Awake()
    {
        //if (PlayerHealth.instance != null) Debug.LogError("Only 1 PlayerHealth allow  to exist");       
        //PlayerHealth.instance = this;
    }
    private void Start()
    {
        _hp = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        Hp -= damage;
        onTakeDamage.Invoke();
        if (isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("============ Player Die");
        Time.timeScale = 0;
        onDie.Invoke();
        PopupGameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void IncreaseHP( int value)
    {
        _hp += value;
    }
}
