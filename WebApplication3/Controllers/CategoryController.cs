﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext context;

        public CategoryController(AppDbContext context)
        {
            this.context = context;
        }


        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();
            response.Entities = context.Categories.ToList();
            response.IsSuccessFul = true;
            return Ok(response);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();

            response.Entity = context.Categories.Find(id);
            if(response.Entity == null)
            {
                return NotFound();
            }
            response.IsSuccessFul = true;
            return Ok(response);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] CategoryModel model)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();


            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Title = model.Title,
                    Description = model.Description
                };

                context.Categories.Add(category);
                context.SaveChanges();

                return Ok(category);
            }
            else
            {
                response.Errors = ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => e.ErrorMessage).ToList();
                return BadRequest(response);
            }
               
            
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category model)
        {
            if(model==null|| model.Id != id)
            {
                return BadRequest();
            }
            var item = context.Categories.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Title = model.Title;
            item.Description = model.Description;
            item.ModifiedOn = DateTime.Now;

            context.Categories.Update(item);
            context.SaveChanges();

            return Ok(item);

        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = context.Categories.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            context.Categories.Remove(item);
            context.SaveChanges();

            return Ok(); 

        }
    }
}
