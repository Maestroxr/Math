using UnityEngine;
using Dest.Math;
using Vectrosity;
using System.Collections.Generic;

namespace Dest.Math.Tests
{
	
	public class Test_IntrLine2Line2 : Test_Base
	{
        public LineUI lineUI0;
        public LineUI lineUI1;
        public Transform IntersectionPoint;
        
		

        
        public Line2 line0, line1;
        public bool test, find;
        Line2Line2Intr info;
        IntersectionTypes intersectionType;

        public void Start()
        {
            
        }


        private void UpdateLines()
        {
            
            line0 = lineUI0.UpdateLine2();
            line1 = lineUI1.UpdateLine2();
            test = Intersection.TestLine2Line2(ref line0, ref line1, out intersectionType);
            find = Intersection.FindLine2Line2(ref line0, ref line1, out info);
        }


        public void Update()
        {
            UpdateLines();
            DrawResult();
        }

        void DrawResult()
        {
            IntersectionPoint.gameObject.SetActive(find);
            if (find)
            {
                if (info.IntersectionType == IntersectionTypes.Point)
                {
                    IntersectionPoint.transform.position = info.Point;
                }
            }
        }

        public void RandomizeLines()
        {
            Vector2 n0 = Randomize(), n1 = Randomize();
            LogInfo("Normal n0: " + n0.ToString());
            LogInfo("Normal n1: " + n1.ToString());
            /*line0 = CreateLine2(n0,Vector2.zero);
            mathPlane1 = boxedPlane1.Setup(n1);
            BoxedPlane.UpdateTransformPlane(Plane0, mathPlane0);
            BoxedPlane.UpdateTransformPlane(Plane1, mathPlane1);
            UpdatePlanes();*/
        }

        public Vector2 Randomize()
        {
            Vector2 result = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            while (result == Vector2.zero) result = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            return result;
        }

        private void OnDrawGizmos()
		{
			FiguresColor();
			DrawLine(ref line0);
			DrawLine(ref line1);

			if (find)
			{
				ResultsColor();
				if (info.IntersectionType == IntersectionTypes.Point)
				{
					DrawPoint(info.Point);
				}
			}

			LogInfo(intersectionType);
			if (test != find) LogError("test != find");
			if (intersectionType != info.IntersectionType) LogError("intersectionType != info.IntersectionType");
		}
  
    }
}
