using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TutorialProjectAPI.Data;
using TutorialProjectAPI.Models.Dto;
using System;

namespace TutorialProjectAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger) {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            Console.WriteLine("teste");
            _logger.LogInformation("Retrieving all products ae");
            return Ok(ProductStore.productList);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (productDTO == null)
            {
                return BadRequest();
            }

            if (productDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            productDTO.Id = ProductStore.productList.OrderByDescending(obj => obj.Id).FirstOrDefault().Id + 1;
            ProductStore.productList.Add(productDTO);

            return CreatedAtRoute("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var product = ProductStore.productList.FirstOrDefault(obj => obj.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ProductStore.productList.Remove(product);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, [FromBody]ProductDTO productDTO)
        {
            if (productDTO == null || id != productDTO.Id)
            {
                return BadRequest();
            }

            var product = ProductStore.productList.FirstOrDefault(obj => obj.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = productDTO.Name;
            product.Price = productDTO.Price;

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult UpdateParcialProduct(int id, JsonPatchDocument<ProductDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var product = ProductStore.productList.FirstOrDefault(obj => obj.Id == id);

            if (product == null) { return NotFound(); }

            patchDTO.ApplyTo(product, ModelState);

            if (!ModelState.IsValid) { return  BadRequest(ModelState); }

            return NoContent();
        }

    }
}
