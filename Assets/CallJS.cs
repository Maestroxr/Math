using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

[Serializable]
public class JsonWebgl
{
    public string inputId;
    public string optional;
}

public class CallJS : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void HelloString(string str);

    [DllImport("__Internal")]
    private static extern void PrintFloatArray(float[] array, int size);

    [DllImport("__Internal")]
    private static extern int AddNumbers(int x, int y);

    [DllImport("__Internal")]
    private static extern string StringReturnValueFunction();

    [DllImport("__Internal")]
    private static extern void BindWebGLTexture(int texture);

    

    [DllImport("__Internal")]
    private static extern void SubmitResult(string jsonData);


    JsonWebgl parameters;
    static CallJS calljs;
    void Start()
    {
        //Hello();
        /*
        HelloString("This is a string.");

        float[] myArray = new float[10];
        PrintFloatArray(myArray, myArray.Length);

        int result = AddNumbers(5, 7);
        Debug.Log(result);

        Debug.Log(StringReturnValueFunction());

        var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
        BindWebGLTexture(texture.GetNativeTextureID());
        */
        

        //string parameters = "{\"inputId\" : \"webgl_problem1\", \"optional\" : \"True\" }";
        //parameters = JsonUtility.FromJson<JsonWebgl>(jsonParameters);
       
        //Debug.Log("inputId:" + parameters.inputId +" optional:" + parameters.optional);
        
        calljs = this;
    }

    public static void Submit(string jsonData)
    {
        
        SubmitResult(jsonData);
    }
}