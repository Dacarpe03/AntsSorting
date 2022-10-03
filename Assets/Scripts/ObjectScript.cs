using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] private int objectType;

    public int GetObjectType(){
        return this.objectType;
    }
}   
