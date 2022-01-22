using System.Collections;
using System.Collections.Generic;
using Magnet;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

public class AttackMagnet : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Actor.Actor actor;


    private List<Enemy> _enemyList = new List<Enemy>();

    public float countDownMax = 5f;

    public float MaxX = 20;
    public float MinMove = 2;
    public float MaxMove = 10;

    private void Start()
    {
        StartCoroutine(CallEnemy());
        StartCoroutine(Move()); 
    }

    IEnumerator Move()
    {
        for(; ; )
        {
            float MoveTime = Random.Range(0.5f, 1f);
            float MovePot = Random.Range(MinMove, MaxMove) * (Random.Range(0, 2) == 0 ? 1 : -1);
            MovePot += transform.localPosition.x;
            MovePot = Mathf.Clamp(MovePot, -MaxX, MaxX);
            yield return transform.DOLocalMoveX(MovePot, MoveTime).SetEase(Ease.Linear).WaitForCompletion();
        }
    }

    private IEnumerator CallEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(countDownMax);
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            var randomPole = Random.Range(0, 2);
            enemy.SetMagneticPole(randomPole > 0 ? MagneticPole.North : MagneticPole.South);
            var randomTrack = Random.Range(0, 3);
            enemy.Move(randomTrack, actor.transform.position);
            yield return null;
        }
    }
}