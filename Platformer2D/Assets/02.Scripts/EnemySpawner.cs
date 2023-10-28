using Platformer.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Platformer.Controllers;

public class EnemySpawner : MonoBehaviour
{
    private BoxCollider2D _spawnArea;
    [SerializeField] private float _spawnTime;
    private float _spawnTimer;
    private Vector2 _spawnPoint;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] PoolOfEnemy _enemyPool;

    public bool canSpawn
    {
        get
        {
            if (_spawnTimer < _spawnTime)
            {
                _spawnTimer += Time.deltaTime;
                return false;
            }

            float randomX = Random.Range(transform.position.x - _spawnArea.size.x / 2, transform.position.x + _spawnArea.size.x / 2);
            float randomY = Random.Range(transform.position.y - _spawnArea.size.y / 2, transform.position.y + _spawnArea.size.y / 2);
            Vector2 startPos = new Vector2(randomX, randomY);
            Vector2 endPos = new Vector2(startPos.x, transform.position.y - _spawnArea.size.y / 2);

            RaycastHit2D hit = Physics2D.Linecast(startPos,
                                                  startPos + Vector2.down * Vector2.Distance(startPos, endPos),
                                                  _groundMask);

            if (hit.collider == null)
                return false;

            _spawnPoint = hit.point;
            return true;
        }
    }

    private void Awake()
    {
        _spawnArea = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (canSpawn)
        {
            EnemyController enemy = _enemyPool.pool.Get();
            enemy.transform.position = _spawnPoint;
            _spawnTimer = 0.0f;
        }
            
    }
}
