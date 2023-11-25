﻿using Microsoft.AspNetCore.Mvc;
using TutorialProjectAPI.Data;
using TutorialProjectAPI.Models.Dto;

namespace TutorialProjectAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            return Ok(ProductStore.productList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            ProductDTO product = ProductStore.productList.FirstOrDefault(obj => obj.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}