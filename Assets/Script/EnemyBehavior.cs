using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    StayOrigin,
    BackToOrigin,
    Created,
    StartMoving,
    Moving
}

public class EnemyBehavior : MonoBehaviour {

    public Vector2 PrevPosition = new Vector2(0, 0);
    public Quaternion PrevQuaternion = new Quaternion(0,0,0,0);

    public Vector2 OriginPosition = new Vector2(0, 0);
    public Vector2 Anchor = new Vector2(0, 0);
    
    public float StartMovingRotateDirection = 0; // 공격 시작시 회전 방향
    public float StartMovingSpeed = 0.1f; // 공격 시작시 이동 속도
    public float StartRotatingSpeed = 30.0f; // 공격 시작시 회전 속도
    public EnemyState state = EnemyState.Created;
    public int SpawnPathIndex = 0;

    int Animator_SpawnPathIndex = 0;

    // 
    public float moving_delta = 0;
    public float attacking_delay = 10.0f;

  
    IEnumerator StateChangeCreatedToBackToOrigin()
    {
        Animator animator = GetComponent<Animator>();

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        PrevPosition = new Vector2(transform.position.x , transform.position.y);
        PrevQuaternion = transform.rotation;
        animator.enabled = false;

        state = EnemyState.BackToOrigin;
        moving_delta = 0.0f;
    }

    // Use this for initialization
    void Start () {
        Animator animator = GetComponent<Animator>();
        Animator_SpawnPathIndex = Animator.StringToHash("PathIndex");
        animator.SetFloat(Animator_SpawnPathIndex,SpawnPathIndex);
        StartCoroutine(StateChangeCreatedToBackToOrigin());
    }

    private void FixedUpdate()
    {
        
        

    }

    // Update is called once per frame
    void Update () {
        
        if (state == EnemyState.BackToOrigin)
        {
            moving_delta += Time.deltaTime;
            transform.position = Vector2.Lerp( PrevPosition ,OriginPosition ,Mathf.SmoothStep(0,1,moving_delta));
            transform.rotation = Quaternion.Slerp(PrevQuaternion , Quaternion.identity, Mathf.SmoothStep(0, 1, moving_delta));
            if (moving_delta > 1.0f) state = EnemyState.StayOrigin;
        }

        if (state == EnemyState.StartMoving)
        {
            moving_delta += Time.deltaTime;
            transform.Rotate(new Vector3(0,0,1), StartRotatingSpeed *Time.deltaTime );
            transform.Translate(transform.up * StartMovingSpeed* Time.deltaTime);
        }

        //rigidbody2D.MovePosition(OriginPosition);

	}

    public void SetAnchorAndOriginPosition(Vector2 _Anchor, Vector2 _Origin)
    {
        Anchor = _Anchor;
        OriginPosition = _Origin;
    }

    public void MovingOnAnchor(float distance)
    {
        if (state == EnemyState.StayOrigin)
        {
            Vector2 directionOriginToAnchor = (Anchor - OriginPosition);
            directionOriginToAnchor.Normalize();

            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.MovePosition(OriginPosition + directionOriginToAnchor*distance);
        }
    }

    public void StartAttack()
    {
        state = EnemyState.StartMoving;
        StartMovingRotateDirection = Mathf.Sign(transform.position.x);
        // 0이 되는 것 방지
        if (Mathf.Approximately(StartMovingRotateDirection, 0.0f)) StartMovingRotateDirection = 1.0f;

        transform.Rotate(new Vector3(0,0,1),180);

        moving_delta = 0.0f;
    }


}
