using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{  
    public class PostNotFoundWithGivenLanguageException:Exception
    {
        public PostNotFoundWithGivenLanguageException(long postId, string languageCode)
          : base($"The {languageCode} translation of given Post with the ID = {postId} was not found")
        {
        }
    }
}
