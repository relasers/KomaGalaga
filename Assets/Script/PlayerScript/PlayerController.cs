using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rigidbody2D;
    public float speed = 150.0f;
    public float shoot_delay = 0.4f;

    public GameObject PlayerProjectile;


    bool CanShoot = true;

    Coroutine coroutine_shooting;

    IEnumerator ShootBulletCoroutine(float delay)
    {

        GameObject newProjectile = Instantiate(PlayerProjectile,transform.position,Quaternion.identity);
        ProjectileMover mover = newProjectile.GetComponent<ProjectileMover>();
        mover.MovingDirection = new Vector2(0,1);





        CanShoot = false;
        yield return new WaitForSeconds(delay);
        CanShoot = true;
    }

    void ShootBullet()
    {
        if(CanShoot)
        coroutine_shooting = StartCoroutine(ShootBulletCoroutine(shoot_delay));
    }


    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        float velocityX = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
       // float velocityY = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;

        // 플레이어는 수평선으로만 움직인다.
        rigidbody2D.velocity = new Vector2(velocityX , 0);


    }

    // Update is called once per frame
    void Update () {

        if(0 < Input.GetAxis("Fire1"))
        ShootBullet();



    }
}
