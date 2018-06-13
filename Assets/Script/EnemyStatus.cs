using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {

    public GameObject particle_on_Destroy;
    public int HP = 1;
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


                
                // 총알 파티클 비활성화
                Transform particle = collision.transform.FindChild("Particle System");
                if (particle)
                {
                    particle.parent = null;

                }



                if (particle_on_Destroy)
                {
                    GameObject new_particle = Instantiate(particle_on_Destroy,transform.position,transform.rotation);

                }

                Destroy(collision.gameObject);

                HP--;

                if(HP <= 0)
                    Destroy(gameObject);
            }
            
        }

    }
}
