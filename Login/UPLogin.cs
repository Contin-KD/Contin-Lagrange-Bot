using Dovis.Tools;
using Lagrange.Core.Common.Interface;
using Lagrange.Core.Common;
using Lagrange.Core;
using Lagrange.Core.Common.Interface.Api;
using Lagrange.Core.Message;

namespace Dovis.Login
{
    internal class UPLogin
    {
        public static async Task UserPassLogin()
        {
            uint username = 1332870258;
            string password = "0714tg..";
            BotDeviceInfo _deviceInfo = new()
            {
                Guid = Guid.NewGuid(),
                MacAddress = RandomTools.GenRandomBytes(6),
                DeviceName = $"Lagrange-52D02F",
                SystemKernel = "Windows 10.0.19042",
                KernelVersion = "10.0.19042.0"
            };
            BotKeystore _keyStore = new BotKeystore();

            // 机器人实例
            BotContext bot = BotFactory.Create(new BotConfig()
            {
                UseIPv6Network = false,
                GetOptimumServer = true,
                AutoReconnect = true,
                Protocol = Protocols.Linux
            }, username, password,out _deviceInfo);

            // 检测是否登陆成功
            bot.Invoker.OnBotOnlineEvent += async (_, @event) =>
            {
                Console.WriteLine(@event.ToString());
                // 发送消息
                var friendChain = MessageBuilder.Friend(1826872946)
.Text("机器人登录成功");
                await bot.SendMessage(friendChain.Build());
            };
            await bot.LoginByPassword();

        }
    }
}
