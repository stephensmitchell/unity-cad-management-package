// Place this entire script in Assets/Editor/LicenseViewerWindow.cs

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LicenseViewerWindow : EditorWindow
{
    // THIS IS THE METHOD THAT WAS MISSING OR INCORRECT
    // It must be public and static to be called from other scripts.
    public static void ShowWindow()
    {
        LicenseViewerWindow wnd = GetWindow<LicenseViewerWindow>();
        wnd.titleContent = new GUIContent("Software License");
        wnd.minSize = new Vector2(500, 400);
    }

    public void CreateGUI()
    {
        // Add your UI for the license window here...
        var scrollView = new ScrollView();
        rootVisualElement.Add(scrollView);

        var label = new Label("The software license text would go here.")
        {
            style = { whiteSpace = WhiteSpace.Normal, paddingLeft = 10, paddingRight = 10 }
        };

        scrollView.Add(label);
    }
}