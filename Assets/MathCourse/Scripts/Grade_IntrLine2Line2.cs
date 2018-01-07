using UnityEngine;
using Dest.Math;
using Vectrosity;
using System.Collections.Generic;
using TMPro;
using System.Collections;
//using static TMPro.TMP_InputField;

namespace Dest.Math.Tests
{

    public class Grade_IntrLine2Line2 : Test_Base
    {
        public LineUI lineUI0, lineUI1, answerLine;
        public Transform IntersectionPoint, AnswerPoint;




        public Line2 line0, line1;
        public bool test, find;
        Line2Line2Intr info;
        IntersectionTypes intersectionType;


        public TMP_InputField inputX, inputY, inputX1, inputY1;
        public TextMeshProUGUI feedback;
        public TMP_Dropdown dropDownMenu;
        public GameObject pointInput, directionInput;

        private enum DropdownChoices { POINT, LINE, EMPTY };
        private HashSet<string> inputDefaults = new HashSet<string> { "X", "Y", "Z" };
        public void Start()
        {
            DropdownUpdate();
            AnswerPoint.gameObject.SetActive(false);
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
            IntersectionPoint.gameObject.SetActive(false);
            if (find)
            {
                if (info.IntersectionType == IntersectionTypes.Point)
                {
                    IntersectionPoint.gameObject.SetActive(true);
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
                    string s = "X:" + MathUtil.FormatNumber(info.Point.x) +
                        " Y:" + MathUtil.FormatNumber(info.Point.y);
                    LogInfo(s);
                    LogInfo(info.Point);
                }
            }

            LogInfo(intersectionType);
            if (test != find) LogError("test != find");
            if (intersectionType != info.IntersectionType) LogError("intersectionType != info.IntersectionType");
        }

        private void failure(string text)
        {
            feedback.color = Color.red;
            feedback.text = text;
        }

        private void success(string text = "טוב מאוד! כל הכבוד הצלחתם!")
        {
            feedback.color = Color.green;
            feedback.text = text;
        }

        private void turnFeedbackOff()
        {
           
            feedback.gameObject.SetActive(false);
            
        }

        private bool CheckInputVector(TMP_InputField x, TMP_InputField y)
        {
            if (inputDefaults.Contains(x.text) || inputDefaults.Contains(y.text))
            {
                failure("לא הזנת תשובה בכל השדות");
                return false;
            }
            if (x.contentType != TMP_InputField.ContentType.DecimalNumber
                || y.contentType != TMP_InputField.ContentType.DecimalNumber)
            {
                failure("הכנס מספרים עשרוניים בלבד");
                return false;
            }
            return true;
        }

        private Vector2 GetVector2Input(TMP_InputField x, TMP_InputField y)
        {
            Vector2 answer = new Vector2(float.Parse(x.textComponent.text),
                float.Parse(y.textComponent.text));
            return MathUtil.RoundVector(answer);
        }

        private void GradePointIntersection()
        {
            AnswerPoint.gameObject.SetActive(false);
            if (dropDownMenu.value != (int)DropdownChoices.POINT)
            {
                failure("טעית בבחירה, הבחירה הנכונה היא: נקודת חיתוך");
                return;
            }

            if (!CheckInputVector(inputX, inputY))
            {
                return;
            }

            Vector2 answer = GetVector2Input(inputX, inputY);

            AnswerPoint.gameObject.SetActive(true);
            AnswerPoint.gameObject.transform.position = answer;
            Vector2 correct = MathUtil.RoundVector(info.Point);
            LogInfo(answer + "\n" + correct);

            MeshRenderer mr = AnswerPoint.GetComponent<MeshRenderer>();
            if (answer != correct)
            {
                mr.material.color = Color.red;
                failure("התוצאה לא נכונה");
                return;
            }
            else
            {
                mr.material.color = Color.green;
                success();
            }
        }

        private void GradeLineIntersection()
        {
            if (dropDownMenu.value != (int)DropdownChoices.LINE)
            {
                failure("טעית בבחירה, הבחירה הנכונה היא: קו חיתוך");
                return;
            }

            if (!CheckInputVector(inputX, inputY) || !CheckInputVector(inputX1, inputY1))
            {
                return;
            }

            Vector2 center = GetVector2Input(inputX, inputY),
                direction = GetVector2Input(inputX1, inputY1);

            Line2 l = new Line2(center, direction);
            IntersectionTypes t = new IntersectionTypes();
            bool doesAnswerIntersectLine0 = Intersection.TestLine2Line2(ref line0, ref l, out t);
            answerLine.transform.position = center;
            answerLine.gameObject.SetActive(true);
            answerLine.vectorLine.SetWidth(4);
            if (doesAnswerIntersectLine0 && t == IntersectionTypes.Line)
            {
                answerLine.SetLineColor(Color.green);

                answerLine.UpdateLine2();
                success();
            }
            else
            {
                answerLine.SetLineColor(Color.red);
                answerLine.UpdateLine2();
                failure("הקו שהכנסת אינו נמצא על אחד הקווים");
            }



        }

        private void GradeEmptyIntersection()
        {
            if (dropDownMenu.value != (int)DropdownChoices.EMPTY)
            {
                failure("טעית בבחירה, הבחירה הנכונה היא: אין חיתוך");
                return;
            }
            else success();
        }

        public void Submit()
        {
            feedback.gameObject.SetActive(true);
            Invoke("turnFeedbackOff", 7);
            //(turnFeedbackOff);
            if (info.IntersectionType == IntersectionTypes.Point)
            {
                GradePointIntersection();
            }
            else if (info.IntersectionType == IntersectionTypes.Line)
            {
                GradeLineIntersection();
            }
            else if (info.IntersectionType == IntersectionTypes.Empty)
            {
                GradeEmptyIntersection();
            }
        }

        public void DropdownUpdate()
        {
            if (dropDownMenu.value == 0)
            {
                pointInput.SetActive(true);
                directionInput.SetActive(false);
            }
            else if (dropDownMenu.value == 1)
            {
                pointInput.SetActive(true);
                directionInput.SetActive(true);
            }
            else if (dropDownMenu.value == 2)
            {
                pointInput.SetActive(false);
                directionInput.SetActive(false);
            }
        }

    }
}
