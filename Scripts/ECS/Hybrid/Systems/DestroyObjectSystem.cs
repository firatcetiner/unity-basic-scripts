using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using ECS.Hybrid.Components;

namespace ECS.Hybrid.Systems
{
    public class DestroySystem : ComponentSystem
    {
        private struct Group
        {
            [ReadOnly] public ComponentArray<MoveOnPathComponent> component; // MoveOnPathComponent is a Monobehaviour
            public readonly int Length;
        }

        [Inject] private Group _group; // nearly same as GetEntities<>()
        protected override void OnUpdate()
        {
            for(int i = 0; i < _group.Length; i++)
            {
                //read next in group:
                var component = _group.component[i];


                //destroy gameobject
                if(component.CurrentWayPointID > 13)
                {
                    Object.Destroy(component.gameObject);
                }
            }
        } 
    }
}
