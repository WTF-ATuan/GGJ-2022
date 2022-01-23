using System.Collections;
using System.Collections.Generic;
using Magnet;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;
using Extra;

public class AttackMagnet : MonoBehaviour
{
    // 單例
    public static AttackMagnet _;

    public int HP
    {
        get => _HP;
        set
        {
            _HP = value;
            if (value <= 0)
            {
                print("Boss死亡");
                BossDead.InvokeEvent();
            }
        }
    }
    public int _HP = 10;

    public Enemy enemyPrefab;
    public AddHP AddHPPrefab;
    public Enemy_Backtrack enemy_Backtrack;
    [SerializeField] private Actor.Actor actor;

    public float countDownMax = 5f;

    public float MaxX = 20;
    public float MinMove = 2;
    public float MaxMove = 10;

    /// <summary> 下次發射補血子彈的時間 </summary>
    public int AddHPIndex;

    public readonly DomainEvent BossDead = new DomainEvent();

    private void Awake()
    {
        _ = this;
    }

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
            
            var obj = actor.GethealthObject();
            if (obj != null)
            {
                Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                enemy.magneticPole = Random.Range(0, 2) > 0 ? MagneticPole.North : MagneticPole.South;
                enemy.End_Act += () => {
                    if(obj.Open)
                    {
                        //print($"擊中{obj.Index}號物件");
                        obj.Open = false;
                        Destroy(enemy.gameObject);
                    }
                };
                enemy.Move(obj.Index, obj.transform.position);

                AddHPIndex--;
                if (AddHPIndex <= 0)
                {
                    AddHP addHP = Instantiate(AddHPPrefab, transform.position, Quaternion.identity);
                    addHP.magneticPole = Random.Range(0, 2) > 0 ? MagneticPole.North : MagneticPole.South;
                    addHP.Move(obj.Index, obj.transform.position);
                    addHP.End_Act += () => {
                        if (obj.Open)
                        {
                            Destroy(enemy.gameObject);
                        }
                    };

                    AddHPIndex = Random.Range(5, 10);
                }
            }
            yield return null;
        }
    }
}