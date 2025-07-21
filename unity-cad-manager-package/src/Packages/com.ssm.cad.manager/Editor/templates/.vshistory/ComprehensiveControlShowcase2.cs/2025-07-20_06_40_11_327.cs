// Place this script in an 'Editor' folder in your Unity project.

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ComprehensiveControlShowcase2 : EditorWindow
{
    // Define an enum for the EnumField and MaskField examples
    [System.Flags]
    public enum ToolOptions
    {
        None = 0,
        EnableLogging = 1 << 0,
        UseCache = 1 << 1,
        RunAsync = 1 << 2,
        ShowGizmos = 1 << 3
    }

    [MenuItem("Tools/Comprehensive Control Showcase")]
    public static void ShowWindow()
    {
        ComprehensiveControlShowcase wnd = GetWindow<ComprehensiveControlShowcase>();
        wnd.titleContent = new GUIContent("Control Showcase");
    }

    public void CreateGUI()
    {
        // A ScrollView is used as the root to ensure all controls are accessible
        var scrollView = new ScrollView(ScrollViewMode.Vertical);
        rootVisualElement.Add(scrollView);

        // --- Sections for different control types ---
        CreateBasicInputSection(scrollView);
        CreateSlidersAndRangesSection(scrollView);
        CreateChoicesAndSelectionsSection(scrollView);
        CreateDataViewsSection(scrollView);
        CreateObjectFieldsSection(scrollView);
        CreateAdvancedControlsSection(scrollView);
    }

    private void CreateBasicInputSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Basic Input Fields ⌨️", value = true };
        container.Add(foldout);

        foldout.Add(new TextField("Text Field") { value = "Enter object name...", tooltip = "Used for string input." });
        foldout.Add(new IntegerField("Integer Field") { value = 101, tooltip = "For whole numbers." });
        foldout.Add(new FloatField("Float Field") { value = 42.5f, tooltip = "For numbers with decimal points." });
        foldout.Add(new DoubleField("Double Field") { value = 99.999, tooltip = "For high-precision decimal numbers." });
        foldout.Add(new Vector3Field("Vector3 Field") { value = Vector3.one, tooltip = "For 3D vectors or coordinates." });
        foldout.Add(new RectField("Rect Field") { value = new Rect(10, 10, 200, 100), tooltip = "For 2D rectangles (x, y, width, height)." });
        foldout.Add(new BoundsField("Bounds Field") { value = new Bounds(Vector3.zero, Vector3.one), tooltip = "For 3D bounding boxes (center, size)." });
        foldout.Add(new Toggle("Toggle (Boolean)") { value = true, text = "Enable Feature", tooltip = "A simple on/off checkbox." });
    }

    private void CreateSlidersAndRangesSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Sliders & Ranges 🎚️", value = true };
        container.Add(foldout);

        foldout.Add(new Slider("Slider (0.0 to 1.0)") { lowValue = 0.0f, highValue = 1.0f, value = 0.5f, showInputField = true, tooltip = "Adjust a float value within a range." });
        foldout.Add(new SliderInt("SliderInt (1 to 10)") { lowValue = 1, highValue = 10, value = 7, showInputField = true, tooltip = "Adjust an integer value within a range." });
        foldout.Add(new MinMaxSlider("MinMaxSlider (Filter Range)", 0f, 100f, 25f, 75f) { tooltip = "Select a minimum and maximum value range." });
    }

    private void CreateChoicesAndSelectionsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Choices & Selections 👇", value = true };
        container.Add(foldout);

        var button = new Button(() => Debug.Log("Button was clicked at " + System.DateTime.Now)) { text = "Run Process", tooltip = "Triggers an action." };
        foldout.Add(button);

        var dropdownOptions = new List<string> { "Option A", "Option B", "Option C" };
        foldout.Add(new DropdownField("Dropdown", dropdownOptions, 0) { tooltip = "Select one item from a list." });

        foldout.Add(new RadioButtonGroup("Radio Group", new List<string> { "Debug", "Release", "Profile" }) { value = 0, tooltip = "Select one exclusive option." });

        foldout.Add(new EnumField("Enum Field", ToolOptions.RunAsync) { tooltip = "Select a value from a C# enum." });
    }

    private void CreateDataViewsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Data & Collection Views 📊", value = true };
        container.Add(foldout);

        // ListView example
        foldout.Add(new Label("ListView (List of Assets)") { style = { unityFontStyleAndWeight = FontStyle.Bold } });
        var fileItems = new List<string> { "ProjectSettings.asset", "MyScene.unity", "Player.prefab", "Icon.png" };
        var listView = new ListView(fileItems, 20, () => new Label(), (element, index) => (element as Label).text = fileItems[index])
        {
            selectionType = SelectionType.Multiple,
            style = { height = 80 }
        };
        foldout.Add(listView);

        // TreeView example
        foldout.Add(new Label("TreeView (Scene Hierarchy)") { style = { unityFontStyleAndWeight = FontStyle.Bold, marginTop = 10 } });
        var treeItems = new List<TreeViewItemData<string>>()
        {
            new TreeViewItemData<string>(0, "SceneRoot", new List<TreeViewItemData<string>>
            {
                new TreeViewItemData<string>(1, "Camera"),
                new TreeViewItemData<string>(2, "Lights", new List<TreeViewItemData<string>> { new TreeViewItemData<string>(4, "Directional Light") }),
                new TreeViewItemData<string>(3, "Environment")
            })
        };
        var treeView = new TreeView(treeItems, 22, (id) => new Label(), (element, id) => (element as Label).text = treeItems.Find(item => item.id == id).data)
        {
            style = { height = 80 }
        };
        foldout.Add(treeView);
    }

    private void CreateObjectFieldsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Object & Asset Fields 🔗", value = true };
        container.Add(foldout);

        foldout.Add(new ObjectField("GameObject Field") { objectType = typeof(GameObject), allowSceneObjects = true, tooltip = "Reference a GameObject from the scene or project." });
        foldout.Add(new ObjectField("Material Field") { objectType = typeof(Material), allowSceneObjects = false, tooltip = "Reference a Material asset." });
        foldout.Add(new ObjectField("Texture Field") { objectType = typeof(Texture2D), allowSceneObjects = false, tooltip = "Reference a Texture2D asset." });
    }
    
    private void CreateAdvancedControlsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Advanced & Specialty Controls ✨", value = true };
        container.Add(foldout);

        foldout.Add(new ColorField("Color Field") { value = Color.red, tooltip = "A visual color picker." });
        foldout.Add(new CurveField("Curve Field") { value = AnimationCurve.EaseInOut(0, 0, 1, 1), tooltip = "Edit an AnimationCurve." });
        foldout.Add(new GradientField("Gradient Field") { tooltip = "Edit a color Gradient." });
        foldout.Add(new LayerField("Layer Field") { tooltip = "Select a single physics layer." });
        foldout.Add(new MaskField("Mask Field (Layers)") { tooltip = "Select multiple physics layers." });
        foldout.Add(new MaskField("Mask Field (Enum Flags)", new List<string>(System.Enum.GetNames(typeof(ToolOptions))), (int)ToolOptions.EnableLogging | (int)ToolOptions.ShowGizmos) { tooltip = "Select multiple options from a flags enum."});
    }
}