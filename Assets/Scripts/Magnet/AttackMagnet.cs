using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMagnet : MonoBehaviour
{
    [SerializeField] private EnemyMagnet enemyMagnet;
    private bool isNeedCallEnemy { get => countDown == 0f;}
    private List<EnemyMagnet> enemyMaget_list;

    public float countDownMax = 5f;
    private float countDown = 0f;

    void Start()
    {
        enemyMaget_list = new List<EnemyMagnet>();

        StartCoroutine(CallEnemy());
    }
    IEnumerator CallEnemy()
    {
        while (true)
        {
            yield return new WaitUntil(() => isNeedCallEnemy);
            float _randomRange_Y = Random.Range(-5f, 5f);
            EnemyMagnet _enemyMagnet = new EnemyMagnet();
            _enemyMagnet.enemyMagnetObj = Instantiate(enemyMagnet.enemyMagnetPrefab, transform.position + Vector3.right * _randomRange_Y, Quaternion.identity);
            _enemyMagnet.rigidbody2D = _enemyMagnet.enemyMagnetObj.GetComponent<Rigidbody2D>();
            StartCoroutine(WakeUpForEnemy(_enemyMagnet));
            enemyMaget_list.Add(_enemyMagnet);
            yield return null;
        }
    }

    IEnumerator WakeUpForEnemy(EnemyMagnet _enemyMagnet)
    {
        // Going to player
        while (!_enemyMagnet.isBeTrigger)
        {
            _enemyMagnet.rigidbody2D.velocity = new Vector2(0, -1f);
            yield return null;
        }
    }


    private void Update()
    {
        countDown += CalculateCountDown();
    }

    private float CalculateCountDown()
    {
        return countDownMax >= countDown ? Time.deltaTime : -countDown;
    }
}

[System.Serializable]
public class EnemyMagnet
{
    public GameObject enemyMagnetPrefab;
    [HideInInspector]public GameObject enemyMagnetObj; // In the scene
    [HideInInspector] public Rigidbody2D rigidbody2D;
    //被玩家用正確磁極碰到
    public bool isBeTrigger = false; 

    //當前磁極
    public Magnet.MagneticPole m_MagneticPole;
}