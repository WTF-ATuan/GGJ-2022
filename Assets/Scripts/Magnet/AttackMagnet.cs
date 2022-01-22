using System.Collections;
using System.Collections.Generic;
using Magnet;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackMagnet : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Actor.Actor actor;

    private bool IsNeedCallEnemy
    {
        get => _countDown == 0f;
    }

    private List<Enemy> _enemyList = new List<Enemy>();

    public float countDownMax = 5f;
    private float _countDown = 0f;


    private void Start()
    {
        StartCoroutine(CallEnemy());
    }

    private IEnumerator CallEnemy()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsNeedCallEnemy);
            var randomRangeY = Random.Range(-5f, 5f);
            var enemy = Instantiate(enemyPrefab, transform.position + Vector3.right * randomRangeY,
                Quaternion.identity);
            var randomPole = Random.Range(0, 2);
            enemy.SetMagneticPole(randomPole > 0 ? MagneticPole.North : MagneticPole.South);
            var randomTrack = Random.Range(0, 3);
            enemy.Move(randomTrack, actor.transform.position);
            yield return null;
        }
    }

    private void Update()
    {
        _countDown += CalculateCountDown();
    }

    private float CalculateCountDown()
    {
        return countDownMax >= _countDown ? Time.deltaTime : -_countDown;
    }
}