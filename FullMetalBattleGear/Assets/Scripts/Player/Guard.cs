using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

    Animator animator;
    int play;

    void Start () {
        gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        play = Animator.StringToHash("Play");

    }

    public void Play()
    {
        animator.SetBool(play, true);
    }
    public void Stop()
    {
        animator.SetBool(play, false);
        Invoke("Disapear", 0.5f);
    }




    void Disapear()
    {
        gameObject.SetActive(false);
    }
}
