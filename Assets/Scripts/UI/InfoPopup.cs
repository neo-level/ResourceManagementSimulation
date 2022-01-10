using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InfoPopup : MonoBehaviour
    {
        public new Text name;
        public Text data;
        public RectTransform contentTransform;

        public ContentEntry entryPrefab;

        public void ClearContent()
        {
            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }
        }
    
        public void AddToContent(int count, Sprite coneImage)
        {
            var newEntry = Instantiate(entryPrefab, contentTransform);

            newEntry.count.text = count.ToString();
            newEntry.coneImage.sprite = coneImage;
        }
    }
}
