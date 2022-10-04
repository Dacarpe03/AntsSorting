using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AntScript : MonoBehaviour
{
    private Rigidbody2D myBody;

    [Header("Speed")]
    [SerializeField] float speed = 1;
    [SerializeField] float stepSeconds = 5f;

    [Header("Memory")]
    private int encounteredObject = 0;
    private GameObject lastObject;
    private Queue<int> memory = new Queue<int>();
    private int memorySize = 50;


    [Header("Drop")]
    private bool carrying = false;
    private int carryingObject = 0;
    [SerializeField] private float c_drop = 0.7f;


    [Header("Pickup")]
    [SerializeField] private float c_pickup = 0.6f;


    private void Start(){
        myBody = GetComponent<Rigidbody2D>();
        Step();
    }

    private void Step(){
        CheckPosition();
        ChangeDirection();
        UpdateMemory();
        TryPick();
        TryDrop();
        StartCoroutine(NextStep());
    }

    private void CheckPosition(){
        if (Vector2.Distance(myBody.position, new Vector2(0f, 0f)) > 200){
            myBody.position = new Vector2(0f, 0f);
        }
    }

    private void ChangeDirection(){
        Vector2 newSpeed = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed)).normalized;
        myBody.velocity = newSpeed;
    }

    private void UpdateMemory(){
        if (memory.Count > memorySize){
            memory.Dequeue();
        }
        memory.Enqueue(encounteredObject);
    }

    private void TryPick(){
        if (!carrying && encounteredObject != 0){
            
            float pickProbability = CalculatePickProbability();
            float doPickUp = Random.Range(0f, 1f);
            if (doPickUp < pickProbability){
                Destroy(lastObject);
                carrying = true;
                carryingObject = encounteredObject;
                Debug.Log("Pick");
            }
        }
    }

    private void TryDrop(){
        if (carrying && encounteredObject == 0){
            float dropProbability = CalculateDropProbability();
            float doDrop = Random.Range(0f, 1f);
            if (doDrop < dropProbability){
                FindObjectOfType<ObjectSpawnerScript>().DropObject(carryingObject, myBody.position);
                carrying = false;
                carryingObject = 0;
                Debug.Log("Droping");
            }
        }

    }

    private float CalculatePickProbability(){
        var instances = from number in memory
                        where number.Equals(encounteredObject)
                        select number;
        int nObjects = instances.Count();
        int proportion = nObjects/memorySize;
        float fraction = (c_pickup)/(c_pickup + proportion);
        float probability = Mathf.Pow(fraction, 2);
        return probability;
    }

    private float CalculateDropProbability(){
        var instances = from number in memory
                        where number.Equals(encounteredObject)
                        select number;
        int nObjects = instances.Count();
        int proportion = nObjects/memorySize;
        float fraction = (proportion)/(c_drop + proportion);
        float probability = Mathf.Pow(fraction, 2);
        return probability;
    }

    private IEnumerator NextStep(){
        yield return new WaitForSeconds(stepSeconds);
        Step();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        encounteredObject = other.GetComponent<ObjectScript>().GetObjectType();
        if (!carrying){
            lastObject = other.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        encounteredObject = 0;
    }
}   
