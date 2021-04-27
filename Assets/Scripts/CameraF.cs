using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraF : MonoBehaviour
{

    public GameObject target;
    private Vector3 positionTarget;
    [SerializeField]
    private float speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    void Follow()
    {
        positionTarget = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        Vector3 tempPosition = Vector3.Lerp(transform.position, positionTarget, speed);
        transform.position = tempPosition;        
    } 
}