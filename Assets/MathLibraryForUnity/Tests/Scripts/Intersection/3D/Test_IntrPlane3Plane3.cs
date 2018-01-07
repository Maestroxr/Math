using UnityEngine;
using Dest.Math;
using UnityEngine.UI;
using Vectrosity;
using System.Collections.Generic;

namespace Dest.Math.Tests
{
	
	public class Test_IntrPlane3Plane3 : Test_Base
	{
		public Transform Plane0;
		public Transform Plane1;

        public Plane3 mathPlane0;
        public Plane3 mathPlane1;
        public float magnitude0, magnitude1;

        public BoxedPlane boxedPlane0;
        public BoxedPlane boxedPlane1;

        public VectorLine result;
        public List<Vector3> resultPoints = new List<Vector3>();
        bool test;
        Plane3Plane3Intr info;
        bool find;
        
        private void registerDropPlane(GameObject go)
        {
            DragAndDrop dnd = go.GetComponent<DragAndDrop>();
            dnd.update += UpdatePlanes;
        }

        private void deregisterDropPlane(GameObject go)
        {
            DragAndDrop dnd = go.GetComponent<DragAndDrop>();
            dnd.update -= UpdatePlanes;
        }

        public void Start()
        {
            RandomizePlanes();
            
        }

        public void RandomizePlanes()
        {
            Vector3 n0 = Randomize(), n1 = Randomize();
            LogInfo("Normal n0: " + n0.ToString());
            LogInfo("Normal n1: " + n1.ToString());
            mathPlane0 = boxedPlane0.Setup(n0);
            mathPlane1 = boxedPlane1.Setup(n1);
            BoxedPlane.UpdateTransformPlane(Plane0, mathPlane0);
            BoxedPlane.UpdateTransformPlane(Plane1, mathPlane1);
            UpdatePlanes();
        }


        public Vector3 Randomize()
        {
            Vector3 result =  new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            while (result == Vector3.zero) result = 
                    new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            return result;
        }


        public void Update()
        {
            UpdatePlanes();

        }


        public void OnEnable()
        {
            result = new VectorLine("Result", resultPoints, 2f);
            registerDropPlane(Plane0.gameObject);
            registerDropPlane(Plane1.gameObject);
        }


        public void OnDisable()
        {
            VectorLine.Destroy(ref result);
            deregisterDropPlane(Plane0.gameObject);
            deregisterDropPlane(Plane1.gameObject);
        }


        public void UpdatePlanes()
        {
            mathPlane0 = CreatePlane3(Plane0);
            mathPlane1 = CreatePlane3(Plane1);
            boxedPlane0.UpdatePlane(mathPlane0);
            boxedPlane1.UpdatePlane(mathPlane1);
            test = Intersection.TestPlane3Plane3(ref mathPlane0, ref mathPlane1);
            find = Intersection.FindPlane3Plane3(ref mathPlane0, ref mathPlane1, out info);
            DrawResult();
        }
        
        private void DrawResult()
        {
            if (find)
            {
                if (info.IntersectionType == IntersectionTypes.Line)
                {                 
                    Vector3 v1 = info.Line.Center - info.Line.Direction * 100, v2 = info.Line.Center + info.Line.Direction * 100;               
                    result.points3.Clear();
                    result.points3.Add(v1);
                    result.points3.Add(v2);
                    result.Draw3D();
                }
            }
        }


        private void OnDrawGizmos()
		{
			FiguresColor();
            SetColor(Color.red);
			//DrawPlane(ref mathPlane0, Plane0);
            SetColor(Color.blue);
            //DrawPlane(ref mathPlane1, Plane1);
			if (find)
			{
				if (info.IntersectionType == IntersectionTypes.Line)
				{
					ResultsColor();
					DrawLine(ref info.Line);
				}
			}
			//LogInfo("test: " + test + " find: " + info.IntersectionType);
		}
	}
}
