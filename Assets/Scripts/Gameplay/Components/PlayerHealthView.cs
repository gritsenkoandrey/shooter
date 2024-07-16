using Core.Implementation;
using TMPro;
using UnityEngine;

namespace Gameplay.Components
{
    public sealed class PlayerHealthView : EntityComponent<PlayerHealthView>
    {
        [SerializeField] private TextMeshProUGUI _text;

        public TextMeshProUGUI Text => _text;
    }
}