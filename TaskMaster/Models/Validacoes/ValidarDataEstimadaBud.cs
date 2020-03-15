using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.Models.Validacoes
{
    public class ValidarDataEstimadaBud : ValidationAttribute
    {
        protected override ValidationResult
               IsValid(object value, ValidationContext validationContext)
        {
            var model = (Bugs)validationContext.ObjectInstance;
            DateTime _dataestimadabug = Convert.ToDateTime(model.DataEstimadaBug);
            DateTime _databug = Convert.ToDateTime(model.DataBug);

            if ( _dataestimadabug > _databug)
            {
                return new ValidationResult
                    ("Data estimada não pode ser menor que data de abertura do bug");
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}