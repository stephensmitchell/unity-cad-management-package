// Place in Assets/Editor/SchematicsTab.cs

using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SchematicsTab : VisualElement
{
    private SimpleGraphView _graphView;

    public SchematicsTab()
    {
        // Create an instance of our custom GraphView
        _graphView = new SimpleGraphView
        {
            name = "Schematics Graph"
        };

        // Stretch the GraphView to fill the entire tab
        _graphView.StretchToParentSize();
        this.Add(_graphView);

        // Add some example nodes
        CreateNode("Component A", new Vector2(100, 200));
        CreateNode("Component B", new Vector2(400, 250));
    }

    private void CreateNode(string nodeName, Vector2 position)
    {
        var node = new Node()
        {
            title = nodeName
        };
        node.SetPosition(new Rect(position, new Vector2(100, 150))); // Default size

        // Add input/output ports for connecting
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
        inputPort.portName = "In";
        node.inputContainer.Add(inputPort);

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
        outputPort.portName = "Out";
        node.outputContainer.Add(outputPort);

        _graphView.AddElement(node);
    }
}

// A simple GraphView implementation
public class SimpleGraphView : GraphView
{
    public SimpleGraphView()
    {
        // Add manipulators for zooming, panning, and selection
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        // Add a grid background
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
    }
}