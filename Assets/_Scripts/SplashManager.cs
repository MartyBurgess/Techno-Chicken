using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoMossStudios.Utilities;
using System;
using DG.Tweening;

namespace TechnoChicken
{
    public class SplashManager : Singleton<SplashManager>
    {
        bool playfabLoggedIn = false;
        bool splashAnimFinished = false;
        bool loadingGameScene = false;

        [SerializeField] DOTweenAnimation splashAnim;

        private void Start()
        {
            if (PlayFabManager.InstanceExists)
            {
                if (PlayFabManager.IsLoggedIn)
                {
                    playfabLoggedIn = true;
                }
                else
                    PlayFabManager.Instance.OnPlayFabLogin.AddListener(() => { playfabLoggedIn = true; });
            }
            else
                Debug.LogError("Error - cannot find playfabmanager");
        }

        public void HandleSplashAnimComplete()
        {
            splashAnimFinished = true;
        }

        private void Update()
        {
            if (!playfabLoggedIn) return;

            if (!splashAnimFinished) return;

            if (loadingGameScene) return;

            if (SceneTransitionManager.InstanceExists)
            {
                loadingGameScene = true;
                SceneTransitionManager.Instance.TransitionToScene(1);
            }
            else
                Debug.LogError("Error - cannot find scene transition manager");
        }
    }
}
