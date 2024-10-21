using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ST_AI_MusicShop2.Models;
using static ST_AI_MusicShop2.Models.Album;

namespace ST_AI_MusicShop2.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicShopDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AlbumsController(MusicShopDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Albums
        public async Task<IActionResult> Index(string albumType)
        {
            var albums = string.IsNullOrEmpty(albumType)
             ? await _context.Albums.ToListAsync()
             : _context.Albums.AsEnumerable().Where(a => a.GetType().Name == albumType).ToList();

            ViewBag.AlbumType = albumType; // Pass the album type to the view
            return View(albums);
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }


        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewBag.Sizes = Enum.GetValues(typeof(Size)).Cast<Size>();
            ViewBag.Editions = Enum.GetValues(typeof(Edition)).Cast<Edition>();
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string albumType, string title, string artist, decimal price, string description, IFormFile pictureFile, Size size, Edition edition, int? digitalPoints)
        {
            var album = AlbumFactory.CreateAlbum(albumType);
            album.Title = title;
            album.Artist = artist;
            album.Price = price;
            album.Description = description;

            if (album is CD cd)
            {
                cd.Size = size;
                cd.Edition = edition;
            }
            else if (album is Vinyl vinyl)
            {
                vinyl.Size = size;
                vinyl.Edition = edition;
            }
            else if (album is DigitalAlbum digitalAlbum)
            {
                digitalAlbum.DigitalScore = digitalPoints ?? 0;
            }


            if (pictureFile != null && pictureFile.Length > 0)
            {
                var fileName = Path.GetFileName(pictureFile.FileName);
                var path = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await pictureFile.CopyToAsync(stream);
                }
                album.PicturePath = "/images/" + fileName;
            }

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            ViewBag.Sizes = Enum.GetValues(typeof(Size)).Cast<Size>();
            ViewBag.Editions = Enum.GetValues(typeof(Edition)).Cast<Edition>();
            return View(album);
        }

        // POST: Albums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string title, string artist, decimal price, string description, IFormFile pictureFile, Size? size, Edition? edition, int? digitalPoints)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            album.Title = title;
            album.Artist = artist;
            album.Price = price;
            album.Description = description;
           

            if (album is CD cd)
            {
                cd.Size = size ?? cd.Size; // Retain existing size if not changed
                cd.Edition = edition ?? cd.Edition; // Retain existing edition if not changed
            }
            else if (album is Vinyl vinyl)
            {
                vinyl.Size = size ?? vinyl.Size; // Retain existing size if not changed
                vinyl.Edition = edition ?? vinyl.Edition; // Retain existing edition if not changed
            }
            else if (album is DigitalAlbum digitalAlbum)
            {
                digitalAlbum.DigitalScore = digitalPoints ?? digitalAlbum.DigitalScore; // Retain existing digital points if not changed
            }

            if (pictureFile != null && pictureFile.Length > 0)
            {
                var fileName = Path.GetFileName(pictureFile.FileName);
                var path = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await pictureFile.CopyToAsync(stream);
                }
                album.PicturePath = "/images/" + fileName;
            }

            _context.Update(album);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
