using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour {

    public float speed = 150.0f;
    public Vector2 MovingDirection = new Vector2(0,0);
    Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {

        rigidbody2D.velocity = MovingDirection * speed * Time.fixedDeltaTime;

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ProjectileRemover"))
        {
            Debug.Log("remove");
            Destroy(gameObject);
        }
    }

}
