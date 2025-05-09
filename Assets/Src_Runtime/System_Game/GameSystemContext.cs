using System;
using UnityEngine;

namespace GameClient.System_Game {
    public class GameSystemContext {
        public bool isRunning;
        public GameEntity gameEntity;

        public AssetModule assetModule;
        public InputModule inputModule;

        public RoleRepo roleRepo;

        public GameSystemContext() {
            gameEntity = new GameEntity();
            roleRepo = new RoleRepo();
        }

        public void Inject(AssetModule assetModule, InputModule inputModule) {
            this.assetModule = assetModule;
            this.inputModule = inputModule;
        }
    }
}