using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerScript : MonoBehaviour
{
    [Header("Number of objects")]
    [SerializeField] int nSquares;
    [SerializeField] int nCircles;
    [SerializeField] int nDiamonds;
    [SerializeField] int nAnts;

    [Header("World Limits")]
    [SerializeField] float xUpperLimit;
    [SerializeField] float yUpperLimit;
    [SerializeField] float xLowLimit;
    [SerializeField] float yLowLimit;

    [Header("Object Prefabs")]
    [SerializeField] GameObject square;
    [SerializeField] GameObject circle;
    [SerializeField] GameObject diamond;
    [SerializeField] GameObject ant;

    private void Start() {
        
        Time.timeScale = 5f;
        InstantiateObjects(square, nSquares);
        InstantiateObjects(circle, nCircles);
        InstantiateObjects(diamond, nDiamonds);
        InstantiateObjects(ant, nAnts);
    }

    private void InstantiateObjects(GameObject objectToInstantiate, int nTimes){
        for (int i=0; i<nTimes; i++){
            Vector2 position = new Vector2(Random.Range(xLowLimit, xUpperLimit), Random.Range(yLowLimit, yUpperLimit));
            Instantiate(objectToInstantiate, position, Quaternion.identity);
        }
    }

    public void DropObject(int objType, Vector3 position){
        if (objType==1){
            Instantiate(square, position, Quaternion.identity);
        }else if (objType==2){
            Instantiate(circle, position, Quaternion.identity);
        }else if (objType==3){
            Instantiate(diamond, position, Quaternion.identity);
        }
    }
}
