using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyWhenAnimationEnd : MonoBehaviour {

    public float delay = 0f;

    // Use this for initialization
    void Start()
    {
        // 인자로 주어진 시간 이후 제거
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
