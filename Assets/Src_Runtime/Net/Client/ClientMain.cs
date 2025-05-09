using System;
using GameClient.System_Game;
using UnityEngine;

namespace GameClient {
    public class ClientMain : MonoBehaviour {
        public static GameSystem gameSystem;

        public static AssetModule assetModule;
        public static InputModule inputModule;

        void Start() {
            gameSystem = new GameSystem();

            assetModule = GetComponentInChildren<AssetModule>();
            assetModule.Ctor();

            inputModule = GetComponentInChildren<InputModule>();
            inputModule.Ctor();

            // Inject
            gameSystem.Inject(assetModule, inputModule);

            Action action = async () => {
                await assetModule.LoadAll();

                gameSystem.Enter();
            };

            action.Invoke();
        }

        void Update() {
            float dt = Time.deltaTime;

            inputModule.Process();

            gameSystem.Tick(dt);
        }

        void OnApplicationQuit() {
            TearDown();
        }

        void OnDestroy() {
            TearDown();
        }

        void TearDown() {
            assetModule.UnLoadAll();
        }
    }
}