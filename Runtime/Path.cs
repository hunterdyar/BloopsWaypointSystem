using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bloops.WaypointSystem
{
    //Path parents a set of waypoints that define a, er, path.
    //Provides interfaces for getting and using the waypoints in code.
    public class Path : MonoBehaviour
    {
        public bool closed = false;
        [SerializeField] private Waypoint[] waypoints;
        public Waypoint[] Waypoints => waypoints;
        public Vector3[] WaypointsAsV3(PathSpace space = PathSpace.world)
        {
            switch (space)
            {
                case PathSpace.local:
                    return waypoints.Select(x =>x.transform.position - transform.position).ToArray();//note: We don't use transform.localPosition because nesting waypoints while copying and pasting is bad... but shouldn't break the code.
                case PathSpace.world:
                default:
                    return waypoints.Select(x =>x.transform.position).ToArray();
            }
        }
        public Vector2[] WaypointsAsV2(PathSpace space = PathSpace.world)
        {
            switch (space)
            {
                case PathSpace.local:
                    return waypoints.Select(x =>(Vector2)(x.transform.position - transform.position)).ToArray();//note: We don't use transform.localPosition because nesting waypoints while copying and pasting is bad... but shouldn't break the code.
                case PathSpace.world:
                default:
                    return waypoints.Select(x =>(Vector2)(x.transform.position)).ToArray();
            }
        }

        public void SetWaypointsFromChildren()
        {
            List<Waypoint> wp = new List<Waypoint>();
            foreach (Transform t in transform)
            {
                if (t.GetComponent<Waypoint>() != null)
                {
                    wp.Add(t.GetComponent<Waypoint>());
                }
            }
            waypoints = wp.ToArray();
        }
        
        public Vector3 GetPosition(int ind)
        {
            return Waypoints[ind].position;
        }
        
        public void SetPosition(int ind, Vector3 position)
        {
            Waypoints[ind].position = position;
        }

        public void Flatten()
        {
            foreach (var wp in waypoints)
            {
                wp.transform.localPosition = new Vector3(wp.transform.localPosition.x,wp.transform.localPosition.y,0);
            }
        }
    }
}