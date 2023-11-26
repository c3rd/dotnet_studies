using Microsoft.AspNetCore.Mvc;
using TutorialProjectAPI.Data;
using TutorialProjectAPI.Models.Dto;

namespace TutorialProjectAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            return Ok(ProductStore.productList);
        }

        [HttpGet("{id:int}")]
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
        public ActionResult<ProductDTO> CreateProduct([FromBody]ProductDTO productDTO)
        {
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

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
