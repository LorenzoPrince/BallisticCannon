using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonballprefab;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float shootForce = 700f;
    [SerializeField] private Slider angleSliderx;
    [SerializeField] private Slider angleSliderz;


    [SerializeField] private Slider forceSlider;
    [SerializeField] private TMP_Text forceText;

    [SerializeField] private List<GameObject> projectilePrefabs; 
    [SerializeField] private TMP_Dropdown projectileDropdown;

    void Start()
    {
  

        if (forceSlider != null)
        {
            forceSlider.value = shootForce;
            forceSlider.onValueChanged.AddListener(UpdateShootForce);
            UpdateShootForce(forceSlider.value); // Mostrar texto inicial
        }
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
   
        int index = projectileDropdown != null ? projectileDropdown.value : 0;      // Elegir proyectil según el dropdown

        if (index < 0 || index >= projectilePrefabs.Count)
        {
            Debug.LogWarning("Índice de proyectil fuera de rango");
            return;
        }

        GameObject selectedProjectile = projectilePrefabs[index];

        GameObject cannonball = Instantiate(selectedProjectile, firepoint.position, firepoint.rotation);
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
    public void UpdateShootForce(float newForce)
    {
        shootForce = newForce;

        if (forceText != null)
        {
            forceText.text = "Fuerza: " + shootForce.ToString("F0");
        }
    }
}
