using static ST_AI_MusicShop2.Models.Album;

namespace ST_AI_MusicShop2.Models
{
    public class Vinyl: Album
    {
        public Size Size { get; set; }
        public Edition Edition { get; set; }
    }
}
