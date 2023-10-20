using System.Collections.Generic;
using FirstDecisionDesafioMoises.Infraestructure.Validators.Interfaces;

namespace FirstDecisionDesafioMoises.Models.Interfaces
{
    public interface IModel
    {
        bool IsValid();
        bool IsValid(out string message);
        bool IsValid(out ICollection<IModelValidationResult> result);
    }
}