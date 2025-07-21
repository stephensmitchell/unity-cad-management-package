using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UpdateCheckWindow : EditorWindow
{
    // Note the use of ShowModal() instead of GetWindow<T>()
    public static void ShowModalWindow()
    {
        // CreateInstance is needed because modal windows have a different lifecycle
        UpdateCheckWindow wnd = CreateInstance<UpdateCheckWindow>();
        wnd.titleContent = new GUIContent("Update Check");

        // Prevent the window from being docked
        wnd.ShowModal();
    }

    public void CreateGUI()
    {
        rootVisualElement.style.paddingTop = 15;
        rootVisualElement.style.alignItems = Align.Center; // Center content

        var infoLabel = new UnityEngine.UIElements.Label("You are running the latest version of the plugin.")
        {
            style = { fontSize = 14 }
        };
        rootVisualElement.Add(infoLabel);

        var okButton = new Button(() => this.Close()) // The button simply closes this window
        {
            text = "OK",
            style = { width = 100, marginTop = 20 }
        };
        rootVisualElement.Add(okButton);

        // A modal window should have a fixed size and not be resizable.
        this.maxSize = this.minSize = new Vector2(350, 100);
    }
}