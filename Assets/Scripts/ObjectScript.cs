using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] private int objectType;

    public int GetObjectType(){
        return this.objectType;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("HOla");
    }
}   
