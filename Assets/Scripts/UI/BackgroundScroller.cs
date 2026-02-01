using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private Image background;

        private void Update()
        {
            background.mainTexture.wrapMode = TextureWrapMode.Repeat;
            
        }
    }
}
