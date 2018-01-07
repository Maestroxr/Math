using Dest.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BoxedPlane : MonoBehaviour {
    public Plane3 plane;
    // Use this for initialization
    public Text planeText;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    Vector3 origNormal;
    public Plane3 Setup(Vector3 normal)
    {
        plane = new Plane3(normal, Vector3.zero);
        origNormal = normal;
        UpdatePlane(plane);
        return plane;
    }



    public static void UpdateTransformPlane(Transform planeT, Plane3 update)
    {
        planeT.position = update.CalcOrigin();
        Vector3 u, v, n;
        update.CreateOrthonormalBasis(out u, out v, out n);
        planeT.transform.rotation = Quaternion.LookRotation(u, n);
        //planeT.up = update.Normal;
       
    }

    public void UpdatePlane(Plane3 update)
    {
        UpdateTransformPlane(transform, update);
        plane = update;
        planeText.text = PlaneEquation();
    }

    

    public string PlaneEquation()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(gameObject.name + "\n");
        Vector3 v = plane.Normal * origNormal.magnitude;
        string a = MathUtil.FormatAndRoundNumber( v.x),
            b = MathUtil.FormatAndRoundNumber(v.y),
            c = MathUtil.FormatAndRoundNumber(v.z), 
            d = MathUtil.FormatAndRoundNumber(plane.Constant * origNormal.magnitude);
        sb.Append(a + "X ");
        if (plane.Normal.y >= 0f) sb.Append("+" + b + "Y "); else sb.Append(b + "Y ");
        if (plane.Normal.z >= 0f) sb.Append("+" + c + "Z "); else sb.Append(c + "Z ");
        if (plane.Constant >= 0f) sb.Append("+" + d); else sb.Append(d);
        sb.Append(" = 0");
        return  sb.ToString();
    }
}
