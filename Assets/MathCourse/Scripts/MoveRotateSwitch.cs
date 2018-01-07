using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveRotateSwitch : MonoBehaviour {
    public bool isMoveMode;
    public bool isRotateMode;

    public DragAndDrop drag;
    public MouseRotate rotate;

    public Text switchButton;
	// Use this for initialization
    public void SwitchToRotate()
    {
        isRotateMode = true;
        isMoveMode = false;
        RefreshSwitch();
        if (switchButton) switchButton.text = "בבוס";
    }

    public void SwitchToMove()
    {
        isRotateMode = false;
        isMoveMode = true;
        RefreshSwitch();
        if (switchButton) switchButton.text = "זזה";
    }

    public void ChangeMode()
    {
        if (isMoveMode) SwitchToRotate();
        else SwitchToMove();
    }

    public void RefreshSwitch()
    {
        rotate.enabled = isRotateMode;
        drag.enabled = isMoveMode;
    }

    void Start ()
    {
		if (isMoveMode == isRotateMode) isMoveMode = !isRotateMode;
        RefreshSwitch();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
