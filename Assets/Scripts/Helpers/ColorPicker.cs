using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class ColorPicker : MonoBehaviour
    {
        public Color[] availableColors;
        public Button colorButtonPrefab;

        public Color SelectedColor { get; private set; }
        public System.Action<Color> OnColorChanged;

        List<Button> _mColorButtons = new List<Button>();

        // Start is called before the first frame update
        public void Init()
        {
            foreach (var color in availableColors)
            {
                var newButton = Instantiate(colorButtonPrefab, transform);
                newButton.GetComponent<Image>().color = color;

                newButton.onClick.AddListener(() =>
                {
                    SelectedColor = color;
                    foreach (var button in _mColorButtons)
                    {
                        button.interactable = true;
                    }

                    newButton.interactable = false;

                    OnColorChanged.Invoke(SelectedColor);
                });

                _mColorButtons.Add(newButton);
            }
        }

        public void SelectColor(Color color)
        {
            for (int i = 0; i < availableColors.Length; ++i)
            {
                if (availableColors[i] == color)
                {
                    _mColorButtons[i].onClick.Invoke();
                }
            }
        }
    }
}