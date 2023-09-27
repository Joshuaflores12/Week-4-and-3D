using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eBodyColor 
{
    Red, 
    Blue,
    Green
}

public class rotationsample : MonoBehaviour
{
    float x, y, z;
    public Vector3 currentEulerAngles;
    public Quaternion currentRotation;
    public float rotspeed;
    public Transform targetA;
    public Transform targetB;
    float TimeCount = 0.0f;
    public eBodyColor bodycolor;
    public Color Red,Green,Blue,CurrentColor;
    public float radius;


    // Start is called before the first frame update
    void Start()
    {
        Red = Color.red;
        Green = Color.green;
        Blue = Color.blue;
            
    }

    // Update is called once per frame
    void Update()
    {
        RotateInput();
        //QuaternionRotateTowards();
        //SlerpExample();
        LookRotation();
        float dist = Vector3.Distance(transform.position, targetA.position);
        if (dist <= radius) 
        {
            Debug.Log("Enemy Has Enterned ");
        }
         
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        GUI.Label(new Rect(10,0,0,0), "Rotating on X:" + x + " Y: " + y + " Z: " + z ,style);
        
        style.fontSize = 50;
        GUI.Label(new Rect(10,50,0,0), "Current Euler Angles: " + currentEulerAngles ,style);
        
        
        GUI.Label(new Rect(10,100,0,0), "World Euler Angles: " + transform.eulerAngles ,style);
    }

    void RotateInput() 
    {
        if (Input.GetKeyDown(KeyCode.X)) { x = 1 - x; }
        if (Input.GetKeyDown(KeyCode.Y)) { y = 1 - y; }
        if (Input.GetKeyDown(KeyCode.Z)) { z = 1 - z; }

        currentEulerAngles +=  new Vector3(x, y, z) * Time.deltaTime * rotspeed; 
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
    }

    void QuaternionRotateTowards() 
    {
        float step = rotspeed * Time.time;
        transform.rotation = Quaternion.RotateTowards(transform.rotation,targetA.rotation, step);  
    }

    void SlerpExample() 
    {
        transform.rotation = Quaternion.Slerp(targetA.rotation, targetB.rotation,TimeCount);
        TimeCount = TimeCount + Time.deltaTime;
    }

    void LookRotation() 
    {
        Vector3 relativePos = targetA.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos,Vector3.up);; 
        transform.rotation = rotation;
    }

    private void OnMouseDown()
    {
        switch (bodycolor)
        {
            case eBodyColor.Red:
                CurrentColor = Red;
                break;
            case eBodyColor.Blue:
                CurrentColor = Blue;
                break;
            case eBodyColor.Green:
                CurrentColor = Green;
                break;
            default:
                break;
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
   