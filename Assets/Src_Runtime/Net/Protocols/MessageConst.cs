namespace Protocol {
    // Request(Req) : 客户端 发-> 服务端
    // Response(Res): 服务端 单发-> 客户端
    // Broadcast(Bro): 服务端 广播-> 客户端
    public static class MessageConst {
        public const int SpawnRole_Req = 10;
        public const int SpawnRole_Bro = 12;
    }
}