

using System.ComponentModel.DataAnnotations.Schema;


namespace PatikaBookStore.Models
{
    public class Book
    {

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }
    
    public int GenreID { get; set; }
    public Genre Genre { get; set; }

        
    }         
}