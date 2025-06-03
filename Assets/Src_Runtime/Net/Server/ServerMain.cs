using System;
using UnityEngine;
using Telepathy;
using System.Collections.Generic;
using Protocol;

namespace GameServer {
    public class ServerMain : MonoBehaviour {
        Server server;
        bool isTearDown;

        List<int> clients = new List<int>();

        void Awake() {
            // int port = 5555;
            int port = 12345; // 不知道为什么5555连不上
            int messageSize = 1024;

            server = new Server(messageSize);
            server.Start(port);
            Debug.Log("[Server] Listening on port " + port);

            server.OnConnected += (connId, str) => { // 有客户端连接
                Debug.Log("[Server] Connected " + connId);

                clients.Add(connId);
            };

            server.OnData += (connId, data) => { // 有数据到达
                Debug.Log("[server]Data " + connId + " " + data.Count);
                int typeID = MessageHelper.ReadHeader(data.Array);
                if (typeID == MessageConst.SpawnRole_Req) {
                    // SpawnRoleReqMessage
                    Debug.Log("[server]SpawnRole_Req " + connId);
                    SpawnRoleReqMessage req = MessageHelper.ReadData<SpawnRoleReqMessage>(data.Array);
                    OnSpawnRole(req);
                } else {
                    Debug.LogError("[server]Unknown typeID " + typeID);
                }
            };

            server.OnDisconnected += (connId) => { // 客户端断开连接
                Debug.Log("[Server] Disconnected " + connId);

                clients.Remove(connId);
            };

            Application.runInBackground = true;
        }

        void Start() {

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

        void OnSpawnRole(SpawnRoleReqMessage req) {
            for (int i = 0; i < clients.Count; i++) {
                int clientID = clients[i];
                // 广播给其他人
                SpawnRoleBroMessage bro = new SpawnRoleBroMessage();
                bro.id = req.id;
                bro.position = req.position;

                byte[] data = MessageHelper.ToData(bro);
                server.Send(clientID, data);
            }
        }
    }
}