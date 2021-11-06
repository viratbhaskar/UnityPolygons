using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Vector2 mousePos;
    public Vector2 startMousePos; 
    public Vector2 polygonParentVertexPos;

    public Button completeShapeBtn;
    public Button copyShapeBtn;

    public GameObject lineRenderer;
    private GameObject newShape;
    private GameObject copiedShape;

    public bool isShapeStarted = true;
    public bool isShapeCompleted = false;

    private List<Vector2> polygonEdges = new List<Vector2>(); // For setting the color of the shape formed

    private void Awake() 
    {
        completeShapeBtn.onClick.AddListener(CompleteShape);
        copyShapeBtn.onClick.AddListener(CopyShape);
    }

    void Update()
    {
        if (isShapeStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                polygonParentVertexPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startMousePos = polygonParentVertexPos;
                polygonEdges.Add(startMousePos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                InstantiateLine(startMousePos, mousePos);
                polygonEdges.Add(mousePos);

                isShapeStarted = false;
            }
        }
        else if (!isShapeCompleted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePos = mousePos;
                polygonEdges.Add(startMousePos);

            }
            if (Input.GetMouseButtonUp(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                InstantiateLine(startMousePos, mousePos);
                polygonEdges.Add(mousePos);

            }
        }

        if (copiedShape)
        {
            if (Input.GetMouseButtonDown(0))
            {   
                copiedShape.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // To move copied shape with mouse
            }
        }
    }

    void InstantiateLine(Vector2 oldPos, Vector2 newPos)
    {
        GameObject createdLine = Instantiate(lineRenderer);
        createdLine.GetComponent<LineController>().DrawLine(oldPos, newPos);
    }

    void CompleteShape()
    {
        startMousePos = polygonEdges[polygonEdges.Count - 1];
        mousePos = polygonParentVertexPos;
        InstantiateLine(startMousePos, mousePos);

        copyShapeBtn.gameObject.SetActive(true);
        completeShapeBtn.gameObject.SetActive(false);
        isShapeCompleted = true;

        newShape = new GameObject("Created Shape");
        LineRenderer[] linesInGame = FindObjectsOfType<LineRenderer>();

        for (int i = 0; i < linesInGame.Length; i++)
        {
            if (!linesInGame[i].transform.parent)
            {
                linesInGame[i].transform.SetParent(newShape.transform);
            }
        }

        // Build Mesh from the connected lines to form a shape
    }

    void CopyShape()
    {
        if (isShapeCompleted)
        {
            copiedShape = Instantiate(newShape);
        }
    }
}
