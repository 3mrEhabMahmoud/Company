using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Ass.PL.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Name is Required !")]
        public string Name { get; set; }
        [DisplayName("Date of Create")]
        public DateTime CreateAt { get; set; }
        [Range(22,60,ErrorMessage = "Age is Required !")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage = "Email is Required !")]
        public string Email { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage = "Address is Required !")]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DataType("Hiring Date")]
        public DateTime HiringDate { get; set; }
        public int? DepartmentId { get; set; }

    }
}
