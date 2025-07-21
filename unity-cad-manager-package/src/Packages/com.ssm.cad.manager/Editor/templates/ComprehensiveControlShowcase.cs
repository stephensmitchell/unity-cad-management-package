// Place this script in an 'Editor' folder in your Unity project.

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Diagnostics; // Required for Process and ProcessStartInfo
using System.Text;
using UnityEditor.UIElements; // Required for StringBuilder

public class ComprehensiveControlShowcase : EditorWindow
{
    [System.Flags]
    public enum ToolOptions
    {
        None = 0, EnableLogging = 1 << 0, UseCache = 1 << 1, RunAsync = 1 << 2, ShowGizmos = 1 << 3
    }

    private TextField _cliOutput; // Field to hold a reference to our CLI output text area

    [MenuItem("Tools/Comprehensive Control Showcase")]
    public static void ShowWindow()
    {
        ComprehensiveControlShowcase wnd = GetWindow<ComprehensiveControlShowcase>();
        wnd.titleContent = new GUIContent("Control Showcase");
        wnd.minSize = new Vector2(400, 300);
    }

    public void CreateGUI()
    {
        var scrollView = new ScrollView(ScrollViewMode.Vertical);
        rootVisualElement.Add(scrollView);

        // --- All previous sections from the control showcase ---
        CreateBasicInputSection(scrollView);
        CreateSlidersAndRangesSection(scrollView);
        CreateChoicesAndSelectionsSection(scrollView);
        CreateDataViewsSection(scrollView);
        CreateObjectFieldsSection(scrollView);
        CreateAdvancedControlsSection(scrollView);

        // --- NEW: Sections for App Launcher and CLI Runner ---
        CreateAppLauncherSection(scrollView);
        CreateCliRunnerSection(scrollView);
    }

