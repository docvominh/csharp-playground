using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace WebApi.Attributes;

public class ManufactureValidate : ValidationAttribute
{
    public string[] AcceptList { get; }

    public ManufactureValidate(string[] acceptList)
    {
        AcceptList = acceptList;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var product = (Product)validationContext.ObjectInstance;
        if (!AcceptList.Contains(value))
        {
            if (product.Price > 500)
            {
            }

            return new ValidationResult("Manufacture is not accepted");
        }

        return ValidationResult.Success;
    }
}