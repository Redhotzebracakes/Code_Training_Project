using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 3;
    [SerializeField] private int hp = 3;

    [SerializeField] private UnityEvent OnDamaged;
    [SerializeField] private UnityEvent OnZero;
    public void Damage(int hpAmount)
    {
        hp -= hpAmount;
        Debug.Log("hp amount changed by " + hpAmount + " and is now " + hp);

        //tell any subscriber to this event that damage happened!
        OnDamaged?.Invoke();

        //If we hit zero health, raise the OnZero event with all that registered
        if (hp <= 0)
        {
            OnZero?.Invoke();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
