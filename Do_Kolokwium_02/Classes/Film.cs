using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_Kolokwium_02.Classes
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public double Budget { get; set; }
        public DateTime CreationDate { get; set; }
        public string Year => CreationDate.Year.ToString();
        public string AverageRating
        {
            get
            {
                if (Reviews.Count != 0)
                {
                    return $"{Math.Round(Reviews.Sum(r => r.Rating) / Reviews.Count, 1)}/10";
                }
                else { return "Brak"; }
            }
        }
        public List<Review> Reviews { get; set; }
        public Film()
        {
            Reviews = new List<Review>();
        }
        public bool HasReviews()
        {
            return Reviews != null && Reviews.Any();
        }
    }
}
