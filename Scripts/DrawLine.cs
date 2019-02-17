using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utils
{
    public class DrawLine : MonoBehaviour
    {
	private LineRenderer 		_line;
	private bool 			_isMousePressed;
	private Vector3 		_mousePos;
	private List<Vector3> 		_pointList;
	private Camera 			_camera;

	public Color lineColor = 	Color.black;
	public float lineSpeed = 	10.0f;

	private void Awake()
	{
		_camera = Camera.main; // Get MainCamera
		CreateAndSetLine();
		_isMousePressed = false;
		_pointList = new List<Vector3>();
		_pointList.Clear();
	}

	private void Update()
	{
		// keep tracking the latest mouse position - which is line's 2nd position - and move 1st position to 2nd
		_line.SetPosition(0, Vector3.MoveTowards(_line.GetPosition(0), _line.GetPosition(1), Time.deltaTime * lineSpeed));
		/* If mouse button is pressed, remove the points in the list so that the line will be invisible */
		if (Input.GetMouseButtonDown(0))
		{
			_isMousePressed = true;
			_line.positionCount = 0;
			_pointList.RemoveRange(0, _pointList.Count);
			_line.startColor = lineColor;
			_line.endColor = lineColor;	
		}
		else if (Input.GetMouseButtonUp(0))			
			_isMousePressed = false;

		if (!_isMousePressed) return;
		_mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
		_mousePos.z = 0;

		/* Adding mouse position(Vector3) to point list, then draw line */
		for (var i = 1; i < 3; i++)
		{
			if (_pointList.Count >= 2) continue;
			_pointList.Add(_mousePos);
			_line.positionCount = _pointList.Count;
			_line.SetPosition(_pointList.Count - 1, _pointList[_pointList.Count - 1]);
		}
		// Set line's 2nd position to be the mouse position.
		_line.SetPosition(1, _mousePos);

	}

	/* Initialize method for LineRenderer */
	private void CreateAndSetLine()
	{		
		_line = gameObject.AddComponent<LineRenderer>(); // Add a new LineRenderer component to gameObject

		/* no shadow things */
		_line.receiveShadows = false;
		_line.shadowCastingMode = ShadowCastingMode.Off;
		_line.allowOcclusionWhenDynamic = false;
		_line.shadowBias = 0.0f;

		/* Create and set material */
		_line.material =  new Material(Shader.Find("Particles/Standard Unlit"));

		/* Other line settings */
		_line.positionCount = 0;
		/* Width settings for the line,
		 * you can adjust these values to make the line more spesific */
		_line.startWidth = 0.2f;
		_line.endWidth = 0.2f;

		_line.startColor = lineColor;
		_line.endColor = lineColor;
		_line.useWorldSpace = true;
		_line.numCapVertices = 30; // Make the line less edgy
	}
    }	     
}
