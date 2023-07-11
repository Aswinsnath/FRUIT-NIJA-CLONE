using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private bool Slising;

    private Camera Maincamera;

    private Collider bladeColider;

    public float sliceVelocity = 0.01f;

    public float SliceForce = 5f;


    //partivle sysytem of blade 

    private TrailRenderer bladeTraileRenter;

    //Encapsulation
    public Vector3 direction { get; private set; }

    private void Awake()
    {
        Maincamera =Camera.main;

        bladeColider =GetComponent<Collider>();

        bladeTraileRenter =GetComponentInChildren<TrailRenderer>();
    }
    public void OnEnable()
    {
        StopSlising();
    }
    public void OnDisable()
    {
        StopSlising();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlising();

        }else if (Input.GetMouseButtonUp(0))
        {
            StopSlising();
        }else if (Slising)
        {
            continueSlising();
        }
    }


    private void StartSlising()
    {
        Vector3 newposition = Maincamera.ScreenToViewportPoint(Input.mousePosition);
        newposition.z = 0f; 
        transform.position = newposition;

        Slising =true;
        bladeColider.enabled=true;

        bladeTraileRenter.enabled=true;

        bladeTraileRenter.Clear();
    }
    private void StopSlising()
    {
        Slising = false;
        bladeColider.enabled = false;

        bladeTraileRenter.enabled = true;
    }

    private void continueSlising()
    {
        Vector3 newposition = Maincamera.ScreenToWorldPoint(Input.mousePosition);
        newposition.z = 0f;

        direction = newposition - transform.position;

        float velocity = direction.magnitude /Time.deltaTime;

        bladeColider.enabled = velocity > sliceVelocity;

        transform.position = newposition;
    }
}
