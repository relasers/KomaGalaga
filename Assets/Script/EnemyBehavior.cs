using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    StayOrigin,
    Moving
}

public class EnemyBehavior : MonoBehaviour {

    public Vector2 OriginPosition = new Vector2(0,0);
    public Vector2 Anchor = new Vector2(0,0);

    Rigidbody2D rigidbody2D;
    EnemyState state = EnemyState.StayOrigin;


    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {



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

            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.MovePosition(OriginPosition + directionOriginToAnchor*distance);
        }
    }
}
