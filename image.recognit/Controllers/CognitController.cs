﻿using Dapper;
using image.recognit.Models;
using image.recognit.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace image.recognit.Controllers
{
    [Route("api/cognit")]
    [ApiController]
    public class CognitController : ControllerBase
    {
        private RecognitService _recognit;
        private IWebHostEnvironment _appEnvironment;
        private readonly CacheService _cacheService;
        public CognitController(RecognitService recognitService, IWebHostEnvironment appEnvironment, CacheService cacheService)
        {
            _recognit = recognitService;
            _appEnvironment = appEnvironment;
            _cacheService = cacheService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var data = new
            {
                message = "Hello GET Request!"
            };
            return new JsonResult(data);
        }



        [HttpPost]
        public async Task<JsonResult> Post(IFormFile image)
        {
            if (image != null)
            {
                try
                {
                    // путь к папке Files
                    string path = "/files/" + image.FileName;
                    try
                    {
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("file exception,  " + ex.Message);
                    }

                    string result;
                    try
                    {
                        result = _recognit.Recognit(_appEnvironment.WebRootPath + path);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("cognit service, " + ex.Message);
                    }
                    

                    var arr = result.Split(" ");

                    IEnumerable<Search> data = _cacheService.GetSearch();
                    if (data == null)
                    {
                        try
                        {
                            using (var conn = new SqlConnection("Data Source=185.234.114.117;Initial Catalog=db_a8b736_geo2023;User Id=geoadmin;Password=12357Geoid@;MultipleActiveResultSets=true;Encrypt=False;"))
                            {
                                await conn.OpenAsync();
                                string sqlP = "select * from Searches";
                                data = await conn.QueryAsync<Search>(sqlP);
                                _cacheService.AddClick(data);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("sql exceptions , " + ex.Message);
                        }
                    }

                    var len = arr.Length;
                    string texts = "";

                    for (int i = 0; i < len / 2; i++)
                    {
                        data = data.Where(p => p.Name.Contains(arr[i])).ToList();
                        texts = texts + arr[i] + " ";
                    }

                    SearchResult sr = new SearchResult();
                    sr.searchtext = texts;
                    sr.Searches = data;

                    return new JsonResult(sr);
                }
                catch(Exception ex)
                {
                   return new JsonResult(BadRequest(ex));
                }
            }
            return new JsonResult(NoContent());
        }
    }
}
