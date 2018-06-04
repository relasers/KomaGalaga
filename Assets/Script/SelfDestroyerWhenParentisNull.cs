using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyerWhenParentisNull : MonoBehaviour {

    
    bool DestroyEnable = false;
    Coroutine SelfDestroyerCoroutine;

    IEnumerator SelfDestroyer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.parent == null && !DestroyEnable)
        {
            ParticleSystem particlesystem = transform.GetComponent<ParticleSystem>();
            if (particlesystem)
            {
                particlesystem.Stop();
            }

            SelfDestroyerCoroutine = StartCoroutine(SelfDestroyer(3));
        }
	}
}
