using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    
    private NavMeshAgent ThisAgent = null;
    private Transform Player = null;

    private void Awake()
    {
        ThisAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        ThisAgent.SetDestination(Player.position);
    }
}
