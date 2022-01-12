using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocketLib
{
    public class SocketsMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ISocketHandler handler;

        public SocketsMiddleware(RequestDelegate next, ISocketHandler handler)
        {
            this.next = next;
            this.handler = handler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                // 转换当前连接为一个 ws 连接
                var socket = await context.WebSockets.AcceptWebSocketAsync();
                var key = context.Request.Query["key"];
                if (string.IsNullOrWhiteSpace(key))
                {
                    context.Response.StatusCode = 400;
                    return;
                }
                await handler.OnConnected(key, socket);

                // 接收消息的 buffer
                var buffer = new byte[1024 * 4];
                // 判断连接类型，并执行相应操作
                while (socket.State == WebSocketState.Open)
                {
                    // 这句执行之后，buffer 就是接收到的消息体，可以根据需要进行转换。
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            await handler.Receive(socket, result, buffer);
                            break;
                        case WebSocketMessageType.Close:
                            await handler.OnDisconnected(socket);
                            break;
                        case WebSocketMessageType.Binary:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
