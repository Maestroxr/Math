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
    
    public TextMeshProUGUI textMesh;
    [HideInInspector]
    public MeshRenderer mr;
    [HideInInspector] public VectorLine vectorLine;
    List<Vector3> linePoints;

    public Texture lineTexture;
    public Material lineMaterial;
    public Color lineColor;
    public bool centered;
    // Use this for initialization
    void Start () {
        mr = GetComponent<MeshRenderer>();
        //SetLineColor(lineColor);
       
        if (centered)
            linePoints = new List<Vector3>() { new Vector3(5, 0, 0), new Vector3(-5, 0, 0) };
        else
            linePoints = new List<Vector3>() { new Vector3(0, 0, 0), new Vector3(-10, 0, 0) };
        init();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(gameObject.name + transform.position);
    }

    public Line2 UpdateLine2()
    {
        if (transform.hasChanged)
        {
            line2 = MathUtil.CreateLine2(LineObject);
            transform.hasChanged = false;
        }
        
        if (textMesh) WriteText2();
        //DrawLine2();
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
        sb.Append("<u>"+gameObject.name+"</u>\n");
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
   
    private void init()
    {
        vectorLine = new VectorLine(gameObject.name, linePoints, lineTexture, 10f);
        vectorLine.color = new Color(lineColor.r, lineColor.g, lineColor.b);
        Debug.Log("enable:" + gameObject.name + " " + lineColor.ToString() + " " + transform.eulerAngles.ToString());
        VectorManager.ObjectSetup(gameObject, vectorLine, Visibility.Dynamic, Brightness.None, false);

        // Make VectorManager lines be drawn in the scene instead of as an overlay
        VectorManager.useDraw3D = true;
        UpdateLine2();
    }

    public void OnEnable()
    {
        vectorLine.active = true;
    }
    public void OnDisable()
    {
        //VectorLine.Destroy(ref vectorLine);
        vectorLine.active = false;
    }

}
