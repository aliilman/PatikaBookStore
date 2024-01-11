
using PatikaBookStore.DbOperations;

namespace PatikaBookStore.GenreOperations
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Güncellenecek Kitap Türü Bulunamadı!");
            }

            if (_dbContext.Genres.Any(x =>
                    string.Equals(x.Name.ToLower(), Model.Name.ToLower(), StringComparison.Ordinal) && x.Id != GenreId))
            {
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut");
            }
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}