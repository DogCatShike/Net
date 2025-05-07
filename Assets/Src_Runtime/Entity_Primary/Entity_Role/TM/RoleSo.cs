using System;
using UnityEngine;

namespace GameClient {
    [CreateAssetMenu(fileName = "So_Role", menuName = "Net/So_Role")]
    public class RoleSo : ScriptableObject {
        public RoleTM tm;
    }
}