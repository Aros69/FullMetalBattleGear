using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleBar : MonoBehaviour {

    public static BattleBar main;

    Vector2 startPos;
    Vector2 endPos;

    Coroutine flashCoroutine;
    
    public Color flashColor = Color.white;
    public Color barColor = Color.black;

    public float FightDuration { private set; get; }

    public int actions = 5;
    public float flashDuraction = 0.5f;

    public GameObject spacingBar;

    public RectTransform Tr { private set; get; }
    public Image MyImage { private set; get; }
    
    void Start () {

        main = this;

        FightDuration = FightManager.main.DureeAnimation() * 5f;

        Tr = GetComponent<RectTransform>();
        MyImage = GetComponent<Image>();

        startPos = Tr.anchoredPosition;
        endPos = Tr.anchoredPosition;
        endPos.x = Mathf.Abs(endPos.x);
        
    }

    private void OnValidate()
    {
        GetComponent<Image>().color = barColor;
    }

    public IEnumerator FightSequence(float duration, int actions)
    {
        float startTime = Time.time;
        float t = 0;
        int steps = 1;
        float timePassed = 0;

        while(startTime + duration > Time.time)
        {
            timePassed += Time.deltaTime;

            t = (Time.time - startTime) / duration;
            Tr.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            float sections = (1f / actions);

            if (t >= (sections * steps) - (sections / 2))
            {
                if (flashCoroutine != null)
                    StopCoroutine(flashCoroutine);
                flashCoroutine = StartCoroutine(SpriteFlash(flashDuraction));

                ActionHolder.Reveal(steps);
                steps++;

            }
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        ActionHolder.ResetCards();
    }

    IEnumerator SpriteFlash(float duration)
    {
        float timePassed = 0;
        float t = 0;

        while (t < 1)
        {
            timePassed += Time.deltaTime;
            t += 0.1f;

            MyImage.color = Color.Lerp(flashColor, barColor, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
