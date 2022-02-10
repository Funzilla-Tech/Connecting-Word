using System;
using UnityEngine;

namespace Funzilla
{
    internal class Gameplay : Scene
    {
        private void Start()
        {
            SceneManager.Instance.HideLoading();
            SceneManager.Instance.HideSplash();
        }
    }
}
