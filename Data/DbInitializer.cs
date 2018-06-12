using AgEntities.CustomEntities;
using AgEntities.DataContext;

namespace AgEntities.DataContext
{
    public class DbInitializer
    {
        public static void SavingData ()
        {
            using (var db = new BloggingContext ())
            {
                var blog = new Blog { Url = @"http//sample.com" };
                db.Blog.Add (blog);
                db.SaveChanges ();
            }

        }
    }
}