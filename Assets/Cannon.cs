using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonballprefab;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float shootForce = 700f;
    [SerializeField] private Slider angleSliderx;
    [SerializeField] private Slider angleSliderz;
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

    public void CannonAnglex()
    {
        float anglex = angleSliderx.value;
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(anglex, currentRotation.y, currentRotation.z);
    }

    public void CannonAngleZ()
    {
        float anglez = angleSliderz.value;
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, anglez);
    }

}
