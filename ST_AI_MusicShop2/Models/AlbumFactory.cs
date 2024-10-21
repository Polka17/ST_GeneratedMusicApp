namespace ST_AI_MusicShop2.Models
{
    public class AlbumFactory
    {
        public static Album CreateAlbum(string albumType)
        {
            return albumType.ToLower() switch
            {
                "cd" => new CD(),
                "vinyl" => new Vinyl(),
                "digital" => new DigitalAlbum(),
                _ => throw new ArgumentException("Invalid album type")
            };
        }
    }
}
