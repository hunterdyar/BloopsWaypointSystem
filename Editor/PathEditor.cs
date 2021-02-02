using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Path = Bloops.WaypointSystem.Path;

    // might steal some ideas from: https://github.com/nubick/unity-smart-waypoints/blob/master/Assets/Plugins/Smart2DWaypoints/Editor/PathEditor.cs

    [CustomEditor(typeof(Bloops.WaypointSystem.Path))]
    public class PathEditor : Editor
    {
        private Path path;

        private const string namePrefix = "WP";
        //Draw lines/Handles.
        void OnSceneGUI()
        {
            path = target as Path;
            if (path.Waypoints == null)
            {
                return;
            }

            if (path.Waypoints.Length < 2)
            {
                return;
            }

            if (Event.current.type == EventType.Repaint)
            {
                for (int i = 0; i < path.Waypoints.Length - 1; i++)
                {
                //    DrawArrowLines(path.Waypoints[i].transform.position, path.Waypoints[i + 1].transform.position);
                }

                if (path.closed)
                {
                 //   DrawArrowLines(path.Waypoints[path.Waypoints.Length - 1].transform.position, path.Waypoints[0].transform.position);
                }
            }
            //

            for (int i = 0; i < path.Waypoints.Length; i++)
            {
                
                Handles.color = Color.white;

                path.Waypoints[i].position = Handles.FreeMoveHandle(path.Waypoints[i].position, Quaternion.identity, HandleUtility.GetHandleSize(path.Waypoints[i].position) * 0.1f, Vector3.one, Handles.CubeHandleCap);

                if (i == 0)
                {
                    Handles.color = Color.blue;
                    path.SetPosition(i, Handles.FreeMoveHandle(path.GetPosition(i), Quaternion.identity, HandleUtility.GetHandleSize(path.GetPosition(i)) * 0.2f, Vector3.one, Handles.SphereHandleCap));
                }
                else
                {
                    Handles.color = Color.black;
                    path.SetPosition(i, Handles.FreeMoveHandle(path.GetPosition(i), Quaternion.identity, HandleUtility.GetHandleSize(path.GetPosition(i)) * 0.1f, Vector3.one, Handles.CircleHandleCap));
                }
            }
            
            for (int i = 0; i < path.Waypoints.Length-1; i++)
            {
                DrawArrowLine(path.GetPosition(i), path.GetPosition(i+1));
            }

            if (path.closed)
            {
                DrawArrowLine(path.GetPosition(path.Waypoints.Length-1), path.GetPosition(0));
            }
            
        }

        void DrawArrowLine(Vector3 start, Vector3 end)
        {
            
            Vector3 camForward = SceneView.currentDrawingSceneView.camera.transform.forward;
            Vector3 diff = end - start;
            Handles.DrawLine(start,end);
            Vector3 midpoint = Vector3.Lerp(start, end, 0.5f);
            float radius = Mathf.Min(diff.magnitude/2,HandleUtility.GetHandleSize(midpoint) * 0.2f);
            
            Handles.DrawSolidArc(midpoint,camForward,-diff,20,radius);
            Handles.DrawSolidArc(midpoint,camForward,-diff,-20,radius);
            midpoint = Vector3.Lerp(start, end, 0.33f);
            Handles.DrawSolidArc(midpoint,camForward,-diff,20,radius);
            Handles.DrawSolidArc(midpoint,camForward,-diff,-20,radius);
            midpoint = Vector3.Lerp(start, end, 0.66f);
            Handles.DrawSolidArc(midpoint,camForward,-diff,20,radius);
            Handles.DrawSolidArc(midpoint,camForward,-diff,-20,radius);
        }
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Path path = target as Path;
            if (GUILayout.Button("Set Waypoints From Children"))
            {
               path.SetWaypointsFromChildren();
            }
            if (GUILayout.Button("Flatten z"))
            {
                path.Flatten();
            }

        }
        
       
    }

