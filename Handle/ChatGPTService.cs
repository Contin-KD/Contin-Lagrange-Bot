using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class ChatGPTService
{
    private readonly string _apiUrl;
    private readonly string _apiKey;
    private readonly string _model;

    public ChatGPTService(string apiUrl, string apiKey, string model)
    {
        _apiUrl = apiUrl;
        _apiKey = apiKey;
        _model = model;
    }

    public async Task<string> SendMessageAsync(string prompt)
    {
        using var httpClient = new HttpClient();

        // 设置请求头
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        // 创建请求内容
        var requestBody = new
        {
            model = _model,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

        // 发送 POST 请求
        var response = await httpClient.PostAsync(_apiUrl, content);

        // 确保请求成功
        response.EnsureSuccessStatusCode();

        // 读取响应内容
        var responseContent = await response.Content.ReadAsStringAsync();
        // 打印回调
        Console.WriteLine(responseContent);
        // 解析 JSON
        var responseJson = JsonConvert.DeserializeObject<ChatGPTResponse>(responseContent);

        return responseJson.Choices[0].Message.Content;
    }

    private class ChatGPTResponse
    {
        public Choice[] Choices { get; set; }

        public class Choice
        {
            public Message Message { get; set; }
        }

        public class Message
        {
            public string Content { get; set; }
        }
    }
}