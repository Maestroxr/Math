using UnityEngine;
using Dest.Math;
using System.Collections.Generic;
using Vectrosity;

namespace Dest.Math.Tests
{
	[ExecuteInEditMode]
	public class Test_DistPoint2Line2 : Test_Base
	{
		public Transform Point;
		public Transform Line;
        public void DrawLines(List<Vector2> linePoints)
        {
             // ...and one on the right

            // Make a VectorLine object using the above points, with a width of 2 pixels
            var line = new VectorLine("Line", linePoints, 2.0f);

            // Draw the line
            line.Draw();
        }
		private void OnDrawGizmos()
		{
			Vector2 point = Point.position;
			Line2 line = CreateLine2(Line);

			Vector2 closestPoint;
			float dist0 = Distance.Point2Line2(ref point, ref line, out closestPoint);
			float dist1 = line.DistanceTo(point);

			FiguresColor();
			DrawLine(ref line);

			ResultsColor();
			DrawPoint(closestPoint);
            /*
            List<Vector2> linePoints = new List<Vector2>();
            linePoints.Add(closestPoint);             // ...one on the left side of the screen somewhere
            linePoints.Add(point);
            // ...and one on the right

            // Make a VectorLine object using the above points, with a width of 2 pixels
            var vectorLine = new VectorLine("Line", linePoints, 2.0f);

            // Draw the line
            vectorLine.Draw();
            */
            LogInfo(dist0 + "   " + dist1);
		}
        VectorLine vectorLine;
        public void DrawVectorLine(Vector2 from, Vector2 to)
        {
            
           List<Vector2> linePoints = new List<Vector2>();
           linePoints.Add(from);             // ...one on the left side of the screen somewhere
           linePoints.Add(to);
            // ...and one on the right

            // Make a VectorLine object using the above points, with a width of 2 pixels
            if (vectorLine != null)
                ;
           vectorLine = new VectorLine("Line", linePoints, 0.2f);

           // Draw the line
           vectorLine.Draw();
           
        }

        private void Start()
        {
            DrawResults();
        }

        private void DrawResults()
        {
            Vector2 point = Point.position;
            Line2 line = CreateLine2(Line);

            Vector2 closestPoint;
            float dist0 = Distance.Point2Line2(ref point, ref line, out closestPoint);
            float dist1 = line.DistanceTo(point);

            DrawVectorLine(closestPoint, point);
           
            /*
            List<Vector2> linePoints = new List<Vector2>();
            linePoints.Add(closestPoint);             // ...one on the left side of the screen somewhere
            linePoints.Add(point);
            // ...and one on the right

            // Make a VectorLine object using the above points, with a width of 2 pixels
            var vectorLine = new VectorLine("Line", linePoints, 2.0f);

            // Draw the line
            vectorLine.Draw();
            */
            LogInfo(dist0 + "   " + dist1);
        }

    }
}
