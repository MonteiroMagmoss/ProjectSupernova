using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;

    const string PLAYER_TAG = "Player";

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        player = FindAnyObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) return;

        agent.SetDestination(player.transform.position);
    }
}
