using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using System;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public Material material;
    public Material line_material;
    public Transform currentpoint;
    public Transform point1;

    private InputDevice targetDevice;
    public GameObject red_sphere;
    [Tooltip("Event when the button starts being pressed")]

    public UnityEvent OnPress;
    [Tooltip("Event when the button is released")]

    public UnityEvent OnRelease;
    // to check whether it's being pressed
    public bool IsPressed { get; private set; }
    public bool IsAPressed { get; private set; }

    bool TriggerButtonValue;
    bool SecondaryButtonValue;
    bool PrimaryButtonValue;
    public List<Transform> points;

    public List<Vector3> verts;

    public TextMeshProUGUI lognews;

    public int index = 0;
    public int u = 0;

    public Mesh mesh;
    public Vector3[] vertices;
    public int[] triangles;

    public float width;
    public MeshRenderer meshRenderer;
    public GameObject quad;

    public MeshCollider meshCollider;

    public GameObject lineRenderer1;
    public GameObject lineRenderer2;
    public GameObject lineRenderer3;
    public GameObject lineRenderer4;  
    public GameObject lineRenderer5;
    public GameObject lineRenderer6;
    public GameObject lineRenderer7;
    public GameObject lineRenderer8;  


    public LineRenderer lineRenderer_1;
    public LineRenderer lineRenderer_2;
    public LineRenderer lineRenderer_3;
    public LineRenderer lineRenderer_4;
    public LineRenderer lineRenderer_5;
    public LineRenderer lineRenderer_6;
    public LineRenderer lineRenderer_7;
    public LineRenderer lineRenderer_8;
    
    public BoxCollider boxCollider1;

    public Transform Parent;

    public String type_of_object = "";


    public int exists = 0;

    void Start()
    {
        //showVertices = true;

        // Die Buttons werden aufgelistet um darauf zugreifen zu können
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
        lineRenderer_1 = lineRenderer1.GetComponent<LineRenderer>();

    	boxCollider1 = lineRenderer1.GetComponent<BoxCollider>();
		boxCollider1.enabled = true;


        lineRenderer_2 = lineRenderer2.GetComponent<LineRenderer>();
        lineRenderer_3 = lineRenderer3.GetComponent<LineRenderer>();
        lineRenderer_4 = lineRenderer4.GetComponent<LineRenderer>();
        lineRenderer_5 = lineRenderer5.GetComponent<LineRenderer>();
        lineRenderer_6 = lineRenderer6.GetComponent<LineRenderer>();
        lineRenderer_7 = lineRenderer7.GetComponent<LineRenderer>();
        lineRenderer_8 = lineRenderer8.GetComponent<LineRenderer>();


        width= 0.001f;
    }

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;

        meshCollider = quad.GetComponent<MeshCollider>();
        meshCollider.enabled = true;

        if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton,
                out TriggerButtonValue) && TriggerButtonValue)
            {
                if (!IsPressed)
                {
                    IsPressed = true;
                    OnPress.Invoke();

                 }

            }
            else if (IsPressed)
            {   
                IsPressed = false;
                OnRelease.Invoke();

                if (exists==1){
                    currentpoint = Instantiate(point1);
                    currentpoint.GetComponent<MeshRenderer>().enabled = true;
                    //point1.GetComponent<MeshRenderer>().enabled = false;
                    currentpoint.transform.localScale  = new Vector3(0.01f, 0.01f, 0.01f);
                    currentpoint.transform.parent = Parent;

                    points.Add(currentpoint);
                    verts.Add(currentpoint.position);
                }
            }



        vertices = new Vector3[verts.Count];

        for(int i = 0; i < verts.Count; i++){
            vertices[i]  = verts[i];
        };
        
        triangles = new int[9*verts.Count];

        var z = index - 4;
        if(type_of_object == "surface"){

            if (index >= 4){
                for(int i = 0; i <= z*6; i = i + 6)
                {
                    triangles[i] = 0;
                    triangles[i+3] = 0;

                    triangles[i+1] = 2;
                    triangles[i+2] = 1;
                    triangles[i+4] = 1;
                    triangles[i+5] = 2;

                    if(i > 0){
                        triangles[i+1] = i/6 + 2;
                        triangles[i+2] = i/6 + 1;  
                        triangles[i+4] = i/6 + 1;
                        triangles[i+5] = i/6 + 2;
                    }
                }
            }
            if (verts.Count > 2){
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            meshCollider.sharedMesh          =  mesh;
            }

        }
        if(type_of_object == "pyramid"){
            if (verts.Count > 3){
                for(int i = 0; i <= z*6; i = i + 6)
                {
                    triangles[i] = 0;
                    triangles[i+3] = 0;

                    triangles[i+1] = 2;
                    triangles[i+2] = 1;
                    triangles[i+4] = 1;
                    triangles[i+5] = 2;

                    if(i > 0){
                        triangles[i+1] = i/6 + 2;
                        triangles[i+2] = i/6 + 1;  
                        triangles[i+4] = i/6 + 1;
                        triangles[i+5] = i/6 + 2;
                    }

                    if(i > 10){
                        triangles[i]    = 4;
                        triangles[i+1]  = 3;
                        triangles[i+2]  = 2;  
                        triangles[i+3]  = 4;
                        triangles[i+4]  = 2;
                        triangles[i+5]  = 3;

                        triangles[i+6]  = 4;
                        triangles[i+7]  = 1;
                        triangles[i+8]  = 2;  
                        triangles[i+9]  = 4;
                        triangles[i+10] = 2;
                        triangles[i+11] = 1;

                        triangles[i+12]  = 4;
                        triangles[i+13]  = 0;
                        triangles[i+14]  = 1;  
                        triangles[i+15]  = 4;
                        triangles[i+16]  = 1;
                        triangles[i+17]  = 0;

                        triangles[i+18]  = 4;
                        triangles[i+19]  = 3;
                        triangles[i+20]  = 1;  
                        triangles[i+21]  = 4;
                        triangles[i+22] = 1;
                        triangles[i+23] = 3;

                    }

                }
            }


            if (verts.Count > 4){
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            meshCollider.sharedMesh          =  mesh;

            lineRenderer_1.SetPositions(new[] {mesh.vertices[0], mesh.vertices[1]});
            lineRenderer_2.SetPositions(new[] {mesh.vertices[1], mesh.vertices[2]});
            lineRenderer_3.SetPositions(new[] {mesh.vertices[2], mesh.vertices[3]});
            lineRenderer_4.SetPositions(new[] {mesh.vertices[3], mesh.vertices[0]});

            lineRenderer_5.SetPositions(new[] {mesh.vertices[0], mesh.vertices[4]});
            lineRenderer_6.SetPositions(new[] {mesh.vertices[1], mesh.vertices[4]});
            lineRenderer_7.SetPositions(new[] {mesh.vertices[2], mesh.vertices[4]});
            lineRenderer_8.SetPositions(new[] {mesh.vertices[3], mesh.vertices[4]});


			lineRenderer_1.startWidth  = width;
			lineRenderer_1.endWidth    = width;
			lineRenderer_2.startWidth  = width;
			lineRenderer_2.endWidth    = width;
            lineRenderer_3.startWidth  = width;
			lineRenderer_3.endWidth    = width;
			lineRenderer_4.startWidth  = width;
			lineRenderer_4.endWidth    = width;

			lineRenderer_5.startWidth  = width;
			lineRenderer_5.endWidth    = width;
			lineRenderer_6.startWidth  = width;
			lineRenderer_6.endWidth    = width;
            lineRenderer_7.startWidth  = width;
			lineRenderer_7.endWidth    = width;
			lineRenderer_8.startWidth  = width;
			lineRenderer_8.endWidth    = width;

            boxCollider1.size          =  new Vector3(3f, 3f, 3f);

            //lineRenderer2.startWidth  = width;
			//lineRenderer2.endWidth    = width;



            }
        } 



    }
}
