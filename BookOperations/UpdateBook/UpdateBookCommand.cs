using PatikaBookStore.DbOperations;

namespace PatikaBookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId {get; set;}
        public UpdateBookModel Model {get; set;}
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book =_dbContext.Books.SingleOrDefault(x=> x.Id==BookId);
            if(book is null)
             {
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            book.GenreID=Model.GenreId != default ? Model.GenreId : book.GenreID;
            book.Title=Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}