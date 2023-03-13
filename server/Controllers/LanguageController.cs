﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.DataAccess.Entities;
using server.ViewModel.Languages;
using MySqlConnector;

namespace server.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/languages")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _languageService.GetAll();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var start = DateTime.Now;
            using var connection = new MySqlConnection("Server=34.87.73.59,3306;User ID=root;Password=root;Database=green-way-db");
            connection.Open();

            using var command = new MySqlCommand("SELECT * FROM Languages;", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1) + ", " + reader.GetString(2) +", " +reader.GetBoolean(3));
            }
            //var data = await _languageService.GetById(id);
            var end = DateTime.Now;
            var duration = (end- start).TotalSeconds.ToString();
            Console.WriteLine($"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA {duration}");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LanguageModel model)
        {
            //if (_languageService.AlreadyExist(model, out string message))
            //{
            //    return BadRequest(message);
            //}
            var data = await _languageService.Insert(new Language 
            {
                Name = model.Name,
                Code= model.Code,
                IsDefault =model.IsDefault
            });
            return Ok(data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id , LanguageModel model)
        {
            var entity = await _languageService.GetById(id);
            if(entity == null)
            {
                return BadRequest("Not found !");
            }

            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.IsDefault = model.IsDefault;
            await _languageService.Update(entity);            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _languageService.GetById(id);
            if (entity == null)
            {
                return BadRequest("Not found !");
            }
            if (_languageService.Delete(id))
                return Ok();
            else 
                return BadRequest();
        }
    }
}
