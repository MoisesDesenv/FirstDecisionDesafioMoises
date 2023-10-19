using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using FirstDecisionDesafioMoises.Models.Interfaces;
using FirstDecisionDesafioMoises.Infraestructure.Validators.Interfaces;

namespace FirstDecisionDesafioMoises.Infraestructure.Validators.Classes
{
    public class ModelBaseValidationResult : ValidationResult, IModelValidationResult
    {
        private readonly PropertyInfo property;
        private readonly string myName;
        public ModelBaseValidationResult(string errorMessage, PropertyInfo property, string myName) : base(errorMessage)
        {
            this.property = property;
            this.myName = myName;
        }
        public Expression<Func<TModel, object>> GetMember<TModel>()
            where TModel : class, IModel, new()
        {
            var typeModel = typeof(TModel);

            var propPai = typeModel.GetProperty(this.myName ?? this.property.Name);
            var propFilho = this.property;

            var paramPai = Expression.Parameter(typeModel, "x");
            MemberExpression bodyPai = Expression.MakeMemberAccess(paramPai, propPai);

            if (this.myName != null)
            {
                var exprPai = Expression.Lambda(bodyPai, paramPai);
                bodyPai = Expression.MakeMemberAccess(exprPai.Body, propFilho);
            }

            return Expression.Lambda<Func<TModel, object>>(bodyPai, paramPai);
        }
    }
}