using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            ProjectileMover mover = collision.GetComponent<ProjectileMover>();

            // 만약 충돌 대상 총알이 다른 오브젝트와 충돌한 적이 없다면?
            if (!mover.isCollisioned)
            {
                mover.SwitchCollisioned(true);
                Destroy(gameObject);

                // 총알 파티클 비활성화
                Transform particle = collision.transform.FindChild("Particle System");
                if (particle)
                {
                    particle.parent = null;

                }
                Destroy(collision.gameObject);
            }
            
        }

    }
}
