using FirstDecisionDesafioMoises.Infraestructure.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDecisionDesafioMoises.Infraestructure.Validators.Classes
{
    public static class ModelBaseValidator
    {
        public static bool TryValidateObject(object instance, 
                                             ValidationContext validationContext, 
                                             out ICollection<IModelValidationResult> modelBaseValidationResults,
                                             string myName)
        {
            var validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(instance, validationContext, validationResults, true);

            modelBaseValidationResults = validationResults.Select(x => (IModelValidationResult)new ModelBaseValidationResult(x.ErrorMessage, 
                                                                                                                             instance.GetType()
                                                                                                                                     .GetProperties()
                                                                                                                                     .FirstOrDefault(y => y.Name == x.MemberNames.FirstOrDefault()), 
                                                                                                                                                          myName))
                                                          .ToList();

            return result;
        }
    }
}
