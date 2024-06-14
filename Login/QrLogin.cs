using Dovis.Tools;
using Lagrange.Core.Common.Interface;
using Lagrange.Core.Common;
using Lagrange.Core;
using System.Text;
using Lagrange.Core.Common.Interface.Api;
using Dovis.Handle;

namespace Dovis.Login
{
    public class QrLogin
    {
        /// <summary>
        /// 执行二维码登录操作。
        /// </summary>
        public static async Task QrCodeLogin()
        {
            try
            {
                // 生成设备信息
                BotDeviceInfo deviceInfo = new()
                {
                    Guid = Guid.NewGuid(),
                    MacAddress = RandomTools.GenRandomBytes(6),
                    DeviceName = "Lagrange-52D02F",
                    SystemKernel = "Windows 10.0.19042",
                    KernelVersion = "10.0.19042.0"
                };

                // 创建密钥存储实例
                BotKeystore keyStore = new();

                // 创建机器人上下文实例
                BotContext bot = BotFactory.Create(new BotConfig()
                {
                    UseIPv6Network = false,
                    GetOptimumServer = true,
                    AutoReconnect = true,
                    Protocol = Protocols.Linux
                }, deviceInfo, keyStore);

                // 初始化事件监听
                await MessageHandle.InitEventListening(bot);

                // 获取二维码
                var qrCode = await bot.FetchQrCode();
                var qrCodeUrl = qrCode.Value.Url;

                // 生成并显示二维码
                StringBuilder qrCodeString = QrCodeTools.GetQrCode(qrCodeUrl);
                Console.WriteLine(qrCodeString.ToString());

                // 执行二维码登录
                await bot.LoginByQrCode();
                Console.WriteLine("登录成功！");
            }
            catch (Exception ex)
            {
                // 捕获并处理可能的异常
                Console.WriteLine($"二维码登录失败: {ex.Message}");
            }
        }
    }
}
