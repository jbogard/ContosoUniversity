namespace ContosoUniversity.Features.Department
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DetailsModel
    {
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int DepartmentID { get; set; }

        public string AdministratorFullName { get; set; }

    }
}