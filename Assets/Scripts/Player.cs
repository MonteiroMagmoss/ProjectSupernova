using System;
using System.Collections;
using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int starterHealth = 100;
    [SerializeField] int damage = 10;
    [SerializeField] float damageTime = .5f;
    [SerializeField] GameObject damageScreen;
    [SerializeField] ParticleSystem fireEffect;
    [SerializeField] int currenHealth;

    StarterAssetsInputs starterAssetsInputs;

    const string ENEMY_TAG = "Enemy";

    void Awake()
    {
        currenHealth = starterHealth;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
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
        ControleDisparo();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(ENEMY_TAG))
        {
            TakeDamage();
        }
    }
    IEnumerator DisableScreenDamage(float time)
    {
        yield return new WaitForSeconds(time);
        damageScreen.SetActive(false);
    }
    void ControleDisparo()
    {
        if(!starterAssetsInputs.shoot) return;

        fireEffect.Play();

        Debug.Log("Botão pressionado!");
    }
    void TakeDamage()
    {
        currenHealth -= damage;
        Debug.Log(currenHealth);
        damageScreen.SetActive(true);
        StartCoroutine(DisableScreenDamage(damageTime));
    }
}
