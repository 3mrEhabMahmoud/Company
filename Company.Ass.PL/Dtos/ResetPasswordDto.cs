using System.ComponentModel.DataAnnotations;

namespace Company.Ass.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmPassword is Required !!")]
        [Compare(nameof(NewPassword), ErrorMessage = "ConfirmPassword dose not math the Password")]
        public string ConfirmPassword { get; set; }
    }
}
