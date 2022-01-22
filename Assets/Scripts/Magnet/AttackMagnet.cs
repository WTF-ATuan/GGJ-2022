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

    public float countDownMax = 5f;

    public float MaxX = 20;
    public float MinMove = 2;
    public float MaxMove = 10;

    /// <summary> 下次發射補血子彈的時間 </summary>
    public int AddHPIndex;

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

            AddHPIndex--;
            if(AddHPIndex <= 0)
            {
                print("發射補血子彈");
                AddHPIndex = Random.Range(5, 10);
            }
            yield return transform.DOLocalMoveX(MovePot, MoveTime).SetEase(Ease.Linear).WaitForCompletion();
        }
    }

    private IEnumerator CallEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(countDownMax);
            
            //var ables = actor.GetCurrentAttackableComponentList();
            //var able = ables[Random.Range(0, ables.Count)];
            var obj = actor.GethealthObject();
            if (obj != null)
            {
                Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                int randomPole = Random.Range(0, 2);
                enemy.SetMagneticPole(randomPole > 0 ? MagneticPole.North : MagneticPole.South);
                enemy.End_Act += () => {
                    print($"擊中{obj.Index}號物件");
                    obj.Open = false;
                    Destroy(enemy.gameObject);
                };
                enemy.Move(obj.Index, obj.transform.position);
            }
            yield return null;
        }
    }
}