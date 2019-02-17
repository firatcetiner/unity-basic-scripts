using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Hybrid.Components
{
    [Serializable]
    public class LineComponent : MonoBehaviour
    {

        public LineRenderer line;
        public bool isMousePressed;
        [HideInInspector]
        public Vector3 mousePos;
        [HideInInspector]
        public List<Vector3> pointList;
        public Camera camera;

        public Color lineColor = Color.black;
        public float lineSpeed = 10.0f;
    }
}