    #region UI Control Sections (Existing)
    private void CreateBasicInputSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Basic Input Fields ⌨️", value = true };
        container.Add(foldout);
        foldout.Add(new TextField("Text Field") { value = "Enter object name...", tooltip = "Used for string input." });
        foldout.Add(new IntegerField("Integer Field") { value = 101, tooltip = "For whole numbers." });
        foldout.Add(new FloatField("Float Field") { value = 42.5f, tooltip = "For numbers with decimal points." });
        foldout.Add(new Toggle("Toggle (Boolean)") { value = true, text = "Enable Feature", tooltip = "A simple on/off checkbox." });
    }

    private void CreateSlidersAndRangesSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Sliders & Ranges 🎚️", value = true };
        container.Add(foldout);
        foldout.Add(new Slider("Slider (0.0 to 1.0)") { lowValue = 0.0f, highValue = 1.0f, value = 0.5f, showInputField = true, tooltip = "Adjust a float value." });
        foldout.Add(new MinMaxSlider("MinMaxSlider (Filter Range)", 0f, 100f, 25f, 75f) { tooltip = "Select a minimum and maximum value range." });
    }

    private void CreateChoicesAndSelectionsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Choices & Selections 👇", value = true };
        container.Add(foldout);
        var button = new Button(() => UnityEngine.Debug.Log("Button clicked!")) { text = "Run Process", tooltip = "Triggers an action." };
        foldout.Add(button);
        var dropdownOptions = new List<string> { "Option A", "Option B", "Option C" };
        foldout.Add(new DropdownField("Dropdown", dropdownOptions, 0) { tooltip = "Select one item from a list." });
        foldout.Add(new EnumField("Enum Field", ToolOptions.RunAsync) { tooltip = "Select a value from a C# enum." });
    }
    
    private void CreateDataViewsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Data & Collection Views 📊", value = true };
        container.Add(foldout);
        var fileItems = new List<string> { "MyScene.unity", "Player.prefab" };
        var listView = new ListView(fileItems, 20, () => new Label(), (e, i) => (e as Label).text = fileItems[i]) { style = { height = 60 } };
        foldout.Add(listView);
    }

    private void CreateObjectFieldsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Object & Asset Fields 🔗", value = true };
        container.Add(foldout);
        foldout.Add(new ObjectField("GameObject Field") { objectType = typeof(GameObject), allowSceneObjects = true });
        foldout.Add(new ObjectField("Material Field") { objectType = typeof(Material), allowSceneObjects = false });
    }

    private void CreateAdvancedControlsSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Advanced & Specialty Controls ✨", value = true };
        container.Add(foldout);
        foldout.Add(new ColorField("Color Field") { value = Color.red });
        foldout.Add(new CurveField("Curve Field") { value = AnimationCurve.EaseInOut(0, 0, 1, 1) });
        foldout.Add(new LayerField("Layer Field"));
        foldout.Add(new MaskField("Mask Field (Enum Flags)", new List<string>(System.Enum.GetNames(typeof(ToolOptions))), (int)ToolOptions.EnableLogging | (int)ToolOptions.ShowGizmos) );
    }
    #endregion

    #region NEW: Launcher & Runner Sections

    /// ## Section for launching external applications
    private void CreateAppLauncherSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "App Launcher 🚀", value = true };
        container.Add(foldout);

        var customPathField = new TextField("App Path:") { value = "notepad.exe" };
        var launchCustomButton = new Button(() => LaunchApplication(customPathField.value)) { text = "Launch App" };
        
        var hbox = new VisualElement() { style = { flexDirection = FlexDirection.Row, alignItems = Align.Center } };
        customPathField.style.flexGrow = 1;
        hbox.Add(customPathField);
        hbox.Add(launchCustomButton);
        foldout.Add(hbox);

        foldout.Add(new Label("Quick Launch:") { style = { unityFontStyleAndWeight = FontStyle.Bold, marginTop = 5 }});
        var quickLaunchBox = new VisualElement() { style = { flexDirection = FlexDirection.Row } };
        quickLaunchBox.Add(new Button(() => LaunchApplication("explorer.exe")) { text = "Explorer" });
        quickLaunchBox.Add(new Button(() => LaunchApplication("mspaint.exe")) { text = "Paint" });
        foldout.Add(quickLaunchBox);
    }

    /// ## Section for running command-line interface tools
    private void CreateCliRunnerSection(VisualElement container)
    {
        var foldout = new Foldout() { text = "Command-Line Runner ⌨️", value = true };
        container.Add(foldout);

        var commandField = new TextField("Command:") { value = "git" };
        var argsField = new TextField("Arguments:") { value = "--version" };

        var runButton = new Button(() => RunCommand(commandField.value, argsField.value))
        {
            text = "Run Command"
        };

        _cliOutput = new TextField("Output:") { isReadOnly = true, multiline = true };
        _cliOutput.style.height = 120; // Give the output field some space
        _cliOutput.style.whiteSpace = WhiteSpace.Normal;

        foldout.Add(commandField);
        foldout.Add(argsField);
        foldout.Add(runButton);
        foldout.Add(_cliOutput);
    }
    
    #endregion

    #region Backend Logic

    /// Launches an external application using its file path.
    private void LaunchApplication(string path)
    {
        try
        {
            Process.Start(path);
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError($"Failed to launch '{path}'. Error: {ex.Message}");
            EditorUtility.DisplayDialog("Launch Error", $"Could not start the application at the specified path:\n\n{ex.Message}", "OK");
        }
    }

    /// Runs a command-line process and captures its output.
    private void RunCommand(string command, string arguments)
    {
        if (_cliOutput == null) return;
        _cliOutput.value = $"Running: {command} {arguments}\n\n"; // Clear previous output

        var process = new Process();
        var outputBuilder = new StringBuilder();

        process.StartInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // Use events to capture data asynchronously
        process.OutputDataReceived += (sender, args) => { if (args.Data != null) outputBuilder.AppendLine(args.Data); };
        process.ErrorDataReceived += (sender, args) => { if (args.Data != null) outputBuilder.AppendLine($"ERROR: {args.Data}"); };

        // When the process exits, update the UI.
        // This must be scheduled to run on the main thread.
        process.EnableRaisingEvents = true;
        process.Exited += (sender, args) =>
        {
            _cliOutput.schedule.Execute(() => _cliOutput.value += $"\nProcess exited with code {process.ExitCode}.");
        };

        try
        {
            process.Start();
            process.BeginOutputReadLine(); // Start asynchronous reading
            process.BeginErrorReadLine();

            // Update the text field with captured output
            // We use a delayed schedule to append the final text after events have fired
            rootVisualElement.schedule.Execute(() => _cliOutput.value += outputBuilder.ToString()).StartingIn(100);

        }
        catch (System.Exception ex)
        {
            _cliOutput.value += $"Failed to run command. Error: {ex.Message}";
            UnityEngine.Debug.LogError($"Failed to run command '{command}'. Error: {ex.Message}");
        }
    }
    #endregion
}