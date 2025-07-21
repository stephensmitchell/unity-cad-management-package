// Place in Assets/Editor/CADTab.cs

using UnityEngine.UIElements;
using UnityEngine;

public class CADTab : VisualElement
{
    public CADTab()
    {
        this.style.paddingLeft = 10;
        this.style.paddingTop = 10;
        Add(new Label("3D CAD Tools"));
        Add(new Label("Select an object with the 'ExtrudableObject' component in the Scene.")
        {
            style = { whiteSpace = WhiteSpace.Normal, marginTop = 10}
        });
        Add(new Label("A blue handle will appear to perform a simple face extrude.")
        {
            style = { whiteSpace = WhiteSpace.Normal }
        });
    }
}