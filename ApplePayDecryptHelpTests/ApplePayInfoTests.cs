﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplePayDecryptHelp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ApplePayDecryptHelp.Tests
{
    [TestClass()]
    public class ApplePayInfoTests
    {
        private string dataFromJson =
                "IX+6/ypLOaBt9zPqXDvtxqbdYfoKHoJuNbIUV+NiPvLUV3do8sr8CDHNGDd5kXbSisYT/9Wcj55sX4JyRf/O1DM4TmvmuhUjCgDOjxuPbRILTY3J3aHoU4FGfRnmEp6KPVVGeYsb6ZdFk8kNeGH4nBKItBMgIrVwBgkkvTn3dt3hcDFr+s+hC6j/o4FajeKeQb/20141XVo8EG5LF1bZWcXTccOMpKpPDaCsEWxnhslcbJig/ANNCrQHFFi348/kjB5AwrMQW2Npk4krARuysVinTXtma0+D6XurbvFL+8lExxAc4f2sRaNUn8sIMbipKh56TYtQAo4xfSQxqwcdxrV7TiJdXAydTm8e6rnB491vRiHuTwSNLW64QooNEOtBQLPbktidWsXyGnm7QFAjCE2y+BhVHrzojBHgWxWNqxqkDqjWXcmnlLg=";
        
        private string base64Pubkey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEW23BB1xHIc5SwZ3d36lr8olhbbNKlXjrWQQxYFU5LJ9mi/YTxAPfWZBKw7B41HWzMzkOmKk2MQGGVyetwLUJbQ==";

        private string certPath = "API.p12";// @"C:\Users\jimmy\Desktop\API.p12";
        private string certPassWord = "";

        private string merchantIdentifierField = "67-E6-B8-51-AA-6C-20-8B-31-03-05-09-B2-19-03-79-95-93-D4-62-B2-46-7B-10-F1-E1-BD-41-E7-95-C3-AF";
        private string sharedSecret = "5F-48-03-B6-1F-25-25-35-E8-49-26-F4-71-CF-6C-D3-10-78-68-2D-6B-46-A7-AD-17-81-29-AF-61-E0-53-FE";
        private string symmetricKey = "74-43-16-39-61-58-E2-A3-F2-6B-E6-79-CC-2F-35-DC-75-6E-57-9D-32-3C-C8-A7-9A-CB-C8-D0-08-2D-B6-F0";
        private string iv = "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00";

        [TestMethod()]
        public void InitTest()
        {
            var encryptedData = Convert.FromBase64String(dataFromJson);

            var applePayInfo = new ApplePayInfo().Init(base64Pubkey, certPath, certPassWord);
            
            Assert.IsInstanceOfType(applePayInfo, typeof(ApplePayInfo));
            Assert.AreEqual(merchantIdentifierField, BitConverter.ToString(applePayInfo.merchantIdentifierField));
            Assert.AreEqual(sharedSecret, BitConverter.ToString(applePayInfo.sharedSecret));
            Assert.AreEqual(symmetricKey, BitConverter.ToString(applePayInfo.symmetricKey));
            Assert.AreEqual(iv, BitConverter.ToString(applePayInfo.Iv));
        }

        [TestMethod()]
        public void DecryptedByAES256GCMTest()
        {
            var encryptedData = Convert.FromBase64String(dataFromJson);

            var applePayInfo = new ApplePayInfo().Init(base64Pubkey, certPath, certPassWord);

            var jsonStr = applePayInfo.DecryptedByAES256GCM(dataFromJson);
            Assert.IsFalse(string.IsNullOrEmpty(jsonStr));
        }
    }
}
