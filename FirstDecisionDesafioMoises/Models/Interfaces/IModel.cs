using FirstDecisionDesafioMoises.Infraestructure.Validators.Interfaces;
using System.Collections.Generic;

namespace FirstDecisionDesafioMoises.Models.Interfaces
{
    public interface IModel
    {
        bool IsValid();
        bool IsValid(out string message);
        bool IsValid(out ICollection<IModelValidationResult> result);
    }
}