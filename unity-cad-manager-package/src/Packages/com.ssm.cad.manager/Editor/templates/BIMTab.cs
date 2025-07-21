// Place in Assets/Editor/BIMTab.cs

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BIMTab : VisualElement
{
    private Label _idLabel;
    private Label _materialLabel;
    private Label _costLabel;
    private Label _supplierLabel;
    private Label _infoLabel;

    public BIMTab()
    {
        this.style.paddingLeft = 10;
        this.style.paddingTop = 10;

        _infoLabel = new Label("Select an object with a BIMData component to see its properties.")
        {
            style = { whiteSpace = WhiteSpace.Normal }
        };
        Add(_infoLabel);

        // Create labels to display data
        _idLabel = new Label();
        _materialLabel = new Label();
        _costLabel = new Label();
        _supplierLabel = new Label();

        Add(_idLabel);
        Add(_materialLabel);
        Add(_costLabel);
        Add(_supplierLabel);

        // Register for selection changes
        Selection.selectionChanged += OnSelectionChanged;
        
        // Initial check
        OnSelectionChanged();

        // Cleanup
        this.RegisterCallback<DetachFromPanelEvent>(evt => Selection.selectionChanged -= OnSelectionChanged);
    }

    private void OnSelectionChanged()
    {
        GameObject selectedObject = Selection.activeGameObject;
        if (selectedObject != null && selectedObject.TryGetComponent<BIMData>(out var data))
        {
            _infoLabel.style.display = DisplayStyle.None;
            _idLabel.text = $"Element ID: {data.ElementID}";
            _materialLabel.text = $"Material: {data.Material}";
            _costLabel.text = $"Cost: ${data.Cost}";
            _supplierLabel.text = $"Supplier: {data.Supplier}";
        }
        else
        {
            _infoLabel.style.display = DisplayStyle.Flex;
            _idLabel.text = "";
            _materialLabel.text = "";
            _costLabel.text = "";
            _supplierLabel.text = "";
        }
    }
}