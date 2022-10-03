using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerScript : MonoBehaviour
{
    [Header("Number of objects")]
    [SerializeField] int nSquares;
    [SerializeField] int nCircles;
    [SerializeField] int nDiamonds;

    [Header("World Limits")]
    [SerializeField] float xUpperLimit;
    [SerializeField] float yUpperLimit;
    [SerializeField] float xLowLimit;
    [SerializeField] float yLowLimit;

    [Header("Object Prefabs")]
    [SerializeField] GameObject square;
    [SerializeField] GameObject circle;
    [SerializeField] GameObject diamond;

    private void Start() {
        InstantiateObjects(square, nSquares);
        InstantiateObjects(circle, nCircles);
        InstantiateObjects(diamond, nDiamonds);
    }

    private void InstantiateObjects(GameObject objectToInstantiate, int nTimes){
        for (int i=0; i<nSquares; i++){
            Vector2 position = new Vector2(Random.Range(xLowLimit, xUpperLimit), Random.Range(yLowLimit, yUpperLimit));
            Debug.Log(position);
            Instantiate(objectToInstantiate, position, Quaternion.identity);
        }
    }
}
