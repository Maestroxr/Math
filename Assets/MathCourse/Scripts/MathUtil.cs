using Dest.Math;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MathUtil  {
    public static int DigitsRound = 100;
    static public float RoundNumber(float number)
    {
        return Mathf.Round(number * DigitsRound) / DigitsRound;
    }

    public static Vector2 RoundVector(Vector2 vec)
    {
        return new Vector2(RoundNumber(vec.x), RoundNumber(vec.y));
    }

    public static Vector3 RoundVector(Vector3 vec)
    {
        return new Vector3(RoundNumber(vec.x), RoundNumber(vec.y), RoundNumber(vec.z));
    }

    static public string FormatNumber(float number)
    {
        return System.String.Format("{0:0.00}", number);
    }

    static public string FormatAndRoundNumber(float number)
    {
        return FormatNumber(RoundNumber(number));
    }



    static public string[] FormatAndRoundVector2(Vector3 vec2)
    {
        return new string[2] { FormatAndRoundNumber(vec2.x),
            FormatAndRoundNumber(vec2.y) };
    }

    static public string[] FormatAndRoundVector3(Vector3 vec3)
    {
        return new string[] { FormatAndRoundNumber(vec3.x),
            FormatAndRoundNumber(vec3.y), FormatAndRoundNumber(vec3.z) };
    }

    static public void RoundVector2(Vector2 vec2, StringBuilder sb)
    {
        string[] xy = FormatAndRoundVector2(vec2);
        sb.Append("(");
        sb.Append(xy[0]);
        sb.Append(",");
        sb.Append(xy[1]);
        sb.Append(")");
        
    }

    static public void RoundVector3(Vector2 vec3, StringBuilder sb)
    {
        string[] xy = FormatAndRoundVector3(vec3);
        sb.Append("(");
        sb.Append(xy[0]);
        sb.Append(",");
        sb.Append(xy[1]);
        sb.Append(",");
        sb.Append(xy[2]);
        sb.Append(")");
    }


    static public AAB2 CreateAAB2(Transform point0, Transform point1)
    {
        // Creates aab from two unsorted points, if you know min and max use constructor
        return AAB2.CreateFromTwoPoints(point0.position, point1.position);
    }

    static public AAB3 CreateAAB3(Transform point0, Transform point1)
    {
        // Creates aab from two unsorted points, if you know min and max use constructor
        return AAB3.CreateFromTwoPoints(point0.position, point1.position);
    }

    static public Box2 CreateBox2(Transform box)
    {
        return new Box2(box.position, box.right, box.up, box.localScale);
    }

    static public Box3 CreateBox3(Transform box)
    {
        return new Box3(box.position, box.right, box.up, box.forward, box.localScale);
    }

    static public Rectangle3 CreateRectangle3(Transform rectangle)
    {
        return new Rectangle3(rectangle.position, rectangle.right, rectangle.forward, rectangle.localScale);
    }

    static public Circle2 CreateCircle2(Transform circle)
    {
        return new Circle2(circle.position, circle.localScale.x);
    }

    static public Circle3 CreateCircle3(Transform circle)
    {
        return new Circle3(circle.position, circle.right, circle.forward, circle.localScale.x);
    }

    static public Sphere3 CreateSphere3(Transform sphere)
    {
        return new Sphere3(sphere.position, sphere.localScale.x);
    }

    static public Line2 CreateLine2(Transform line)
    {
        return new Line2(line.position, line.right);
    }

    static public Line3 CreateLine3(Transform line)
    {
        return new Line3(line.position, line.right);
    }

    static public Ray2 CreateRay2(Transform ray)
    {
        return new Ray2(ray.position, ray.right);
    }

    static public Ray3 CreateRay3(Transform ray)
    {
        return new Ray3(ray.position, ray.right);
    }

    static public Segment2 CreateSegment2(Transform p0, Transform p1)
    {
        return new Segment2(p0.position, p1.position);
    }

    static public Segment3 CreateSegment3(Transform p0, Transform p1)
    {
        return new Segment3(p0.position, p1.position);
    }

    static public Triangle2 CreateTriangle2(Transform v0, Transform v1, Transform v2)
    {
        return new Triangle2(v0.position, v1.position, v2.position);
    }

    static public Triangle3 CreateTriangle3(Transform v0, Transform v1, Transform v2)
    {
        return new Triangle3(v0.position, v1.position, v2.position);
    }

    static public Plane3 CreatePlane3(Transform plane)
    {
        return new Plane3(plane.up, plane.position);
    }

    static public Polygon2 CreatePolygon2(Transform[] polygon)
    {
        Polygon2 result = new Polygon2(polygon.Length);
        for (int i = 0; i < polygon.Length; ++i)
        {
            result[i] = polygon[i].position.ToVector2XY();
        }
        result.UpdateEdges();
        return result;
    }

    static public Capsule3 CreateCapsule3(Transform p0, Transform p1, float radius)
    {
        return new Capsule3(new Segment3(p0.position, p1.position), radius);
    }


    static public Vector2[] GenerateRandomSet2D(Transform transform, float setRadius, int countMin, int countMax, float coeffX = 1f, float coeffY = 1f)
    {
        Transform thisTransform = transform;
        while (thisTransform.childCount != 0)
        {
            Transform child = thisTransform.GetChild(0);
            GameObject.DestroyImmediate(child.gameObject);
        }

        int genCount = Random.Range(countMin, countMax);
        Vector2[] points = new Vector2[genCount];
        for (int i = 0; i < genCount; ++i)
        {
            GameObject child = new GameObject("Point" + i.ToString());
            Transform childTransform = child.transform;
            childTransform.parent = thisTransform;
            Vector2 point = Random.insideUnitCircle * setRadius;
            point.x *= coeffX;
            point.y *= coeffY;
            childTransform.position = point;
            points[i] = point;
        }
        return points;
    }

    static public Vector3[] GenerateRandomSet3D(Transform transform, float setRadius, int countMin, int countMax, float coeffX = 1f, float coeffY = 1f, float coeffZ = 1f)
    {
        Transform thisTransform = transform;
        while (thisTransform.childCount != 0)
        {
            Transform child = thisTransform.GetChild(0);
            GameObject.DestroyImmediate(child.gameObject);
        }

        int genCount = Random.Range(countMin, countMax);
        Vector3[] points = new Vector3[genCount];
        for (int i = 0; i < genCount; ++i)
        {
            GameObject child = new GameObject("Point" + i.ToString());
            Transform childTransform = child.transform;
            childTransform.parent = thisTransform;
            Vector3 point = Random.insideUnitSphere * setRadius;
            childTransform.position = point;
            point.x *= coeffX;
            point.y *= coeffY;
            point.z *= coeffZ;
            points[i] = point;
        }
        return points;
    }

    static public Vector2[] GenerateMemoryRandomSet2D(float setRadius, int countMin, int countMax, float coeffX = 1f, float coeffY = 1f)
    {
        int genCount = Random.Range(countMin, countMax);
        Vector2[] points = new Vector2[genCount];
        for (int i = 0; i < genCount; ++i)
        {
            Vector2 point = Random.insideUnitSphere * setRadius;
            point.x *= coeffX;
            point.y *= coeffY;
            points[i] = point;
        }
        return points;
    }

    static public Vector3[] GenerateMemoryRandomSet3D(float setRadius, int countMin, int countMax, float coeffX = 1f, float coeffY = 1f, float coeffZ = 1f)
    {
        int genCount = Random.Range(countMin, countMax);
        Vector3[] points = new Vector3[genCount];
        for (int i = 0; i < genCount; ++i)
        {
            Vector3 point = Random.insideUnitSphere * setRadius;
            point.x *= coeffX;
            point.y *= coeffY;
            point.z *= coeffZ;
            points[i] = point;
        }
        return points;
    }

    static public Vector2[] CreatePoints2(Transform[] points)
    {
        Vector2[] result = new Vector2[points.Length];
        for (int i = 0; i < points.Length; ++i)
        {
            result[i] = points[i].transform.position;
        }
        return result;
    }

    static public Vector3[] CreatePoints3(Transform[] points)
    {
        Vector3[] result = new Vector3[points.Length];
        for (int i = 0; i < points.Length; ++i)
        {
            result[i] = points[i].transform.position;
        }
        return result;
    }

    static public Vector3[] CreateFromChildren3(Transform parent)
    {
        Vector3[] result = new Vector3[parent.childCount];
        for (int i = 0; i < result.Length; ++i)
        {
            result[i] = parent.GetChild(i).position;
        }
        return result;
    }

}
