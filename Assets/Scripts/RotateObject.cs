using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Transform trans;

    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        trans = this.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        trans.Rotate(speed, speed, speed);
    }
}
