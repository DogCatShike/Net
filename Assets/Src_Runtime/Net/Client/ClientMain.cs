using System;
using GameClient.System_Game;
using UnityEngine;
using Telepathy;
using Protocol;

namespace GameClient {
    public class ClientMain : MonoBehaviour {
        Client client;
        bool isTearDown;

        public static GameSystem gameSystem;

        public static AssetModule assetModule;
        public static InputModule inputModule;

        void Awake() {
            // int port = 5555;
            int port = 12345; // 不知道为什么5555连不上
            int messageSize = 1024;
            // string ip = "120.27.13.194"; // 服务器地址
            string ip = "127.0.0.1"; // 回送地址

            client = new Client(messageSize);
            client.Connect(ip, port);

            client.OnConnected += () => {
                Debug.Log("[Client] Connected");
            };

            client.OnData += (data) => {
                int typeID = MessageHelper.ReadHeader(data.Array);
                if (typeID == MessageConst.SpawnRole_Bro) {
                    // SpawnRoleBroMessage
                    var msg = MessageHelper.ReadData<SpawnRoleBroMessage>(data.Array);
                    OnSpawn(msg);
                } else {
                    Debug.LogError("[client]Unknown typeID " + typeID);
                }
            };

            client.OnDisconnected += () => {
                Debug.Log("[Client] Disconnected");
            };

            Debug.Log("[Client] Connecting to " + ip + ":" + port);
            Application.targetFrameRate = 30;
            Application.runInBackground = true;
        }

        void Start() {
            gameSystem = new GameSystem();

            assetModule = GetComponentInChildren<AssetModule>();
            assetModule.Ctor();

            inputModule = GetComponentInChildren<InputModule>();
            inputModule.Ctor();

            // Inject
            gameSystem.Inject(assetModule, inputModule, client);

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

        void OnSpawn(SpawnRoleBroMessage msg) {
            Debug.Log("[Client] Spawn role: " + msg.id + " at " + msg.position[0] + ", " + msg.position[1]);
            RoleEntity role = RoleDomain.Spawn(gameSystem.ctx, msg.id);
            role.typeID = msg.id;
            role.transform.position = new Vector3(msg.position[0], msg.position[1], 0);
        }
    }
}