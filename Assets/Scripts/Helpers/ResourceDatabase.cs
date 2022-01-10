using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    [CreateAssetMenu(fileName = "ResourcesDatabase", menuName = "Tutorial/Resources Database")]
    public class ResourceDatabase : ScriptableObject
    {
        public List<ResourceItem> resourceTypes = new List<ResourceItem>();

        private Dictionary<string, ResourceItem> _mDatabase;

        public void Init()
        {
            _mDatabase = new Dictionary<string, ResourceItem>();
            foreach (var resourceItem in resourceTypes)
            {
                _mDatabase.Add(resourceItem.id, resourceItem);
            }
        }

        public ResourceItem GetItem(string uniqueId)
        {
            _mDatabase.TryGetValue(uniqueId, out var type);
            return type;
        }
    }
}