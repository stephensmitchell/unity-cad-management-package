// Place in Assets/Editor/ExtrudableObjectEditor.cs

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExtrudableObject))]
public class ExtrudableObjectEditor : Editor
{
    public void OnSceneGUI()
    {
        var extrudableObject = (ExtrudableObject)target;
        Transform transform = extrudableObject.transform;

        // For this example, we'll just extrude the top face (+Y) of a cube.
        // A real tool would have face selection logic.
        Vector3 faceNormal = Vector3.up;
        Vector3 faceCenter = new Vector3(0, 0.5f, 0);

        // Convert to world space
        Vector3 worldNormal = transform.TransformDirection(faceNormal);
        Vector3 worldCenter = transform.TransformPoint(faceCenter);

        Handles.color = Color.cyan;
        float handleSize = HandleUtility.GetHandleSize(worldCenter) * 0.2f;

        // Draw the handle
        Vector3 newWorldPosition = Handles.Slider(worldCenter, worldNormal, handleSize, Handles.ArrowHandleCap, 0.01f);

        if (GUI.changed)
        {
            // Calculate the extrusion distance
            float extrusionAmount = Vector3.Dot(newWorldPosition - worldCenter, worldNormal);
            if (Mathf.Abs(extrusionAmount) > 0.001f)
            {
                Undo.RecordObject(extrudableObject.GetComponent<MeshFilter>(), "Extrude Face");
                ExtrudeMesh(extrudableObject.GetComponent<MeshFilter>().sharedMesh, extrusionAmount);
            }
        }
    }

    // A very basic extrusion implementation for demonstration
    private void ExtrudeMesh(Mesh mesh, float distance)
    {
        // This is a simplified example assuming a basic cube
        // A robust implementation would be much more complex
        Vector3[] oldVerts = mesh.vertices;
        Vector3[] newVerts = new Vector3[oldVerts.Length];
        System.Array.Copy(oldVerts, newVerts, oldVerts.Length);

        // Identify and move the top 4 vertices of a standard cube
        for (int i = 0; i < newVerts.Length; i++)
        {
            if (Mathf.Approximately(newVerts[i].y, 0.5f))
            {
                newVerts[i] += Vector3.up * distance;
            }
        }
        mesh.vertices = newVerts;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}