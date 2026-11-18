using System;
using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int starterHealth = 100;
    [SerializeField] int damage = 10;
    [SerializeField] float damageTime = .5f;
    [SerializeField] float fireRate = .5f;
    [SerializeField] GameObject damageScreen;
    [SerializeField] Slider lifeSlider;
    [SerializeField] ParticleSystem fireEffect;

    [SerializeField] GameObject objectTest;
    [SerializeField] float currenHealth;

    StarterAssetsInputs starterAssetsInputs;
    float tempoUltimoDisparo = 0;

    const string ENEMY_TAG = "Enemy";

    void Awake()
    {
        lifeSlider.value = starterHealth;
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
    public void TakeDamage()
    {
        currenHealth -= damage;
        lifeSlider.value = currenHealth;
        Debug.Log(currenHealth);
        damageScreen.SetActive(true);
        StartCoroutine(DisableScreenDamage(damageTime));
    }
}
