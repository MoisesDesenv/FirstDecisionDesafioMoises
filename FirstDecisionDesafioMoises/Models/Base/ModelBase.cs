using FirstDecisionDesafioMoises.Infraestructure.Validators.Classes;
using FirstDecisionDesafioMoises.Infraestructure.Validators.Interfaces;
using FirstDecisionDesafioMoises.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace FirstDecisionDesafioMoises.Models.Base
{
    public class ModelBase : IModel
    {
        #region PublicMethods
        public bool IsValid()
            => IsValid(out string _);
        
        public bool IsValid(out string message)
        {
            var retorno = this.IsValid(out ICollection<IModelValidationResult> result);
            message = result.FirstOrDefault()?.ErrorMessage;
            return retorno;
        }
        
        public virtual bool IsValid(out ICollection<IModelValidationResult> result)
            => this.IsValid(out result, null);
        #endregion
        #region PrivateMethods
        private bool IsValid(out ICollection<IModelValidationResult> result, string myName)
        {
            var props = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.PropertyType.IsSubclassOf(typeof(ModelBase))).ToList();

            result = new List<IModelValidationResult>();

            bool valid = ModelBaseValidator.TryValidateObject(this, new ValidationContext(this), out result, myName);

            foreach (var prop in props)
            {
                ModelBase instance = prop.GetValue(this) as ModelBase;

                ICollection<IModelValidationResult> subResult = new List<IModelValidationResult>();

                valid &= instance?.IsValid(out subResult, prop.Name) ?? true;

                result = result.Concat(subResult).ToList();
            }

            return valid;
        }
        #endregion
    }
}