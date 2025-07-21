// Place in Assets/Editor/UIControlShowcase.cs

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEditor.UIElements;

public class UIControlShowcase : EditorWindow
{
    [MenuItem("Tools/UI Control Showcase")]
    public static void ShowWindow()
    {
        UIControlShowcase wnd = GetWindow<UIControlShowcase>();
        wnd.titleContent = new GUIContent("UI Control Showcase");
    }

    public void CreateGUI()
    {
        // The root VisualElement of the window
        VisualElement root = rootVisualElement;

        // Use a ScrollView to ensure all content is visible even if the window is small
        var scrollView = new ScrollView(ScrollViewMode.Vertical);
        root.Add(scrollView);

        // --- Add different sections for UI controls ---
        CreateBasicInputSection(scrollView);
        CreateSlidersSection(scrollView);
        CreateButtonsAndChoicesSection(scrollView);
        CreateDataViewsSection(scrollView);
        CreateContainersSection(scrollView);
    }

    /// ## Section 1: Basic Input Fields ⌨️
    /// Used for direct data entry from the user.
    private void CreateBasicInputSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Basic Input Fields", value = true };
        container.Add(foldout);

        // TextField: For strings
        var textField = new TextField("Object Name:");
        textField.value = "MyGameObject";
        textField.RegisterValueChangedCallback(evt => Debug.Log($"New Name: {evt.newValue}"));
        foldout.Add(textField);

        // IntegerField: For whole numbers
        var intField = new IntegerField("Instance Count:");
        intField.value = 10;
        foldout.Add(intField);

        // FloatField: For decimal numbers
        var floatField = new FloatField("Weight (kg):");
        floatField.value = 25.5f;
        foldout.Add(floatField);

        // Vector3Field: For 3D coordinates or vectors
        var vector3Field = new Vector3Field("Position Offset:");
        vector3Field.value = new Vector3(1.5f, 0, 3.2f);
        foldout.Add(vector3Field);

        // ColorField: For picking colors
        var colorField = new ColorField("Material Color:");
        colorField.value = Color.cyan;
        foldout.Add(colorField);

        // Toggle: A simple on/off checkbox
        var toggleField = new Toggle("Enable Physics?");
        toggleField.value = true;
        foldout.Add(toggleField);
    }

    /// ## Section 2: Sliders & Ranges 🎚️
    /// Ideal for adjusting values within a defined range.
    private void CreateSlidersSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Sliders & Ranges", value = true };
        container.Add(foldout);

        // Slider: For float values in a range
        var slider = new Slider("Light Intensity:", 0, 10);
        slider.value = 5.0f;
        slider.showInputField = true; // Show a float field next to the slider
        foldout.Add(slider);

        // SliderInt: For integer values in a range
        var sliderInt = new SliderInt("Quality Level:", 1, 5);
        sliderInt.value = 3;
        sliderInt.showInputField = true;
        foldout.Add(sliderInt);

        // MinMaxSlider: To define a range (e.g., for filtering)
        var minMaxSlider = new MinMaxSlider("Filter by Size:", 0.0f, 100.0f, 10.0f, 70.0f);
        foldout.Add(minMaxSlider);
    }

    /// ## Section 3: Buttons & Choices 👇
    /// For triggering actions or selecting from a set of options.
    private void CreateButtonsAndChoicesSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Buttons & Choices", value = true };
        container.Add(foldout);

        // Button: To trigger an action
        var button = new Button(() => Debug.Log("Process button clicked!"))
        {
            text = "Process Data"
        };
        foldout.Add(button);

        // DropdownField: A classic dropdown menu for selection
        var dropdownOptions = new List<string> { "Low", "Medium", "High", "Ultra" };
        var dropdown = new DropdownField("Texture Quality:", dropdownOptions, 0);
        foldout.Add(dropdown);

        // RadioButtonGroup: To select one exclusive option
        var radioGroup = new RadioButtonGroup("Target Platform:", new List<string> { "PC", "Mobile", "Console" });
        foldout.Add(radioGroup);
    }

    /// ## Section 4: Data & Collection Views 📊
    /// Essential for displaying lists or hierarchical data.
    private void CreateDataViewsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Data & Collection Views 📊", value = true };
        container.Add(foldout);

        // --- ListView Example (for comparison) ---
        foldout.Add(new Label("Asset List (ListView):"));
        var items = new List<string> { "MainCamera", "Directional Light", "Character.fbx" };
        var listView = new ListView(items, 20, () => new Label(), (element, index) => (element as Label).text = items[index]);
        listView.style.height = 80;
        foldout.Add(listView);

        // --- TreeView Example (Corrected Implementation) ---
        foldout.Add(new Label("Scene Hierarchy (TreeView):") { style = { marginTop = 10 } });

        // 1. Prepare your data source
        var treeViewData = new List<TreeViewItemData<string>>()
    {
        new TreeViewItemData<string>(0, "SceneRoot"),
        new TreeViewItemData<string>(1, "Environment", new List<TreeViewItemData<string>>
        {
            new TreeViewItemData<string>(2, "Ground"),
            new TreeViewItemData<string>(3, "Buildings")
        }),
        new TreeViewItemData<string>(4, "Player")
    };

        // 2. Create the TreeView with ONLY the makeItem and bindItem functions
        var treeView = new TreeView(
            () => new Label(),
            (element, id) => (element as Label).text = treeViewData.Find(item => item.id == id).data
        );

        // 3. Set properties on the instance
        treeView.itemHeight = 20; // Set item height as a property
        treeView.style.height = 80;

        // 4. THIS IS THE FIX: Use SetRootItems() to populate the TreeView
        treeView.SetRootItems(treeViewData);

        foldout.Add(treeView);
    }

    /// ## Section 5: Containers & Layout 📦
    /// Used to organize and structure your UI.
    private void CreateContainersSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Containers & Layout", value = true };
        container.Add(foldout);
        foldout.Add(new Label("The sections in this window use 'Foldout' containers.") { style = { whiteSpace = WhiteSpace.Normal}});
        
        // GroupBox: A container with a title and a border
        var groupBox = new GroupBox("Tool Settings");
        groupBox.Add(new Toggle("Auto-Save"));
        groupBox.Add(new TextField("Backup Path:"));
        foldout.Add(groupBox);
    }
}