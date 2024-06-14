using Dovis.Handle;
using Dovis.Login;
using Newtonsoft.Json;
using static Common.CommonData;

namespace Dovis.Main
{
    class Program
    {
        static async Task Main(string[] args) // Main 方法改为 async Task 以支持异步处理
        {
            //// 指定文件路径
            //string currentDir = Directory.GetCurrentDirectory();
            //string filePath = Path.Combine(currentDir, "appsettings.json");
            //Console.WriteLine(filePath);

            //// 读取文件内容
            //string jsonContent = File.ReadAllText(filePath);

            //// 解析 JSON 内容
            //var appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonContent);

            //// 读取配置项
            //string apiKey = appSettings.OpenAI.ApiKey;

            //// 输出结果
            //Console.WriteLine($"Loaded API Key: {apiKey}");

            //ChatGPTService aIHandle = new ChatGPTService(
            //    appSettings.OpenAI.ApiAddress,
            //    appSettings.OpenAI.ApiKey,
            //    appSettings.OpenAI.ApiModel
            //);



            //string userPrompt = "你好";
            //string response = await aIHandle.SendMessageAsync(userPrompt);

            // 输出响应
            // Console.WriteLine($"ChatGPT: {response}");

            // 继续你的程序逻辑
            await PrintMenuAsync(string.Empty);
        }

        /// <summary>
        /// 返回带有艺术字符的欢迎信息。
        /// </summary>
        private static string GetArtFront()
        {
            return @"
                                                              ,---,                                 
                                                           ,`--.' |                                 
            ,---,                                          |   :  :    ,---,.               ___     
          .'  .' `\                        ,--,            |   |  '  ,'  .'  \            ,--.'|_   
        ,---.'     \    ,---.            ,--.'|            '   :  |,---.' .' |   ,---.    |  | :,'  
        |   |  .`\  |  '   ,'\      .---.|  |,      .--.--.;   |.' |   |  |: |  '   ,'\   :  : ' :  
        :   : |  '  | /   /   |   /.  ./|`--'_     /  /    '---'   :   :  :  / /   /   |.;__,'  /   
        |   ' '  ;  :.   ; ,. : .-' . ' |,' ,'|   |  :  /`./       :   |    ; .   ; ,. :|  |   |    
        '   | ;  .  |'   | |: :/___/ \: |'  | |   |  :  ;_         |   :     \'   | |: ::__,'| :    
        |   | :  |  ''   | .; :.   \  ' .|  | :    \  \    `.      |   |   . |'   | .; :  '  : |__  
        '   : | /  ; |   :    | \   \   ''  : |__   `----.   \     '   :  '; ||   :    |  |  | '.'| 
        |   | '` ,/   \   \  /   \   \   |  | '.'| /  /`--'  /     |   |  | ;  \   \  /   ;  :    ; 
        ;   :  .'      `----'     \   \ |;  :    ;'--'.     /      |   :   /    `----'    |  ,   /  
        |   ,.'                    '---"" |  ,   /   `--'---'       |   | ,'                ---`-'   
        '---'                             ---`-'                   `----'                           
                                                                                           Dovis'Bot
";
        }

        /// <summary>
        /// 显示主菜单，并处理用户选择。
        /// </summary>
        /// <param name="tips">提示信息。</param>
        public static async Task PrintMenuAsync(string tips)
        {
            Console.Clear();
            Console.WriteLine(GetArtFront());
            Console.WriteLine(tips);
            Console.WriteLine("请选择登录方式:");
            Console.WriteLine();
            Console.WriteLine("1. 扫码登录");
            Console.WriteLine("2. 账号密码登录");
            Console.WriteLine();
            Console.WriteLine("扫码登录二维码:");
            Console.WriteLine();
            Console.Write("请输入你的选择 (1 或 2): ");
            await HandleUserInputAsync();
        }

        /// <summary>
        /// 处理用户输入，并根据选择执行对应的登录操作。
        /// </summary>
        public static async Task HandleUserInputAsync()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("您选择了扫码登录。请使用移动设备扫描下方的二维码进行登录。");
                    try
                    {
                        await QrLogin.QrCodeLogin();
                        Console.WriteLine("扫码登录成功！");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"扫码登录失败: {ex.Message}");
                    }
                    break;
                case "2":
                    Console.WriteLine("您选择了账号密码登录。请输入您的账号和密码。");
                    try
                    {
                        await UPLogin.UserPassLogin();
                        Console.WriteLine("账号密码登录成功！");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"账号密码登录失败: {ex.Message}");
                    }
                    break;
                default:
                    Console.WriteLine("无效的选择，请重新输入 1 或 2。");
                    await HandleUserInputAsync();
                    break;
            }
        }
    }

}
