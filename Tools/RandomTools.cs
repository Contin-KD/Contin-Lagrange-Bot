namespace Dovis.Tools
{
    internal class RandomTools
    {
        /// <summary>
        /// 随机设备地址
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GenRandomBytes(int length)
        {
            var bytes = new byte[length];

            //Random.Shared.NextBytes(bytes);
            return new byte[] { 1, 1, 1, 1, 1, 1 };
        }
    }
}
