﻿using FluentValidation;
using StudentAPI.DomainModels;
using StudentAPI.Repositories;
using System.Linq;

namespace StudentAPI.Validators
{
    public class UpdateStudentRequestValidator: AbstractValidator<UpdateStudentRequest>
    {

        public UpdateStudentRequestValidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(9999).LessThan(1000000000000);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (gender != null)
                    return true;

                return false;
            }).WithMessage("Please select a valid gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();
        }
    }
}
