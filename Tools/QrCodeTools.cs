using Net.Codecrete.QrCodeGenerator;
using System.Text;

namespace Dovis.Tools
{
    public class QrCodeTools
    {
        /// <summary>
        /// 生成二维码字符串，用于控制台显示。
        /// </summary>
        /// <param name="url">需要编码为二维码的 URL。</param>
        /// <returns>生成的二维码字符串。</returns>
        public static StringBuilder GetQrCode(string url)
        {
            // 检查输入 URL 是否为空
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL 不能为空或只包含空白字符。", nameof(url));

            StringBuilder sb = new();
            QrCode qrCode = QrCode.EncodeText(url, QrCode.Ecc.Medium);
            int size = qrCode.Size;

            // 构建二维码的字符串表示
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    // 使用两个全角空格表示空白部分，以确保方块形状对称
                    sb.Append(qrCode.GetModule(x, y) ? "██" : "　");
                }
                sb.AppendLine();
            }

            return sb;
        }
    }
}
