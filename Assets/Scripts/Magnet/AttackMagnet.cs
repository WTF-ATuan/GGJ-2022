using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMagnet : MonoBehaviour
{
    [SerializeField] private int enemyMagnetManagerMax;
    private int enemyMagnetManager;

    [SerializeField] private GameObject enemyMagnet;

    private bool isNeedCallEnemy { get => countDown == 0f; }
    private List<GameObject> enemyMaget_list;

    public float countDownMax = 5f;
    private float countDown = 0f;

    void Start()
    {
        enemyMaget_list = new List<GameObject>();

        StartCoroutine(CallEnemy());
    }

    IEnumerator CallEnemy()
    {
        while (true)
        {
            yield return new WaitUntil(() => isNeedCallEnemy);
            float _randomRange_Y = Random.Range(-5f, 5f);
            GameObject _enemyMagnet = Instantiate(enemyMagnet, transform.position + new Vector3(_randomRange_Y, 0, 0), Quaternion.identity);
            enemyMaget_list.Add(_enemyMagnet);
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
