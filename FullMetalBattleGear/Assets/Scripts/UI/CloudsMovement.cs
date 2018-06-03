
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsMovement : MonoBehaviour {
    public Transform Tr { private set; get; }

    private Vector3 startPos;
    private Vector3 minPos;
    private Vector3 maxPos;

    float t = 0;
    int a = 1;

    private void Start()
    {
        Tr = transform;
        startPos = Tr.position;
        minPos = Tr.position;
        maxPos = Tr.position;

        RandomizePositions();
    }

    private void RandomizePositions()
    {
        minPos.x += Random.Range(-2.5f, 2.5f);
        
        Mathf.Clamp(minPos.x, startPos.x - 5f, startPos.x + 5f);
        Mathf.Clamp(minPos.y, startPos.y - 5f, startPos.y + 5f);

        maxPos.x += Random.Range(-2.5f, 2.5f);
        
        Mathf.Clamp(maxPos.x, startPos.x - 5f, startPos.x + 5f);
        Mathf.Clamp(maxPos.y, startPos.y - 5f, startPos.y + 5f);
    }

    void Update () {

        t += 0.001f * a;

        if (Mathf.Abs(t) >= 1)
            a = (a == 1) ? -1 : 1;

        Tr.position = Vector3.Lerp(minPos, maxPos, t);
    }
}
    
