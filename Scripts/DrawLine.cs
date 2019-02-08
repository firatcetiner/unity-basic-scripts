using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utils
{
    public class DrawLine : MonoBehaviour
    {
		private LineRenderer 	line;
		private bool 			isMousePressed;
		private Vector3 		mousePos;
		private List<Vector3> 	pointList;
		private Camera 			_camera;

		public Color lineColor = Color.black;
		public float lineSpeed = 10.0f;

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
			line.SetPosition(0, Vector3.MoveTowards(line.GetPosition(0), line.GetPosition(1), Time.deltaTime * lineSpeed));
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
			
			
			for (var i = 1; i < 3; i++)
			{
				if (pointList.Count >= 2) continue;
				pointList.Add(mousePos);
				line.positionCount = pointList.Count;
				line.SetPosition(pointList.Count - 1, pointList[pointList.Count - 1]);
			}
			line.SetPosition(1, mousePos);
			
		}

		private void CreateAndSetLine()
		{		
			line = gameObject.AddComponent<LineRenderer>();
			
			/* no shadow things */
			line.receiveShadows = false;
			line.shadowCastingMode = ShadowCastingMode.Off;
			line.allowOcclusionWhenDynamic = false;
			line.shadowBias = 0.0f;
			
			/* create and set material */
			line.material =  new Material(Shader.Find("Particles/Standard Unlit"));
			
			/* other line settings */
			line.positionCount = 0;
			line.startWidth = 0.2f;
			line.endWidth = 0.2f;
			line.startColor = lineColor;
			line.endColor = lineColor;
			line.useWorldSpace = true;
			line.numCapVertices = 30; // make it less edgy
		}
    }	     
}

