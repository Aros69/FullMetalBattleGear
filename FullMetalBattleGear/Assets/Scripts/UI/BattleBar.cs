using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleBar : MonoBehaviour {

    Vector2 startPos;
    Vector2 endPos;

    Coroutine flashCoroutine;
    
    public Color flashColor = Color.white;
    public Color barColor = Color.black;

    public float fightDuration = 10f;
    public int actions = 5;
    public float flashDuraction = 0.5f;

    public GameObject spacingBar;

    public RectTransform Tr { private set; get; }
    public Image MyImage { private set; get; }
    
    void Start () {

        Tr = GetComponent<RectTransform>();
        MyImage = GetComponent<Image>();

        startPos = Tr.anchoredPosition;
        endPos = Tr.anchoredPosition;
        endPos.x = Mathf.Abs(endPos.x);
        
    }


    void Update()
    {

        if (Input.GetButtonDown(InputManager.Input(InputKey.select, 0)))
            StartCoroutine(FightSequence(fightDuration, actions));

        if (Input.GetButtonDown(InputManager.Input(InputKey.a, 0)))
            ActionHolder.main.PlayerOneActionAdd(Card.CardType.littlePunch);

        if (Input.GetButtonDown(InputManager.Input(InputKey.b, 0)))
            ActionHolder.main.PlayerOneActionAdd(Card.CardType.lazer);

        if (Input.GetButtonDown(InputManager.Input(InputKey.x, 0)))
            ActionHolder.main.PlayerOneActionAdd(Card.CardType.littlePunch);

        if (Input.GetButtonDown(InputManager.Input(InputKey.y, 0)))
            ActionHolder.main.PlayerOneActionAdd(Card.CardType.head);

        if (Input.GetButtonDown(InputManager.Input(InputKey.a, 1)))
            ActionHolder.main.PlayerTwoActionAdd(Card.CardType.littlePunch);

        if (Input.GetButtonDown(InputManager.Input(InputKey.b, 1)))
            ActionHolder.main.PlayerTwoActionAdd(Card.CardType.lazer);

        if (Input.GetButtonDown(InputManager.Input(InputKey.x, 1)))
            ActionHolder.main.PlayerTwoActionAdd(Card.CardType.littlePunch);

        if (Input.GetButtonDown(InputManager.Input(InputKey.y, 1)))
            ActionHolder.main.PlayerTwoActionAdd(Card.CardType.head);


    }




    private void OnValidate()
    {
        GetComponent<Image>().color = barColor;
        /*
        int a = transform.childCount;
        for (int i = 0; i < a; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        Vector2 endPos = transform.position;
        endPos.x += 1056f;

        float distance = endPos.x - startPos.x;
        
        for (int i = 0; i < actions; i++)
        {
            float b = (1f / actions);

            b *= i;
            b *= distance;
        
            endPos.x = transform.position.x + 1056f;
            endPos.x -= b;
            
            Instantiate(spacingBar, endPos, Quaternion.identity, this.transform);
        }*/
    }

    IEnumerator FightSequence(float duration, int actions)
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
