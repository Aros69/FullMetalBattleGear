using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputMenu : MonoBehaviour {

    Coroutine fadeCoroutine;

    public float fadeDuration = 0.5f;

    public RectTransform Tr { private set; get; }
    public List<Image> MyImages { private set; get; }

    void Start()
    {

        Tr = GetComponent<RectTransform>();
        MyImages = new List<Image>(GetComponentsInChildren<Image>());
        
        MyImages.ForEach(n => n.color = new Color(1, 1, 1, 0));


    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeIn(fadeDuration));
        }	

        else if (Input.GetKeyUp(KeyCode.JoystickButton3))
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeOut(fadeDuration));
        }
	}

    IEnumerator FadeIn(float duration)
    {
        float t = MyImages[0].color.a;
        float startOffset = duration * t;
        float startTime = Time.time;
        float timePassed = 0;

        while (startTime + duration > Time.time + startOffset)
        {
            timePassed += Time.deltaTime;
            t = ((Time.time + startOffset) - startTime) / duration;

            MyImages.ForEach(n => n.color = new Color(1, 1, 1, t));
            yield return null;
        }
        MyImages.ForEach(n => n.color = new Color(1, 1, 1, 1));

    }

    IEnumerator FadeOut(float duration)
    {
        float t = 1 - MyImages[0].color.a;
        float startTime = Time.time;
        float startOffset = duration * t;
        float timePassed = 0;

        while (startTime + duration > Time.time + startOffset)
        {
            timePassed += Time.deltaTime;
            t = 1 - ((Time.time + startOffset) - startTime) / duration;

            MyImages.ForEach(n => n.color = new Color(1, 1, 1, t));
            yield return null;
        }
        MyImages.ForEach(n => n.color = new Color(1, 1, 1, 0));

    }
}
