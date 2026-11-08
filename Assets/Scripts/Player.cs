using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int starterHealth = 100;
    [SerializeField] int damage = 10;
    [SerializeField] float damageTime = .5f;
    [SerializeField] GameObject damageScreen;
    [SerializeField] int currenHealth;

    const string ENEMY_TAG = "Enemy";

    void Awake()
    {
        currenHealth = starterHealth;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currenHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(ENEMY_TAG))
        {
            currenHealth -= damage;
            Debug.Log(currenHealth);
            damageScreen.SetActive(true);
            StartCoroutine(DisableScreenDamage(damageTime));
        }
    }
    IEnumerator DisableScreenDamage(float time)
    {
        yield return new WaitForSeconds(time);
        damageScreen.SetActive(false);
    }
}
