using PatikaBookStore.DbOperations;
using PatikaBookStore.Models;
namespace TestSetup
{
  public static class Authors
  {
    public static void AddAuthors(this BookStoreDbContext context)
    {
      context.Authors.AddRange(
          new Author{Name = "Eric", Surname = "Ries", Birthday = DateTime.ParseExact("1978-09-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
          new Author{Name = "Charlotte", Surname = "Gilman", Birthday = DateTime.ParseExact("1860-07-03", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
          new Author{ Name = "Frank", Surname = "Herbert", Birthday = DateTime.ParseExact("1920-10-08", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)});
    }
  }
}