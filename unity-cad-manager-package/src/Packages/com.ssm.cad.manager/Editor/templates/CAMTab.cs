// Place in Assets/Editor/CAMTab.cs

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CAMTab : VisualElement
{
    private Slider _timeSlider;
    private Vector3[] _toolpathPoints;
    private static GameObject _toolRepresentation;

    public CAMTab()
    {
        this.style.paddingLeft = 10;
        this.style.paddingTop = 10;
        Add(new Label("CAM Toolpath Simulation"));

        _timeSlider = new Slider(0, 1)
        {
            label = "Scrub Timeline",
            value = 0
        };
        _timeSlider.RegisterValueChangedCallback(OnSliderChanged);
        Add(_timeSlider);

        // Define a simple square toolpath
        _toolpathPoints = new Vector3[]
        {
            new Vector3(-1, 0, -1),
            new Vector3(1, 0, -1),
            new Vector3(1, 0, 1),
            new Vector3(-1, 0, 1),
            new Vector3(-1, 0, -1) // Close the loop
        };

        // Register to draw in the scene view
        SceneView.duringSceneGui += OnSceneGUI;

        // Cleanup when the tab is removed
        this.RegisterCallback<DetachFromPanelEvent>(evt => SceneView.duringSceneGui -= OnSceneGUI);
    }

    private void OnSliderChanged(ChangeEvent<float> evt)
    {
        UpdateToolPosition(evt.newValue);
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (this.panel == null) return; // Don't draw if tab is not visible

        // Draw the toolpath in the scene
        Handles.color = Color.green;
        Handles.DrawPolyLine(_toolpathPoints);

        // Draw the tool if it exists
        if (_toolRepresentation != null)
        {
            Handles.Label(_toolRepresentation.transform.position + Vector3.up * 0.2f, "Tool Head");
        }
    }

    private void UpdateToolPosition(float time)
    {
        if (_toolRepresentation == null)
        {
            _toolRepresentation = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _toolRepresentation.name = "CAM_Tool_Head";
            _toolRepresentation.transform.localScale = Vector3.one * 0.1f;
            // Hide from hierarchy to avoid clutter
            _toolRepresentation.hideFlags = HideFlags.HideAndDontSave;
        }

        // Simple interpolation along the path
        float totalLength = 3f; // 4 segments of length 1
        float distance = time * totalLength;
        int segmentIndex = Mathf.FloorToInt(distance);
        if (segmentIndex >= _toolpathPoints.Length -1) segmentIndex = _toolpathPoints.Length - 2;

        float segmentTime = distance - segmentIndex;
        
        _toolRepresentation.transform.position = Vector3.Lerp(_toolpathPoints[segmentIndex], _toolpathPoints[segmentIndex + 1], segmentTime);
    }
}