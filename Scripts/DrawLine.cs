using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utils
{
    public class DrawLine : MonoBehaviour
    {
		private LineRenderer 		line;
		private bool 			isMousePressed;
		private Vector3 		mousePos;
		private List<Vector3> 		pointList;
		private Camera 			_camera;

		public Color lineColor = 	Color.black;
		public float lineSpeed = 	10.0f;

		private void Awake()
		{
			_camera = Camera.main; // Get MainCamera
			CreateAndSetLine();
			isMousePressed = false;
			pointList = new List<Vector3>();
			pointList.Clear();
		}

		private void Update()
		{
			// keep tracking the latest mouse position - which is line's 2nd position - and move 1st position to 2nd
			line.SetPosition(0, Vector3.MoveTowards(line.GetPosition(0), line.GetPosition(1), Time.deltaTime * lineSpeed));
			/* If mouse button is pressed, remove the points in the list so that the line will be invisible */
			if (Input.GetMouseButtonDown(0))
			{
				isMousePressed = true;
				line.positionCount = 0;
				pointList.RemoveRange(0, pointList.Count);
				line.startColor = lineColor;
				line.endColor = lineColor;	
			}
			else if (Input.GetMouseButtonUp(0))			
				isMousePressed = false;

			if (!isMousePressed) return;
			mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			
			/* Adding mouse position(Vector3) to point list, then draw line */
			for (var i = 1; i < 3; i++)
			{
				if (pointList.Count >= 2) continue;
				pointList.Add(mousePos);
				line.positionCount = pointList.Count;
				line.SetPosition(pointList.Count - 1, pointList[pointList.Count - 1]);
			}
			// Set line's 2nd position to be the mouse position.
			line.SetPosition(1, mousePos);
			
		}
		
		/* Initialize method for LineRenderer */
		private void CreateAndSetLine()
		{		
			line = gameObject.AddComponent<LineRenderer>(); // Add a new LineRenderer component to gameObject
			
			/* no shadow things */
			line.receiveShadows = false;
			line.shadowCastingMode = ShadowCastingMode.Off;
			line.allowOcclusionWhenDynamic = false;
			line.shadowBias = 0.0f;
			
			/* Create and set material */
			line.material =  new Material(Shader.Find("Particles/Standard Unlit"));
			
			/* Other line settings */
			line.positionCount = 0;
			/* Width settings for the line,
			 * you can adjust these values to make the line more spesific */
			line.startWidth = 0.2f;
			line.endWidth = 0.2f;
			
			line.startColor = lineColor;
			line.endColor = lineColor;
			line.useWorldSpace = true;
			line.numCapVertices = 30; // Make the line less edgy
		}
    }	     
}
