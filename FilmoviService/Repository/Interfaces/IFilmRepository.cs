using FilmoviService.Models;
using FilmoviService.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmoviService.Repository.Interfaces
{
    public interface IFilmRepository
    {
        IEnumerable<Film> GetAll();
        IEnumerable<Film> GetAll(FilmFilters filter);
        Film GetById(int id);
        void Add(Film film);
        void Delete(Film film);
    }
}
