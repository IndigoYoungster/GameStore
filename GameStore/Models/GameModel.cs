using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class GameModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int Price { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
