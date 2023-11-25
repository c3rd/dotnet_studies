using TutorialProjectAPI.Models.Dto;

namespace TutorialProjectAPI.Data
{
    public class ProductStore
    {
        public static List<ProductDTO> productList = new List<ProductDTO> {
                new ProductDTO { Id = 1, Name = "Produto 1", Price = 50.00 },
                new ProductDTO { Id = 2, Name = "Produto 1", Price = 50.00 }
            };
    }
}
