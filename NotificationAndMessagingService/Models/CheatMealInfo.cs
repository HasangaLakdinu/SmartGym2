
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NotificationAndMessagingService.Models
{
  
        [Table("CheatMealInfo")]
        public class CheatMealInfo
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int CheatMealInfoId { get; set; }
            public string UserName { get; set; }
            public DateTime Date { get; set; }
            public string CheatMealName { get; set; }
            public double Weight { get; set; }
        }
    
}
