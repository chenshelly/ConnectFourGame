using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace Q.Models
{
    public class Player

    {

		[Range(1, 1000, ErrorMessage = "Id must be between 1 and 1000")]
		public int Id { get; set; } 

		[Display(Name = "Client Name")]
        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; } = default!;

       

        [Required(ErrorMessage = "You must enter the phone number!")]
      
        public int PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "Please select a country")]
        public string Country { get; set; } = default!;
        public DateTime Date { get; set; }
        public int numWins=0;
       
       
    }
}
