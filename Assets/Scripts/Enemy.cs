using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject ExplosionFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 10;

    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        AddBoxColider();

        scoreBoard = FindObjectOfType<ScoreBoard>();
    }
        
    private void AddBoxColider()
    {
        Collider enemyColider = gameObject.AddComponent<BoxCollider>();
        enemyColider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hits < 1)
        {
            ProcessKillEnemy();
        }
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        //Todo hit FX
        hits--;
    }

    private void ProcessKillEnemy()
    {
        GameObject fx = Instantiate(ExplosionFX, transform.position, Quaternion.identity);

        fx.transform.parent = parent;

        Destroy(gameObject);
    }
}
