using System;
using UnityEngine;
using Protocol;
using Telepathy;

namespace GameClient.System_Game {
    public class GameSystem {
        public SystemType systemType => SystemType.Game;

        public GameSystemContext ctx;

        const float GRAVITY = 9.8f;

        public GameSystem() {
            ctx = new GameSystemContext();
        }

        public void Inject(AssetModule assetModule, InputModule inputModule, Client client) {
            ctx.Inject(assetModule, inputModule, client);
        }

        public void Enter() {
            ctx.isRunning = true;

            // RoleDomain.Spawn(ctx, 1);
            SpawnRoleReqMessage msg = new SpawnRoleReqMessage();
            msg.id = 1;
            msg.position = new float[2] { 0, 0 };

            byte[] data = MessageHelper.ToData(msg);
            ctx.client.Send(data);
        }

        public void Exit() {
            ctx.isRunning = false;
        }

        public void Tick(float dt) {
            if (!ctx.isRunning) {
                return;
            }

            var game = ctx.gameEntity;

            PreTick(dt);

            ref float restFixTime = ref game.restFixTime;

            restFixTime += dt;

            const float FIX_INTERVAL = 0.020f;

            if (restFixTime <= FIX_INTERVAL) {
                LogicTick(restFixTime);

                restFixTime = 0;
            } else {
                while (restFixTime >= FIX_INTERVAL) {
                    LogicTick(FIX_INTERVAL);
                    restFixTime -= FIX_INTERVAL;
                }
            }

            LastTick(dt);
        }

        void PreTick(float dt) {

        }

        void LogicTick(float dt) {
            int len = ctx.roleRepo.TakeAll(out RoleEntity[] roles);
            for (int i = 0; i < len; i++) {
                var role = roles[i];
                RoleDomain.Input_Record(ctx, role);
                RoleDomain.Loco_Any_Execute(role, dt, GRAVITY);
            }
        }

        void LastTick(float dt) {

        }

        public void Game_Binding() {

        }
    }
}