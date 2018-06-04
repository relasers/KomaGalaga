using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour {

    public Stage[] stage = new Stage[1];

    public Vector2 Anchor = new Vector2(0, 2.36f);
    public Vector2 LeftTop = new Vector2(-3.5f,4.36f);
    // 유닛 배치 간격 
    public float ArrangementOffset = 1.0f;

    public GameObject Moono;
    public GameObject Diamo;
    public GameObject Graba;

    List<GameObject> enemies;

    // 리스트 내 적의 오브젝트가 destroy 되었을 경우 이를 제거하는 코루틴
    Coroutine Coroutine_EnemyRemoverOnList;

    IEnumerator EnemyRemoverOnList(float delay)
    {
        while (true)
        {
            for (int i =0; i< enemies.Count ;++i)
            {
                if (!enemies[i]) enemies.RemoveAt(i);
            }

            Debug.Log("Enemy NUm :: " + enemies.Count);
            yield return new WaitForSeconds(delay);
        }
    }


    public float HullStageDistancer = 2.0f;

    private void Awake()
    {
        enemies = new List<GameObject>();
        stage = new Stage[1];
        // 배열 할당은 Awake에서 진행할 것.
        stage[0] = new Stage();
        
    }

    // Use this for initialization
    void Start () {


        Coroutine_EnemyRemoverOnList = StartCoroutine(EnemyRemoverOnList(1));

        stage[0].ReadData("stage01");

        
        
       

        for (int i = 0; i < stage[0].WidthCell; ++i)
        {
            for (int j = 0; j < stage[0].HeightCell; ++j)
            {
                GameObject new_enemy;
                EnemyBehavior enemy_behavior;
                // 오브젝트의 초기 위치
                Vector2 OriginPosition;

                switch (stage[0].GetData(i, j).ToString())
                {
                    case "0":
                        break;
                    case "1":

                        OriginPosition = new Vector2(LeftTop.x + i * ArrangementOffset, LeftTop.y - j * ArrangementOffset);

                        new_enemy = Instantiate(Moono,OriginPosition, Quaternion.identity);
                        enemy_behavior = new_enemy.GetComponent<EnemyBehavior>();
                        if(enemy_behavior) enemy_behavior.SetAnchorAndOriginPosition(Anchor, OriginPosition);

                        enemies.Add( new_enemy );
                        break;
                    case "2":
                        OriginPosition = new Vector2(LeftTop.x + i * ArrangementOffset, LeftTop.y - j * ArrangementOffset);

                        new_enemy = Instantiate(Diamo, OriginPosition, Quaternion.identity);
                        enemy_behavior = new_enemy.GetComponent<EnemyBehavior>();
                        if (enemy_behavior) enemy_behavior.SetAnchorAndOriginPosition(Anchor, OriginPosition);

                        enemies.Add(new_enemy);
                        break;
                    case "3":
                        OriginPosition = new Vector2(LeftTop.x + i * ArrangementOffset, LeftTop.y - j * ArrangementOffset);

                        new_enemy = Instantiate(Graba, OriginPosition, Quaternion.identity);
                        enemy_behavior = new_enemy.GetComponent<EnemyBehavior>();
                        if (enemy_behavior) enemy_behavior.SetAnchorAndOriginPosition(Anchor, OriginPosition);

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
                    case "8":
                        break;
                    case "9":
                        break;
                    default:
                        break;
                }
    
    
            }
        }


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
                    behavior.MovingOnAnchor(Mathf.Sin(Time.fixedDeltaTime) * HullStageDistancer);
                }
            }
            
        }


    }

    // Update is called once per frame
    void Update () {
		
	}
}
