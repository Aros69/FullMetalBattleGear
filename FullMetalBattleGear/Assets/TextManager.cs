using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public GameObject attack;
    public GameObject guard;

    public static GameObject staticGuard;

    List<Image> images = new List<Image>();
    Image attackImage;
    private void Awake()
    {
        staticGuard = guard;
    }

    void Start () {
        images = new List<Image>(guard.GetComponentsInChildren<Image>());
        
        Invoke("LateStart",0.1f);
    }

    void LateStart()
    {
        guard.SetActive(false);

    }

    private void Update()
    {
        if ((Input.GetAxisRaw(InputManager.Input(InputKey.lt, 0)) != 0 || Input.GetAxisRaw(InputManager.Input(InputKey.rt, 0)) != 0
            || Input.GetAxisRaw(InputManager.Input(InputKey.lt, 1)) != 0 || Input.GetAxisRaw(InputManager.Input(InputKey.rt, 1)) != 0)
            && attack.activeSelf == true)
        {
            attack.SetActive(false);
            guard.SetActive(true);
        }

        else if ((Input.GetAxisRaw(InputManager.Input(InputKey.lt, 0)) == 0 && Input.GetAxisRaw(InputManager.Input(InputKey.rt, 0)) == 0
            && Input.GetAxisRaw(InputManager.Input(InputKey.lt, 1)) == 0 && Input.GetAxisRaw(InputManager.Input(InputKey.rt, 1)) == 0)
            && attack.activeSelf == false)
        {
            attack.SetActive(true);
            guard.SetActive(false);
        }
    }

}
