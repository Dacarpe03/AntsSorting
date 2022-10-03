using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AntScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    [SerializeField] float speed = 1;
    [SerializeField] float stepSeconds = 5f;

    private int encounteredObject = 0;
    private GameObject lastObject;

    private Queue<int> memory = new Queue<int>();
    private int memorySize = 50;

    private bool carrying = false;

    [SerializeField] private float c_pickup = 0.6f;

    private void Start(){
        Time.timeScale = 3f;
        myBody = GetComponent<Rigidbody2D>();
        Step();
    }

    private void Step(){
        ChangeDirection();
        UpdateMemory();
        Pick();
        StartCoroutine(NextStep());
    }

    private void ChangeDirection(){
        Vector2 newSpeed = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed)).normalized;
        myBody.velocity = newSpeed;
    }

    private void UpdateMemory(){
        if (memory.Count > memorySize){
            memory.Dequeue();
        }
        Debug.Log("Adding " + encounteredObject);
        memory.Enqueue(encounteredObject);
    }

    private void Pick(){
        if (!carrying && encounteredObject != 0){
            
            float pickProbability = CalculatePickProbability();
            float doPickUp = Random.Range(0f, 1f);
            if (doPickUp < pickProbability){
                Destroy(lastObject);
                Debug.Log("Destroying");
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

    private IEnumerator NextStep(){
        yield return new WaitForSeconds(stepSeconds);
        Step();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        encounteredObject = other.GetComponent<ObjectScript>().GetObjectType();
        lastObject = other.gameObject;

    }

    private void OnTriggerExit2D(Collider2D other) {
        encounteredObject = 0;
    }
}   
