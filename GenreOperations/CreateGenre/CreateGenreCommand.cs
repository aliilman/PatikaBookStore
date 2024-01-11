using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PatikaBookStore.DbOperations;
using PatikaBookStore.Models;


namespace PatikaBookStore.GenreOperations
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
            {
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
            }

            genre = new Genre();
            genre.Name = Model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}