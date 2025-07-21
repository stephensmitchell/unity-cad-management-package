// Place in Assets/Editor/PluginHelpWindow.cs

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PluginHelpWindow : EditorWindow
{
    [MenuItem("Tools/My Awesome Plugin/About & Help")]
    public static void ShowMainWindow()
    {
        // GetWindow<T>() creates and shows the window. It will focus an existing one or create a new one.
        PluginHelpWindow wnd = GetWindow<PluginHelpWindow>();
        wnd.titleContent = new GUIContent("About My Plugin");
        wnd.minSize = new Vector2(350, 400);
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        root.style.paddingLeft = 10;
        root.style.paddingRight = 10;

        // --- Header Section ---
        var headerLabel = new Label("My Awesome Plugin")
        {
            style = { fontSize = 20, unityFontStyleAndWeight = FontStyle.Bold, unityTextAlign = TextAnchor.MiddleCenter, marginTop = 10, marginBottom = 5 }
        };
        var versionLabel = new Label("Version 1.0.0 - (c) 2025 Your Name")
        {
            style = { unityTextAlign = TextAnchor.MiddleCenter, color = Color.gray }
        };
        root.Add(headerLabel);
        root.Add(versionLabel);

        // Separator
        root.Add(new VisualElement() { style = { height = 2, backgroundColor = new Color(0.2f, 0.2f, 0.2f), marginTop = 10, marginBottom = 10 } });

        // --- Description ---
        var description = new Label("Thank you for using My Awesome Plugin! This tool helps you do amazing things inside the Unity Editor. Use the links below to learn more.")
        {
            style = { whiteSpace = WhiteSpace.Normal, marginBottom = 15 }
        };
        root.Add(description);

        // --- Sub-Window Launchers ---
        CreateSubWindowButtons(root);
        
        // --- Showcase of other controls ---
        CreateShowcaseSection(root);
    }

    private void CreateSubWindowButtons(VisualElement container)
    {
        var groupBox = new GroupBox("Resources & Information");
        container.Add(groupBox);

        // This button will open a NORMAL sub-window (non-modal)
        var licenseButton = new Button(() => LicenseViewerWindow.ShowWindow())
        {
            text = "View Software License",
            tooltip = "Opens the EULA in a separate, non-modal window."
        };
        groupBox.Add(licenseButton);

        // This button will open a MODAL sub-window
        var updateButton = new Button(() => UpdateCheckWindow.ShowModalWindow())
        {
            text = "Check for Updates",
            tooltip = "Opens a modal dialog that must be closed before continuing."
        };
        groupBox.Add(updateButton);

        // Example of linking to external content
        var docsButton = new Button(() => Application.OpenURL("https://docs.unity3d.com/"))
        {
            text = "Open Online Documentation"
        };
        groupBox.Add(docsButton);
    }

    private void CreateShowcaseSection(VisualElement container)
    {
        // You can still include other controls here just like in the previous example
        var foldout = new Foldout() { text = "Example Plugin Settings", value = false };
        foldout.Add(new Toggle("Enable Advanced Features"));
        foldout.Add(new SliderInt("Cache Size (MB)", 32, 512) { value = 128 });
        foldout.Add(new ColorField("Gizmo Color") { value = Color.yellow });
        container.Add(foldout);
    }
}