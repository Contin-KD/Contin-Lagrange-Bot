using Lagrange.Core;
using Lagrange.Core.Common.Interface.Api;
using Lagrange.Core.Event.EventArg;
using Lagrange.Core.Message;
using Lagrange.Core.Message.Entity;

namespace Dovis.Handle
{
    internal class MessageHandle
    {
        /// <summary>
        /// 初始化事件监听，包括机器人上线和收到群消息的处理。
        /// </summary>
        /// <param name="bot">机器人上下文实例。</param>
        public static async Task InitEventListening(BotContext bot)
        {
            // 订阅机器人上线事件
            bot.Invoker.OnBotOnlineEvent += async (_, @event) =>
            {
                Console.WriteLine("机器人已上线: " + @event.ToString());

                // 发送通知消息
                var friendMessage = MessageBuilder.Friend(1826872946)
                                                  .Text("机器人登录成功");
                await bot.SendMessage(friendMessage.Build());
            };

            // 订阅群消息接收事件
            bot.Invoker.OnGroupMessageReceived += async (_, @event) =>
            {
                // 提取消息文本内容
                string message = @event.Chain
                    .OfType<TextEntity>()
                    .FirstOrDefault()?.Text ?? string.Empty;

                Console.WriteLine(@event.Chain.TargetUin);
               Console.WriteLine($"收到消息: 类型={@event.Chain.Type}, 群组={@event.Chain.GroupMemberInfo.Uin}, 内容={message}, 时间={@event.EventTime}");

                // 处理特定的消息内容
                await HandleSpecialMessages(bot, @event, message);
            };
        }

        /// <summary>
        /// 处理特定文本内容的群消息。
        /// </summary>
        /// <param name="bot">机器人上下文实例。</param>
        /// <param name="event">群消息事件。</param>
        /// <param name="message">消息文本内容。</param>
        private static async Task HandleSpecialMessages(BotContext bot, GroupMessageEvent @event, string message)
        {
            switch (message)
            {
                case "美女图片":
                    await APIHandle.GetBeautyImage(bot, @event);
                    break;
                case "看看黑丝":
                    await APIHandle.GetBlackSilkImage(bot, @event);
                    break;
                default:
                    // 可以在这里添加更多的消息处理
                    break;
            }
        }
    }
}
