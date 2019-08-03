using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TvShows.Host.Models
{
    public class PostTvShowsDto : IValidatableObject
    {
        public List<PostTvShowDto> Shows { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Shows != null && Shows.Any())
            {
                yield break;
            }
            
            yield return new ValidationResult(
                "You must have at least one TV Show.",
                new[] {nameof(TvShows)});
        }
    }
}
