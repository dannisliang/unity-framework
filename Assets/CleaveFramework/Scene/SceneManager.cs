﻿using System;
using CleaveFramework.Core;
using UnityEngine;

namespace CleaveFramework.Scene
{

    public sealed class SceneManager
    {

        public SceneManager()
        {
            Command.Register(typeof(ChangeSceneCmd), OnChangeScene);
            Command.Register(typeof(SceneLoadedCmd), OnSceneLoaded);
        }

        void OnChangeScene(Command cmd)
        {
            var cCmd = (ChangeSceneCmd)cmd;
            // enter transition scene
            UnityEngine.Application.LoadLevel(Framework.TransitionScene);
            // load the new scene
            UnityEngine.Application.LoadLevelAsync(cCmd.SceneName);
        }

        void OnSceneLoaded(Command cmd)
        {
            var cCmd = (SceneLoadedCmd) cmd;

            cCmd.View.Initialize();

            Framework.PushCommand(new SceneInitializedCmd());
        }

        public static void CreateSceneView(string viewName)
        {
            var sceneViewObject = new GameObject();
            var sceneViewComponent = sceneViewObject.AddComponent(viewName) as SceneView;

            Framework.PushCommand(new SceneLoadedCmd(sceneViewComponent));
        }

    }

}