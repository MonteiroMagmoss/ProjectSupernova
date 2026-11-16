using System;
using System.Collections;
using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int starterHealth = 100;
    [SerializeField] int damage = 10;
    [SerializeField] float damageTime = .5f;
    [SerializeField] float fireRate = .5f;
    [SerializeField] GameObject damageScreen;
    [SerializeField] ParticleSystem fireEffect;

    [SerializeField] GameObject objectTest;
    [SerializeField] int currenHealth;

    StarterAssetsInputs starterAssetsInputs;
    float tempoUltimoDisparo = 0;

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
        tempoUltimoDisparo += Time.deltaTime;

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

        if (tempoUltimoDisparo >= fireRate)
        {
            fireEffect.Play();

            RaycastHit hit;

            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                Instantiate(objectTest, hit.point, Quaternion.identity);
            }

            Debug.Log("Botão pressionado!");
            tempoUltimoDisparo = 0f;
        }
    }
    void TakeDamage()
    {
        currenHealth -= damage;
        Debug.Log(currenHealth);
        damageScreen.SetActive(true);
        StartCoroutine(DisableScreenDamage(damageTime));
    }
}
