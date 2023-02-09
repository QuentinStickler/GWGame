using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RoomTransparency;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;
using Vertex = UnityEngine.ProBuilder.Vertex;

public class ColliderBuilder : MonoBehaviour
{
    // [MenuItem("CONTEXT/Room/Add Collider")]
    [MenuItem("GameObject/Room Settings/Add Collider", false, 10)]
    static void CreateCollider(MenuCommand command)
    {
        // var q = (Room)command.context;
        // var t = q.gameObject;

        var t = command.context as GameObject;
        
        var meshes = t.GetComponentsInChildren<ProBuilderMesh>();

        var floorMesh = meshes.First(x => x.gameObject.name.Contains("Floor"));

        if (floorMesh == null)
        {
            throw new MissingComponentException("No ProBuilder object named \"Floor\" was found among object's children");
        }

        var floor = floorMesh.gameObject;
        
        var vertices = floorMesh.GetVertices();
        
        var x = new Vector3();
        var z = new Vector3();
        
        var diagonal = 0f;

        var v1 = new Vertex();
        var v2 = new Vertex();

        for (int i = 0; i < vertices.Length - 1; i++)
        {
            for (int j = i + 1; j < vertices.Length; j++)
            {
                if(vertices[i].position.y != vertices[j].position.y) continue;

                var d = Vector3.Magnitude(vertices[i].position - vertices[j].position);
                
                if (d <= diagonal) continue;

                diagonal = d;

                v1 = vertices[i];
                v2 = vertices[j];
            }
        }

        var collider = t.GetComponent<BoxCollider>() == null ? Undo.AddComponent(t, typeof(BoxCollider)) as BoxCollider : t.GetComponent<BoxCollider>();
        
        // Assembly assembly = Assembly.Load("UnityEditor.dll");
        // Type gridSettings = assembly.GetType("UnityEditor.GridSettings");
        // var gridSize = (Vector3) gridSettings.GetProperty("size").GetValue("size");

        collider.isTrigger = true;
        collider.center = new Vector3((v1.position.x + v2.position.x) / 2f,
                              1f, 
                              (v1.position.z + v2.position.z) / 2) +
                          new Vector3(floor.transform.localPosition.x, 0f, floor.transform.localPosition.z);
        
        collider.size = new Vector3(Mathf.Abs(v1.position.x - v2.position.x) - 2f, 6f, Mathf.Abs(v1.position.z - v2.position.z) - 2f);

        // collider.center = new Vector3((int)(collider.center.x / gridSize.x) * gridSize.x,
            // (int)(collider.center.y / gridSize.y) * gridSize.y,
            // (int)(collider.center.z / gridSize.z) * gridSize.z);
        // collider.size = new Vector3((int) (collider.size.x / gridSize.x) * gridSize.x,
            // (int) (collider.size.y / gridSize.y) * gridSize.y,
            // (int) (collider.size.z / gridSize.z) * gridSize.z);
    }
}
