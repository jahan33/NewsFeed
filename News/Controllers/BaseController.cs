
using DataAccess.Models;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using News.Common;
using Newtonsoft.Json;
//using Service.Contracts;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Controllers
{
    //[EnableCors("CorsPolicy")]
    //[Authorize]
    [JsonObject(IsReference = false)]
    // [AutoValidateAntiforgeryToken]
    [Route("api/[controller]/[action]")]

    public class BaseController : Controller
    {
        public List<KeyValuePair<string, StringValues>> list;
        public ILog log;
        public HttpContextAccessor context;
        public IConfigurationRoot _config;
        string AllowURLs = "";
       public string AccessKey = "";
       public string Issuer = "";
        public BaseController(HttpContextAccessor httpContextAccessor)

        {
            _config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            AllowURLs= _config.GetConnectionString("URL");
            AccessKey = _config.GetConnectionString("Key");
            Issuer = _config.GetConnectionString("Issuer");
            context = httpContextAccessor;
            try
            {
                isValidOrigin();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //////////////////// 
            //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            //   log = LogManager.GetLogger(typeof(BaseController));
        }
        public string ConvertToJson(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
        public string ConvertToJson(DataSet ds)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new CustomDataSetConverter());
            settings.Formatting = Formatting.Indented;

            string json = JsonConvert.SerializeObject(ds, settings);
            return json;
        }

          public static string GetJSONString(DataTable Dt)
        {

            StringBuilder Sb = new StringBuilder();

            string[] StrDc = new string[Dt.Columns.Count];
            string HeadStr = string.Empty;

            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);


            Sb.Append("{\"" + Dt.TableName + "\" : [");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                string TempStr = HeadStr;
                Sb.Append("{");

                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString());
                }
                Sb.Append(TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
            Sb.Append("]}");

            return Sb.ToString();
        }
        private bool isValidOrigin()
        {
            bool isValid = false;
            string URL = "";
            try
            {


                URL = getCurrentOrigin();


            }
            catch (Exception ex)
            {
                //var responseMsg = new HttpResponseMessage(HttpStatusCode.Forbidden);
                //responseMsg.Content = new StringContent("Invalid Client");
                //throw ex;

            }

            if (URL == null || URL.Length < 1)
            {
                var responseMsg = new HttpResponseMessage(HttpStatusCode.Forbidden);
                responseMsg.Content = new StringContent("Invalid Client");

                throw new Exception("Invalid Request");


            }
            else
            {
                isValid = true;
            }
            return isValid;
        }
        private string getCurrentOrigin()
        {

            try
            {
                string URL = null;

                // Microsoft.Extensions.Primitives.StringValues URL2 = "";//= context.HttpContext.Request.Headers["Origin"].ToString();
                //  context.HttpContext.Request.Headers.TryGetValue("Origin", out URL2);
                //  var scheme = context.HttpContext.Request.Scheme;
                 // URL=   context.HttpContext.Request.Headers["Referer"].FirstOrDefault();
             
                try
                {
                    URL = context.HttpContext.Request.Headers["Origin"].FirstOrDefault();
                }
                catch (Exception ex3)
                {
                    log.Fatal("URL", ex3);
                }
                if (URL == null)
                {
                    try
                    {
                        URL = context.HttpContext.Request.Headers["Referer"].FirstOrDefault();
                    }
                    catch (Exception ex3)
                    {
                        log.Fatal("URL", ex3);
                    }
                }
                if (URL == null)
                {
                    list = context.HttpContext.Request.Headers.ToList();
                    var b = list[8].Value;

                    URL = b.ToString();
                }
                // URL = abc.FirstOrDefault().ToString();
                URL = URL.Split('?')[0];
                if (URL != "/")
                    //  URL = URL.Replace(URL, "");
                    if (URL.Substring(URL.Length - 1, 1) == "/")
                    {
                        URL = URL.Remove(URL.Length - 1);
                    }
               
                string tempURL = null;
                string[] strOrg = AllowURLs.Split(",");
                foreach(string data in strOrg)
                {
                    tempURL = URL.Substring(0, data.Length);
                
                    if (data== tempURL)
                    {
                        tempURL = URL;
                        return URL;
                       

                    }
                   
                }
                return tempURL;


            }
            catch (Exception ex)
            {
                log.Fatal("Origin", ex);
                throw ex;
            }
        }

    }

}