using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonballprefab;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float shootForce = 700f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }
    }
    public void shoot() 
    {
        GameObject cannonball = Instantiate(cannonballprefab, firepoint.position, firepoint.rotation);
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        rb.AddForce(firepoint.forward * shootForce); //hace que el adelante sea el adelante del spawn
    }
}
