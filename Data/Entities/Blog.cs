<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> f65938ceca5ffdef9bf74938536ac4fcfd1c45fa
using System.Collections.Generic;

namespace AgEntities.CustomEntities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> f65938ceca5ffdef9bf74938536ac4fcfd1c45fa
