using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmApp.Models.Entity;
using FilmApp.Models.ViewModel;
using FilmApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmApp.Controllers
{

    public class MovieController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _appEnvironment;

        /// <summary>
        /// Controller Constructor.
        /// </summary>
        /// <param name="uow">Unit Of Work</param>
        /// <param name="appEnvironment">Hosting information</param>
        public MovieController(IUnitOfWork uow, IWebHostEnvironment appEnvironment)
        {
            _uow = uow;
            _appEnvironment = appEnvironment;
        }


        /// <summary>
        /// Main action that loads all films with related tables and send it view
        /// </summary>
        /// <returns>return IActionResult as response for the request</returns>
        public IActionResult Index()
        {
            var films = _uow.FilmRepository.Get().Include(a => a.FilmGenres).ThenInclude(a => a.Genre);
            var genres = _uow.GenreRepository.Get();
            var homeViewModel = new HomeViewModel { FilmViewModels = films, GenreViewModels = genres };
            return View(homeViewModel);
        }


        /// <summary>
        /// This action filter films based on filter value
        /// </summary>
        /// <param name="search">Filter value</param>
        /// <returns>return IActionResult as response for the request</returns>
        public IActionResult FilterFilm(string search)
        {
            var isValidFilter = int.TryParse(search, out var searchValue);
            IQueryable<Film> films;
            if (isValidFilter)
            {
                films = _uow.FilmRepository.Get()
                    .Include(a => a.FilmGenres).ThenInclude(a => a.Genre)
                    .Where(a => a.FilmGenres.Any(filmGenre => filmGenre.GenreId == searchValue));
            }
            else
            {
                films = _uow.FilmRepository.Get()
                    .Include(a => a.FilmGenres).ThenInclude(a => a.Genre);
            }
            var genres = _uow.GenreRepository.Get();
            var homeViewModel = new HomeViewModel { FilmViewModels = films?.ToList(), GenreViewModels = genres, SelectedGenre = searchValue };
            return View(homeViewModel);
        }


        /// <summary>
        /// Thsi action loads all films and send it to view
        /// </summary>
        /// <returns>return IActionResult as response for the request</returns>
        public IActionResult Film()
        {
            var films = _uow.FilmRepository.Get()
                .Include(a => a.FilmGenres).ThenInclude(a => a.Genre);

            var filmViewModel = new FilmViewModel { FilmViewModels = films };
            return View(filmViewModel);
        }


        /// <summary>
        /// This Action loads genres data and send it AddFilm view 
        /// </summary>
        /// <returns>return IActionResult as response for the request</returns>
        public IActionResult AddFilm()
        {
            var genres = _uow.GenreRepository.Get();
            var genreViewModel = new AddFilmViewModel { Genres = genres };
            return View(genreViewModel);
        }


        /// <summary>
        /// This action is a post action that add film entry to storage
        /// </summary>
        /// <param name="form">form parameter get information from view</param>
        /// <param name="addFilmViewModel">addFilmViewModel viewmodel get data from view</param>
        /// <returns>return IActionResult as response for the request</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFilm(IFormCollection form, AddFilmViewModel addFilmViewModel)
        {
            if (!ModelState.IsValid)
            {
                var genres = _uow.GenreRepository.Get();
                var genreViewModel = new AddFilmViewModel { Genres = genres };
                return View(genreViewModel);
            }

            var uploads = Path.Combine(_appEnvironment.WebRootPath, "img", addFilmViewModel.PostedFile.FileName);
            await using (var fileStream = new FileStream(uploads, FileMode.Create))
            {
                await addFilmViewModel.PostedFile.CopyToAsync(fileStream);
            }

            var filmEntity = new Film
            {
                Title = addFilmViewModel.Film.Title,
                Length = addFilmViewModel.Film.Length,
                Url = addFilmViewModel.PostedFile.FileName,
                Description = addFilmViewModel.Film.Description,
            };

            _uow.FilmRepository.Add(filmEntity);

            if (form["SelectedGenre"].Count > 0)
            {
                foreach (var filmGenre in form["SelectedGenre"].Select(item => _uow.GenreRepository.GetById(int.Parse(item))).Where(a => a.Deleted == 0).Select(genre => new FilmGenre
                {
                    Film = filmEntity,
                    Genre = genre
                }).Where(a => a.Deleted == 0))
                {
                    _uow.FilmGenreRepository.Add(filmGenre);
                }
            }

            _uow.Commit();

            return RedirectToAction("Film");
        }


        /// <summary>
        /// get information from storage and send it to EditFilm View
        /// </summary>
        /// <param name="filmId">receive FilmId as a parameter</param>
        ///  <returns>return IActionResult as response for the request</returns>
        public IActionResult EditFilm(int filmId)
        {
            var film = _uow.FilmRepository.GetById(filmId);
            var genres = _uow.GenreRepository.Get();
            var selectedGenres = _uow.GenreRepository.Get().Include(a => a.FilmGenres)
                .Where(a => a.FilmGenres.Any(filmGenre => filmGenre.FilmId == filmId));
            var items = new List<SelectListItem>();
            foreach (var genre in selectedGenres)
            {
                var item = new SelectListItem(genre.GenreName, genre.GenreId.ToString());
                items.Add(item);
            }
            var genreViewModel = new AddFilmViewModel { SelectedGenres = items, Film = film, OriginalSelectedGenres = items, Genres = genres };
            return View(genreViewModel);
        }


        /// <summary>
        /// this post action save edited film in storage
        /// </summary>
        /// <param name="form">form parameter get information from view</param>
        /// <param name="addFilmViewModel">addFilmViewModel viewmodel get data from view</param>
        ///  <returns>return IActionResult as response for the request</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFilm(IFormCollection form, AddFilmViewModel addFilmViewModel)
        {
            if (!ModelState.IsValid)
            {
                var film = _uow.FilmRepository.GetById(addFilmViewModel.Film.FilmId);
                var genres = _uow.GenreRepository.Get();
                var selectedGenres = _uow.GenreRepository.Get().Include(a => a.FilmGenres)
                    .Where(a => a.FilmGenres.Any(filmGenre => filmGenre.FilmId == addFilmViewModel.Film.FilmId));

                var items = new List<SelectListItem>();
                foreach (var genre in selectedGenres)
                {
                    var item = new SelectListItem(genre.GenreName, genre.GenreId.ToString());
                    items.Add(item);
                }
                var genreViewModel = new AddFilmViewModel { SelectedGenres = items, Film = film, OriginalSelectedGenres = items, Genres = genres };
                return View(genreViewModel);
            }

            var uploads = Path.Combine(_appEnvironment.WebRootPath, "img", addFilmViewModel.PostedFile.FileName);
            await using (var fileStream = new FileStream(uploads, FileMode.Create))
            {
                await addFilmViewModel.PostedFile.CopyToAsync(fileStream);
            }

            var myFilm = _uow.FilmRepository.GetById(addFilmViewModel.Film.FilmId);
            myFilm.Title = addFilmViewModel.Film.Title;
            myFilm.Length = addFilmViewModel.Film.Length;
            myFilm.Url = addFilmViewModel.PostedFile.FileName;
            myFilm.Description = addFilmViewModel.Film.Description;

            _uow.FilmRepository.Update(myFilm);

            var addItems = form["SelectedGenre"].Except(form["OriginalSelectedGenre"]);
            var removeItems = form["OriginalSelectedGenre"].Except(form["SelectedGenre"]);
            foreach (var item in addItems)
            {
                var filmGenre = new FilmGenre { FilmId = myFilm.FilmId, GenreId = int.Parse(item) };
                _uow.FilmGenreRepository.Add(filmGenre);
            }

            foreach (var item in removeItems)
            {
                var filmGenre = _uow.FilmGenreRepository.Get()
                    .Where(a => a.FilmId == myFilm.FilmId && a.GenreId == int.Parse(item)).Cast<FilmGenre>().FirstOrDefault();
                if (filmGenre == null) continue;
                filmGenre.Deleted = 1;
                _uow.FilmGenreRepository.Update(filmGenre);
            }

            _uow.Commit();

            return RedirectToAction("Film");
        }


        /// <summary>
        /// this action get genres information from storage and send it to Genre View
        /// </summary>
        /// <returns>return IActionResult as response for the request</returns>
        public IActionResult Genre()
        {
            var genres = _uow.GenreRepository.Get();
            var genreViewModel = new GenreViewModel { GenreViewModels = genres };
            return View(genreViewModel);

        }


        /// <summary>
        /// Show AddGenre View
        /// </summary>
        /// <returns>return IActionResult as response for the request</returns>
        public IActionResult AddGenre()
        {
            return View();
        }


        /// <summary>
        /// This action add a new genre to storage
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>return IActionResult as response for the request</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddGenre(Genre genre)
        {
            if (!ModelState.IsValid) return View();
            _uow.GenreRepository.Add(genre);
            _uow.Commit();
            return RedirectToAction("Genre");

        }


        /// <summary>
        /// this action load actors data from storage and send it to Actor View
        /// </summary>
        /// <returns>return IActionResult as response for the request</returns>
        public IActionResult Actor()
        {
            var actors = _uow.ActorRepository.Get();
            var actorViewModel = new ActorViewModel { ActorViewModels = actors };
            return View(actorViewModel);
        }


        /// <summary>
        /// Show AddActor View
        /// </summary>
        /// <returns>return IActionResult as response for the request</returns>
        public IActionResult AddActor()
        {
            return View();
        }


        /// <summary>
        /// Add new actor to storage
        /// </summary>
        /// <param name="model"></param>
        /// <returns>return IActionResult as response for the request</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddActor(Actor model)
        {
            if (!ModelState.IsValid) return View();
            _uow.ActorRepository.Add(model);
            _uow.Commit();
            return RedirectToAction("Actor");
        }




    }
}
