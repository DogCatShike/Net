using System;
using UnityEngine;

namespace GameClient.System_Game {
    public class GameSystem {
        public SystemType systemType => SystemType.Game;

        GameSystemContext ctx;

        public GameSystem() {
            ctx = new GameSystemContext();
        }

        public void Inject(AssetModule assetModule) {
            ctx.Inject(assetModule);
        }

        public void Enter() {
            ctx.isRunning = true;

            RoleDomain.Spawn(ctx, 1);
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
            
        }

        void LastTick(float dt) {

        }

        public void Game_Binding() {

        }
    }
}