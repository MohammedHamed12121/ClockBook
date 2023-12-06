using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FacebookClone.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacebookClone.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? GivenName { get; set; }

        [Required]
        public string? SurName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }


        public int SelectedDay { get; set; }
        public List<SelectListItem> DayOptions { get; set; }
        public int SelectedMonth { get; set; }
        public List<SelectListItem> MonthOptions { get; set; }
        public int SelectedYear { get; set; }
        public List<SelectListItem> YearOptions { get; set; }

        [Required]
        public Genera? Genera { get; set; }

        public RegisterViewModel()
        {
            // Initialize the DayOptions property in the constructor
            DayOptions = Enumerable.Range(1, 31)
                                   .Select(day => new SelectListItem
                                   {
                                       Value = day.ToString(),
                                       Text = day.ToString()
                                   })
                                   .ToList();

            MonthOptions = Enumerable.Range(1, 12)
                            .Select(month => new SelectListItem
                            {
                                Value = month.ToString(),
                                Text = GetMonthName(month)
                            })
                            .ToList();

            // TODO: fix the year issue
            YearOptions = Enumerable.Range(1970, 2023)
                            .Select(year => new SelectListItem
                            {
                                Value = year.ToString(),
                                Text = year.ToString()
                            })
                            .ToList();
        }


        public string GetMonthName(int monthNumber)
        {
            // Validate that the month number is in the valid range (1 to 12)
            if (monthNumber < 1 || monthNumber > 12)
            {
                throw new ArgumentException("Invalid month number. Must be between 1 and 12.", nameof(monthNumber));
            }

            // Create a DateTime object with the specified month and a day (e.g., 1)
            DateTime date = new DateTime(2000, monthNumber, 1);

            // Use the "MMMM" format to get the full month name
            string monthName = date.ToString("MMMM");

            return monthName;
        }
    }
}


