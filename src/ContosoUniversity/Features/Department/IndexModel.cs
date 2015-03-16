namespace ContosoUniversity.Features.Department
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IndexModel
    {
        public string Name { get; set; }

        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }

        public int DepartmentID { get; set; }

        [Display(Name = "Administrator")]
        public string AdministratorFullName { get; set; }
    }
}