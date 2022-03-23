
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.DTOs;
using System.Threading.Tasks;
using System;
using Stack.DTOs.Enums;
using Stack.Entities.Models.Modules.Activities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Stack.DTOs.Requests.Modules.Pool;

namespace Stack.ServiceLayer.Modules.Zoom
{
    public class ZoomService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();
        static readonly char[] padding = { '=' };
        private static string apiKey = "SrplqtMHTFSgAoVIzdu8EQ";
        private static string apiSecret = "ONTFlbTo5OALdUyOKm1cVX2jFVinBWEZETCV";
        //private static string meetingNumber = "";
        //private static string role = "1";
        public ZoomService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;


        }

        public static long ToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000;
            return epoch;
        }

        public async Task<ApiResponse<string>> GetSignatrure(GetSignatureModel model)
        {
            ApiResponse<string> result = new ApiResponse<string>();
            try
            {
                return await Task.Run(() =>
                {
                    var ts = (ToTimestamp(DateTime.UtcNow.ToUniversalTime()) - 30000).ToString();
                    string message = String.Format("{0}{1}{2}{3}", apiKey, model.MeetingNumber, ts, model.Role);
                    apiSecret = apiSecret ?? "";
                    var encoding = new System.Text.ASCIIEncoding();
                    byte[] keyByte = encoding.GetBytes(apiSecret);
                    byte[] messageBytesTest = encoding.GetBytes(message);
                    string msgHashPreHmac = Convert.ToBase64String(messageBytesTest);
                    byte[] messageBytes = encoding.GetBytes(msgHashPreHmac);
                    using (var hmacsha256 = new HMACSHA256(keyByte))
                    {
                        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                        string msgHash = Convert.ToBase64String(hashmessage);
                        string token = String.Format("{0}.{1}.{2}.{3}.{4}", apiKey, model.MeetingNumber, ts, model.Role, msgHash);
                        var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
                        var signature = Convert.ToBase64String(tokenBytes).TrimEnd(padding);
                        if (signature != null)
                        {
                            result.Succeeded = true;
                            result.Data = signature;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unable to generate token");
                            return result;
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;

            }
        }
    }

}


