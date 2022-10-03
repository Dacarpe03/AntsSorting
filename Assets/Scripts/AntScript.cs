using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    [SerializeField] float speed = 1;
    [SerializeField] float stepSeconds = 5f;

    private int encounteredObject = 0;

    private List<int> memory = new List<int>();

    private void Start(){
        myBody = GetComponent<Rigidbody2D>();
        Step();
    }

    private void Step(){
        ChangeDirection();
        
        StartCoroutine(NextStep());
    }

    private void ChangeDirection(){
        Vector2 newSpeed = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed)).normalized;
        myBody.velocity = newSpeed;
    }

    private IEnumerator NextStep(){
        yield return new WaitForSeconds(stepSeconds);
        Step();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        encounteredObject = other.GetComponent<ObjectScript>().GetObjectType();
    }

    private void OnTriggerExit2D(Collider2D other) {
        encounteredObject = 0;
    }
}   
