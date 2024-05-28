using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform target;
    private GameController gameController;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            gameController.LooseGame();
        }
    }
}
