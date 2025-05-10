using System;

namespace GameClient {
    [Flags]
    public enum RoleState {
        Idle = 0,
        Move = 1 << 0,
        Jump = 1 << 1,
        Fall = 1 << 2,
        Climb = 1 << 3,
    }
}