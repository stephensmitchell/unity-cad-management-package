// Place in Assets/Editor/ProToolSuiteWindow.cs

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ProToolSuiteWindow : EditorWindow
{
    private VisualElement[] tabs;
    private Button[] tabButtons;

    [MenuItem("Tools/Pro-Tool Suite")]
    public static void ShowWindow()
    {
        ProToolSuiteWindow wnd = GetWindow<ProToolSuiteWindow>();
        wnd.titleContent = new GUIContent("Pro-Tool Suite");
    }

    public void CreateGUI()
    {
        // Create a root VisualElement
        VisualElement root = rootVisualElement;

        // Create the main container with a horizontal layout for tabs + content
        var mainContainer = new VisualElement() { style = { flexDirection = FlexDirection.Row, flexGrow = 1 } };
        root.Add(mainContainer);

        // --- Create Tab Buttons ---
        var tabButtonContainer = new VisualElement()
        {
            style =
            {
                width = 120,
                borderRightWidth = 2,
                borderRightColor = new Color(0.2f, 0.2f, 0.2f)
            }
        };
        mainContainer.Add(tabButtonContainer);

        string[] tabNames = { "📝 2D Schematics", "🔩 3D CAD", "⚙️ CAM Sim", "🏗️ BIM Viewer" };
        tabButtons = new Button[tabNames.Length];
        for(int i = 0; i < tabNames.Length; i++)
        {
            int index = i; // Capture index for the click event
            var button = new Button(() => SelectTab(index)) { text = tabNames[i] };
            button.style.marginTop = 5;
            button.style.marginLeft = 5;
            button.style.marginRight = 5;
            tabButtonContainer.Add(button);
            tabButtons[i] = button;
        }

        // --- Create Content Area for Tabs ---
        var contentContainer = new VisualElement() { style = { flexGrow = 1 }};
        mainContainer.Add(contentContainer);

        // --- Create VisualElements for each tab's content ---
        tabs = new VisualElement[]
        {
            new SchematicsTab(), // From SchematicsTab.cs
            new CADTab(),        // From CADTab.cs
            new CAMTab(),        // From CAMTab.cs
            new BIMTab()         // From BIMTab.cs
        };

        foreach (var tab in tabs)
        {
            contentContainer.Add(tab);
        }

        // Select the first tab by default
        SelectTab(0);
    }

    private void SelectTab(int index)
    {
        // Hide all tabs and reset button styles
        for(int i = 0; i < tabs.Length; i++)
        {
            tabs[i].style.display = DisplayStyle.None;
            tabButtons[i].style.backgroundColor = new StyleColor(StyleKeyword.Undefined);
        }

        // Show the selected tab and highlight its button
        tabs[index].style.display = DisplayStyle.Flex;
        tabButtons[index].style.backgroundColor = new Color(0.3f, 0.4f, 0.6f);
    }
}