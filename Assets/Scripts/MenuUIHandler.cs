using Helpers;
using UnityEngine;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker colorPicker;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
    }

    private void Start()
    {
        colorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        colorPicker.OnColorChanged += NewColorSelected;
    }
}