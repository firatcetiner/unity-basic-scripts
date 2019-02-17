using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECS.Hybrid.Components;
using UnityEngine.Rendering;

namespace ECS.Hybrid.Systems
{
    public class DrawLineSystem : ComponentSystem
    {
        private struct Data
        {
            public LineComponent lineComponent;
        }

        protected override void OnStartRunning()
        {
            foreach(var entity in GetEntities<Data>())
            {
                var e = entity.lineComponent;

                e.camera = Camera.main; // Get MainCamera
                e.isMousePressed = false;
                e.pointList = new List<Vector3>();
                e.pointList.Clear();


                e.line = e.gameObject.AddComponent<LineRenderer>();

                /* no shadow things */
                e.line.receiveShadows = false;
                e.line.shadowCastingMode = ShadowCastingMode.Off;
                e.line.allowOcclusionWhenDynamic = false;
                e.line.shadowBias = 0.0f;

                /* create and set material */
                e.line.material = new Material(Shader.Find("Particles/Standard Unlit"));

                /* other line settings */
                e.line.positionCount = 0;
                e.line.startWidth = 0.2f;
                e.line.endWidth = 0.2f;
                e.line.startColor = e.lineColor;
                e.line.endColor = e.lineColor;
                e.line.useWorldSpace = true;
                e.line.numCapVertices = 30; // make it less edgy
            }
            
        }

        protected override void OnUpdate()
        {
            var dt = Time.deltaTime;
            foreach(var entity in GetEntities<Data>())
            {
                var e = entity.lineComponent;
                var transform = entity.transform;

                e.line.SetPosition(0, Vector3.MoveTowards(e.line.GetPosition(0), e.line.GetPosition(1), dt * e.lineSpeed));
                if (Input.GetMouseButtonDown(0))
                {
                    e.isMousePressed = true;
                    e.line.positionCount = 0;
                    e.pointList.RemoveRange(0, e.pointList.Count);
                }
                else if (Input.GetMouseButtonUp(0))
                    e.isMousePressed = false;

                if (!e.isMousePressed) return;
                e.mousePos = e.camera.ScreenToWorldPoint(Input.mousePosition);
                e.mousePos.z = 0;

                for (var i = 1; i < 3; i++)
                {
                    if (e.pointList.Count >= 2) continue;
                    e.pointList.Add(e.mousePos);
                    e.line.positionCount = e.pointList.Count;
                    e.line.SetPosition(e.pointList.Count - 1, e.pointList[e.pointList.Count - 1]);
                }
                e.line.SetPosition(1, e.mousePos);
            }
        }
    }
}
