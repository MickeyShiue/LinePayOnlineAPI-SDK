using System;
using System.Security.Cryptography;

namespace LinePayEC
{
    internal static class LinePayClientHelper
    {
        public static void HttpHeaderValueValid(string channelId, string uuid, string signature)
        {
            if (string.IsNullOrEmpty(channelId))
            {
                throw new ArgumentNullException(nameof(channelId));
            }

            if (string.IsNullOrEmpty(uuid))
            {
                throw new ArgumentNullException(nameof(uuid));
            }

            if (string.IsNullOrEmpty(signature))
            {
                throw new ArgumentNullException(nameof(signature));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">( ChannelSecret + apiUrl + requestJson + nonce)</param>
        /// <param name="key">ChannelSecret</param>
        /// <returns></returns>
        public static string Encrypt(string message, string key)
        {
            var encoding = new System.Text.UTF8Encoding();
            var keyByte = encoding.GetBytes(key);
            var messageBytes = encoding.GetBytes(message);
            using (var hmac256 = new HMACSHA256(keyByte))
            {
                var hashMessage = hmac256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashMessage);
            }
        }
    }
}