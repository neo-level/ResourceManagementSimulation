using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIMainScene : MonoBehaviour
    {
        public static UIMainScene Instance { get; private set; }

        public interface IUIInfoContent
        {
            string GetName();
            string GetData();
            void GetContent(ref List<Building.InventoryEntry> content);
        }

        public InfoPopup infoPopup;
        public ResourceDatabase resourceDB;

        protected IUIInfoContent MCurrentContent;
        protected List<Building.InventoryEntry> MContentBuffer = new List<Building.InventoryEntry>();


        private void Awake()
        {
            Instance = this;
            infoPopup.gameObject.SetActive(false);
            resourceDB.Init();
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            if (MCurrentContent == null)
                return;

            //This is not the most efficient, as we reconstruct everything every time. A more efficient way would check if
            //there was some change since last time (could be made through a IsDirty function in the interface) or smarter
            //update (match an entry content ta type and just update the count) but simplicity in this tutorial we do that
            //every time, this won't be a bottleneck here. 

            infoPopup.data.text = MCurrentContent.GetData();

            infoPopup.ClearContent();
            MContentBuffer.Clear();

            MCurrentContent.GetContent(ref MContentBuffer);
            foreach (var entry in MContentBuffer)
            {
                Sprite icon = null;
                if (resourceDB != null)
                    icon = resourceDB.GetItem(entry.resourceId)?.coneImage;

                infoPopup.AddToContent(entry.count, icon);
            }
        }

        public void SetNewInfoContent(IUIInfoContent content)
        {
            if (content == null)
            {
                infoPopup.gameObject.SetActive(false);
            }
            else
            {
                infoPopup.gameObject.SetActive(true);
                MCurrentContent = content;
                infoPopup.name.text = content.GetName();
            }
        }

        /// <summary>
        /// Loads the menu scene.
        /// </summary>
        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}