using System;
using GameClient.System_Game;
using UnityEngine;
using Telepathy;

namespace GameClient {
    public class ClientMain : MonoBehaviour {
        Client client;
        bool isTearDown;

        public static GameSystem gameSystem;

        public static AssetModule assetModule;
        public static InputModule inputModule;

        void Awake() {
            int port = 12345;
            int messageSize = 1024;
            string ip = "127.0.0.1";

            client = new Client(messageSize);
            client.Connect(ip, port);

            client.OnConnected += () => {
                Debug.Log("[Client] Connected");
            };

            client.OnData += (data) => {
                Debug.Log("[Client] data: " + data.Count + " bytes");
            };

            client.OnDisconnected += () => {
                Debug.Log("[Client] Disconnected");
            };

            Debug.Log("[Client] Connecting to " + ip + ":" + port);
            Application.runInBackground = true;
        }

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
            if (client != null) {
                client.Tick(10);
            }

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
            if (isTearDown) {
                return;
            }
            isTearDown = true;

            assetModule.UnLoadAll();

            if (client != null) { 
                client.Disconnect();
            }
        }
    }
}