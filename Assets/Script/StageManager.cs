using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct EnemyInfo
{
    public string type;
    public Vector2 OriginPosition;
}

public class StageManager : MonoBehaviour {

    public Stage[] stage = new Stage[1];

    public Vector2 Anchor = new Vector2(0, 2.36f);
    public Vector2 LeftTop = new Vector2(-3.5f,4.36f);
    // 유닛 배치 간격 
    public float ArrangementOffset = 0.5f;

    public GameObject Moono;
    public GameObject Diamo;
    public GameObject Graba;

    
    List<EnemyInfo> enemyinfos;
    List<GameObject> enemies;

    public int MaxSpawnPathIndex = 8;
    public int MaxUnitPerPath = 10;
    public float EnemyAttackDelay = 5.0f;

    // 리스트 내 적의 오브젝트가 destroy 되었을 경우 이를 제거하는 코루틴
    Coroutine Coroutine_EnemyRemoverOnList;

    IEnumerator EnemySpawner(float delay)
    {
        int spawn_count = 0;

        yield return new WaitForSeconds(delay * 5);

        while (enemyinfos.Count > 0)
        {
            EnemyInfo next_enemy;

            // 짝수일경우 앞에것, 반대일경우 뒤에걸 뽑는다.
            if ((int)( spawn_count / MaxUnitPerPath )  == 0)
                next_enemy = enemyinfos[0];
            else
                next_enemy = enemyinfos[enemyinfos.Count-1];

            enemyinfos.Remove(next_enemy);

            GameObject new_enemy;
            EnemyBehavior enemy_behavior;

            switch (next_enemy.type)
            {
                case "0":
                    break;
                case "1":
                    new_enemy = Instantiate(Moono, next_enemy.OriginPosition, Quaternion.identity);
                    enemy_behavior = new_enemy.GetComponent<EnemyBehavior>();
                    if (enemy_behavior)
                    {
                        enemy_behavior.SetAnchorAndOriginPosition(Anchor, next_enemy.OriginPosition);
                        enemy_behavior.SpawnPathIndex = (int)(spawn_count / MaxUnitPerPath);
                    }
                    enemies.Add(new_enemy);
                    break;
                case "2":
                    new_enemy = Instantiate(Diamo, next_enemy.OriginPosition, Quaternion.identity);
                    enemy_behavior = new_enemy.GetComponent<EnemyBehavior>();
                    if (enemy_behavior)
                    {
                        enemy_behavior.SetAnchorAndOriginPosition(Anchor, next_enemy.OriginPosition);
                        enemy_behavior.SpawnPathIndex = (int)(spawn_count / MaxUnitPerPath);
                    }
                    enemies.Add(new_enemy);
                    break;
                case "3":
                    new_enemy = Instantiate(Graba, next_enemy.OriginPosition, Quaternion.identity);
                    enemy_behavior = new_enemy.GetComponent<EnemyBehavior>();
                    if (enemy_behavior)
                    {
                        enemy_behavior.SetAnchorAndOriginPosition(Anchor, next_enemy.OriginPosition);
                        enemy_behavior.SpawnPathIndex = (int)(spawn_count / MaxUnitPerPath);
                    }
                    enemies.Add(new_enemy);
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;


            }
            




            spawn_count++;
            if(spawn_count % MaxUnitPerPath == 0 && spawn_count != 0)
                yield return new WaitForSeconds(delay*5);
            else
            yield return new WaitForSeconds(delay);

        }
    }

    IEnumerator EnemyRemoverOnList(float delay)
    {
        while (true)
        {
            for (int i =0; i< enemies.Count ;++i)
            {
                if (!enemies[i]) enemies.RemoveAt(i);
            }

            //Debug.Log("Enemy NUm :: " + enemies.Count);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator LaunchEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (enemies.Count <= 0) continue;

            GameObject enemy = enemies[Random.Range(0, enemies.Count - 1)];
            EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();
            if (behavior.state != EnemyState.StayOrigin)
                continue;

            behavior.StartAttack();

            yield return new WaitForSeconds(EnemyAttackDelay);
        }



    }
    public float HullStageDistancer = 2.0f;

    private void Awake()
    {
        enemies = new List<GameObject>();
        enemyinfos = new List<EnemyInfo>();

        stage = new Stage[1];
        // 배열 할당은 Awake에서 진행할 것.
        stage[0] = new Stage();
        
    }

    // Use this for initialization
    void Start () {


        Coroutine_EnemyRemoverOnList = StartCoroutine(EnemyRemoverOnList(1));
        StartCoroutine(LaunchEnemy());
        stage[0].ReadData("stage01");


        for (int i = 0; i < stage[0].WidthCell; ++i)
        {
            for (int j = 0; j < stage[0].HeightCell; ++j)
            {
               
                EnemyInfo new_enemyinfo;
                // 오브젝트의 초기 위치
                Vector2 OriginPosition;

                switch (stage[0].GetData(i, j).ToString())
                {
                    case "0":
                        break;
                    case "1":
                        OriginPosition = new Vector2(LeftTop.x + i * ArrangementOffset, LeftTop.y - j * ArrangementOffset);
                        new_enemyinfo = new EnemyInfo();
                        new_enemyinfo.type = "1";
                        new_enemyinfo.OriginPosition = OriginPosition;
                        enemyinfos.Add(new_enemyinfo);
                        break;
                    case "2":
                        OriginPosition = new Vector2(LeftTop.x + i * ArrangementOffset, LeftTop.y - j * ArrangementOffset);
                        new_enemyinfo = new EnemyInfo();
                        new_enemyinfo.type = "2";
                        new_enemyinfo.OriginPosition = OriginPosition;
                        enemyinfos.Add(new_enemyinfo);
                        break;
                    case "3":
                        OriginPosition = new Vector2(LeftTop.x + i * ArrangementOffset, LeftTop.y - j * ArrangementOffset);
                        new_enemyinfo = new EnemyInfo();
                        new_enemyinfo.type = "3";
                        new_enemyinfo.OriginPosition = OriginPosition;
                        enemyinfos.Add(new_enemyinfo);
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    default:
                        break;
                }
    
    
            }
        }

        // 적 기체들 소환
        StartCoroutine(EnemySpawner(0.25f));
    }

    // 전체 적 움직임 관리
    private void FixedUpdate()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy)
            {
                EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();
                if (behavior)
                {
                    behavior.MovingOnAnchor((0.5f + Mathf.Sin(Time.fixedTime)*0.5f) * HullStageDistancer);
                }
            }
            
        }


    }

    // Update is called once per frame
    void Update () {
		
	}
}
