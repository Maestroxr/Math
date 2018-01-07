using Dest.Math;
using Dest.Math.Tests;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;


public class LineUI : MonoBehaviour {
    public Transform LineObject;
    public Line2 line2;
    public Line3 line3;
    public MeshRenderer mr;
    public TextMeshProUGUI textMesh;
    public VectorLine vectorLine;
    List<Vector3> linePoints = new List<Vector3>();
    
    // Use this for initialization
    void Start () {
        mr = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Line2 UpdateLine2()
    {
        line2 = MathUtil.CreateLine2(LineObject);
        if (textMesh) WriteText2();
        DrawLine2();
        return line2;
    }

    public void SetLineColor(Color c)
    {
        vectorLine.color = c;
        mr.material.color = c;
    }

    public Line3 UpdateLine3()
    {
        line3 = MathUtil.CreateLine3(LineObject);
        DrawLine3();
        return line3;
    }

    protected void WriteText2()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(gameObject.name+"\n");
        MathUtil.RoundVector2(line2.Center, sb);
        sb.Append(":רוקמ");
        sb.Append("\n");
        MathUtil.RoundVector2(line2.Direction, sb);
        sb.Append(":ןוויכ");
        //text.text = sb.ToString();
        textMesh.text = sb.ToString();
    }

    protected void WriteText3()
    {
        //text.text = line3.ToString();
    }

    protected void DrawLine2()
    {
        vectorLine.points3.Clear();
        vectorLine.points3.Add(line2.Eval(-15));
        vectorLine.points3.Add(line2.Eval(15));
        vectorLine.Draw3D();
    }

    protected void DrawLine3()
    {
        vectorLine.points3.Clear();
        vectorLine.points3.Add(line3.Eval(-15));
        vectorLine.points3.Add(line3.Eval(15));
        vectorLine.Draw3D();
    }

    public void OnEnable()
    {
        vectorLine = new VectorLine("Result0", linePoints, 2f);
    }
    public void OnDisable()
    {
        VectorLine.Destroy(ref vectorLine);
    }

}
