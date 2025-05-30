using System;
using UnityEngine;
using Telepathy;

namespace GameServer {
    public class ServerMain : MonoBehaviour {
        Server server;
        bool isTearDown;

        void Awake() {
            int port = 5555;
            int messageSize = 1024;

            server = new Server(messageSize);
            server.Start(port);
            Debug.Log("[Server] Listening on port " + port);

            server.OnConnected += (connId, str) => { // 有客户端连接
                Debug.Log("[Server] Connected " + connId);
                server.Send(connId, new byte[] { 1, 2, 3 });
            };

            server.OnData += (connId, data) => { // 有数据到达

            };

            server.OnDisconnected += (connId) => { // 客户端断开连接

            };

            Application.runInBackground = true;
        }

        void Update() {
            if (server != null) {
                server.Tick(10);
            }
        }

        void OnDestroy() {

        }

        void OnApplicationQuit() {

        }

        void TearDown() {
            if (isTearDown) {
                return;
            }
            isTearDown = true;

            if (server != null) {
                server.Stop();
            }
        }
    }
}