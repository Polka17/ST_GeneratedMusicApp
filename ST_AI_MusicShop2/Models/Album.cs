using System.ComponentModel.DataAnnotations;

namespace ST_AI_MusicShop2.Models
{
    public abstract class Album
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
    }
}
