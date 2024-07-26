using Game.Core.Implementation;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Entities
{
    public sealed class PlayerHealthView : EntityComponent<PlayerHealthView>
    {
        [SerializeField] private TextMeshProUGUI _text;

        public TextMeshProUGUI Text => _text;
    }
}