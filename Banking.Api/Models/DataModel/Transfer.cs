using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Api.Models.DataModel
{
    public class Transfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int FromCustomerId { get; set; }
        public int ToCustomerId { get; set; }
        
        public int ToAccountId { get; set; }
        public int FromAccountId { get; set; }

        public decimal Amount { get; set; }
    }
}