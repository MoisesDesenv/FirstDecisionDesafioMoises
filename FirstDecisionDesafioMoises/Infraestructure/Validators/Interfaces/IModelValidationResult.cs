using FirstDecisionDesafioMoises.Models.Interfaces;
using System;
using System.Linq.Expressions;

namespace FirstDecisionDesafioMoises.Infraestructure.Validators.Interfaces
{
    public interface IModelValidationResult
    {
        Expression<Func<TModel, object>> GetMember<TModel>()
            where TModel : class, IModel, new();
        string ErrorMessage { get; set; }
    }
}