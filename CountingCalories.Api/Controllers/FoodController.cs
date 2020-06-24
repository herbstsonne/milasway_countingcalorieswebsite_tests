﻿using System.Collections.Generic;
using System.Linq;
using CountingCalories.UI.Models;
using EFGetStarted;
using Microsoft.AspNetCore.Mvc;

namespace CountingCalories.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private CountingCaloriesContext _db;

        public FoodController(CountingCaloriesContext db)
        {
            _db = db;
        }

        // GET: api/<FoodController>
        [HttpGet]
        public IEnumerable<Food> Get()
        {
            var allFood = from f in _db.Food select f;
            return allFood;
        }

        // GET api/<FoodController>/5
        [HttpGet("{name}")]
        public Food Get(string name)
        {
            return _db.Food.FirstOrDefault(f => f.Name == name);
        }

        // POST api/<FoodController>
        [HttpPost]
        public void Post(Food food)
        {
            _db.Food.Add(food);
            _db.SaveChanges();
        }

        // PUT api/<FoodController>/5
        [HttpPut("{id}")]
        public void Put(int id, Food food)
        {
        }

        // DELETE api/<FoodController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}