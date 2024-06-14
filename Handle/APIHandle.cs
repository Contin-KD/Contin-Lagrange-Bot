using Lagrange.Core;
using Lagrange.Core.Common.Interface.Api;
using Lagrange.Core.Event.EventArg;
using Lagrange.Core.Message;

namespace Dovis.Handle
{
    public static class APIHandle
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        /// 获取并发送美女图片。
        /// </summary>
        /// <param name="bot">机器人上下文实例。</param>
        /// <param name="event">群消息事件。</param>
        /// <returns>任务对象。</returns>
        public static Task GetBeautyImage(BotContext bot, GroupMessageEvent @event)
        {
            const string apiUrl = "http://api.mhimg.cn/api/girls_img?type=img";
            return FetchAndSendImage(apiUrl, bot, @event);
        }

        /// <summary>
        /// 获取并发送黑丝图片。
        /// </summary>
        /// <param name="bot">机器人上下文实例。</param>
        /// <param name="event">群消息事件。</param>
        /// <returns>任务对象。</returns>
        public static Task GetBlackSilkImage(BotContext bot, GroupMessageEvent @event)
        {
            const string apiUrl = "http://api.mhimg.cn/api/heisi_img?type=img";
            return FetchAndSendImage(apiUrl, bot, @event);
        }

        /// <summary>
        /// 从指定的 API 获取图片并发送到群组。
        /// </summary>
        /// <param name="apiUrl">图片 API 的 URL。</param>
        /// <param name="bot">机器人上下文实例。</param>
        /// <param name="event">群消息事件。</param>
        /// <returns>任务对象。</returns>
        private static async Task FetchAndSendImage(string apiUrl, BotContext bot, GroupMessageEvent @event)
        {
            try
            {
                Console.WriteLine("请求图片: " + apiUrl);

                // 发出 GET 请求获取图片数据
                HttpResponseMessage response = await HttpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // 读取图片数据为字节数组
                Console.WriteLine("转换图片数据为字节数组");
                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                // 构建并发送消息链
                Console.WriteLine("发送图片");
                var groupChain = MessageBuilder.Group((uint)@event.Chain.GroupUin)
                                               .Image(imageBytes); // 传递字节数组
                await bot.SendMessage(groupChain.Build());
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine("HTTP 请求错误: " + httpEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送图片时发生错误: " + ex.Message);
            }
        }
    }
}
