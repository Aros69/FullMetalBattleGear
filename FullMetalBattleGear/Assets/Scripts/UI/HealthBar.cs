using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Image MyImage { private get; set; }
    public float duration = 0.5f;
    public AnimationCurve lifeLossCurve;

    Coroutine healthUpdateCoroutine;


    void Start () {
        MyImage = GetComponent<Image>();
	}

    public void UpdateHealth(float percent)
    {
        if (healthUpdateCoroutine != null)
            StopCoroutine(healthUpdateCoroutine);

        healthUpdateCoroutine = StartCoroutine(UpdateImage(percent, duration));
    }

    IEnumerator UpdateImage(float percent, float duration)
    {
        float timePassed = 0;
        float t = 0;
        float startFillAmount = MyImage.fillAmount;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            t = timePassed / duration;

            float evaluatedT = lifeLossCurve.Evaluate(t);

            MyImage.fillAmount = Mathf.Lerp(startFillAmount, percent, evaluatedT);

            yield return null;
        }
    }
}
