using UnityEngine;

namespace Nidavellir.Scriptables
{
    /// <summary>
    /// This Scriptable Object can be used to create several virtual resources for your game. In instance it can be used for RTS games to reflect wood, stone or gold.
    /// It can also be used to reflect the stamina. health or mana of your player. It rather used for globally or singularly used resources. If you have multiple enemies
    /// and they refer to one resource it might not work as intended. For individual resource usage refer to the <see cref="ResourceController"/>  directly.
    /// </summary>
    [CreateAssetMenu(fileName = "Resource", menuName = "Resource", order = 0)]
    public class Resource : ScriptableObject
    {
        [SerializeField] private string m_name;
        [SerializeField] private string m_description;
        [SerializeField] private Sprite m_icon;
        [SerializeField] private ResourceData m_resourceData;

        private ResourceController m_resourceController;

        public ResourceController ResourceController => this.m_resourceController;
        public string Name => this.m_name;
        public string Description => this.m_description;
        public Sprite Icon => this.m_icon;

        private void Awake()
        {
            this.m_resourceController = new ResourceController(this.m_resourceData);
        }

        private void OnValidate()
        {
            this.m_resourceController = new ResourceController(this.m_resourceData);
        }
    }
}