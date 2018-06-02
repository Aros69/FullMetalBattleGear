using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHolder : MonoBehaviour {

    public Vector2 left;
    public Vector2 right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnValidate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Vector2 startPos = left;
        startPos.y += 1f;
        Vector2 endPos = left;
        endPos.y += 1f;

        Gizmos.DrawLine(startPos, endPos);

        startPos = left;
        startPos.x += 1f;
        endPos = left;
        endPos.x += 1f;

        Gizmos.DrawLine(startPos, endPos);


        startPos = right;
        startPos.y += 1f;
        endPos = right;
        endPos.y += 1f;

        Gizmos.DrawLine(startPos, endPos);

        startPos = left;
        startPos.x += 1f;
        endPos = left;
        endPos.x += 1f;

        Gizmos.DrawLine(startPos, endPos);
    }
}
