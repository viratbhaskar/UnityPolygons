using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private int totalVertices = 2;
    
    private void Awake() 
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = totalVertices;
    }

    public void DrawLine(Vector2 startMousePos, Vector2 mousePos) 
    {
        lineRenderer.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
        lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
    }
}
