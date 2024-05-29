using System.ComponentModel.DataAnnotations;

namespace FinaFlow.Shared.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Titulo inválido")]
        [MaxLength(80, ErrorMessage = "O titulo deve conter no maximo 80 caracteres")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Descrição inválida")]
        public string Description { get; set; }
    }
}
