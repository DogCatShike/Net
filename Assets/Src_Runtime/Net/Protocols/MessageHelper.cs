using System;
using System.Collections.Generic;
using UnityEngine;

namespace Protocol {
    public static class MessageHelper {
        static Dictionary<Type, int> typeIDMap = new Dictionary<Type, int>() {
            { typeof(SpawnRoleReqMessage), MessageConst.SpawnRole_Req },
            { typeof(SpawnRoleBroMessage), MessageConst.SpawnRole_Bro },
        };
    }
}