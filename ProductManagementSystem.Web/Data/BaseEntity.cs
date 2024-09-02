using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Web.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }       
    }
}
