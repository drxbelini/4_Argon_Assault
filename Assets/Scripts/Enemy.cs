using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject ExplosionFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;

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
        scoreBoard.ScoreHit(scorePerHit);

       GameObject fx = Instantiate(ExplosionFX, transform.position, Quaternion.identity);

        fx.transform.parent = parent;
        
        Destroy(gameObject);

    }


}
