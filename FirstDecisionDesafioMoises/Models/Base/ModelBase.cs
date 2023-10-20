using FirstDecisionDesafioMoises.Infraestructure.Extensions;
using FirstDecisionDesafioMoises.Infraestructure.Validators.Classes;
using FirstDecisionDesafioMoises.Infraestructure.Validators.Interfaces;
using FirstDecisionDesafioMoises.Models.Interfaces;
using System;
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

        public TCopy CopyMembersTo<TCopy>(TCopy to)
            where TCopy : IModel, new()
        {
            if (to == null)
                throw new ArgumentNullException(nameof(to));

            if (typeof(TCopy) != this.GetType())
                throw new ApplicationException("Tipo incompatível para cópia");

            return (TCopy)CopyMembersTo(this, to, typeof(TCopy));
        }
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
        
        private object CopyMembersTo(object from, object to, Type typeOfTo)
        {
            if (typeOfTo == null)
                throw new ArgumentNullException(nameof(typeOfTo));
            
            var typeFrom = from?.GetType();

            if (typeFrom.IsPrimitiveType())
                return from;

            var propertiesFrom = typeFrom?.GetProperties();
            var propertiesTo = typeOfTo.GetProperties();

            foreach (var propertyFrom in propertiesFrom)
            {
                var propertyTo = propertiesTo.FirstOrDefault(x => x.Name.Equals(propertyFrom.Name));

                if (propertyTo.GetSetMethod() == null)
                    continue;

                var valueFrom = propertyFrom.GetValue(from);

                if (valueFrom == null)
                    continue;

                var valueTypeFrom = valueFrom.GetType();

                string typeFromFullName = valueTypeFrom.GetElementType()?.FullName ?? valueTypeFrom.FullName;

                if (typeFromFullName.Equals(propertyTo?.PropertyType.FullName))
                {
                    propertyTo.SetValue(to, valueFrom);
                }
                else if (propertyTo != null)
                {
                    var propertyTypeTo = propertyTo?.PropertyType;

                    if (propertyTypeTo.IsArray)
                    {
                        Type typeArrayTo = propertyTypeTo.GetElementType();

                        Array arrayFrom = (Array)valueFrom;
                        Array arrayTo = Array.CreateInstance(typeArrayTo, arrayFrom.Length);
                        if (arrayTo != null)
                            for (int i = 0; i < arrayFrom.Length; i++)
                            {
                                var objFrom = arrayFrom.GetValue(i);
                                var objTo = Activator.CreateInstance(typeArrayTo);
                                objTo = CopyMembersTo(objFrom, objTo, typeArrayTo);
                                arrayTo.SetValue(objTo, i);
                            }

                        propertyTo.SetValue(to, arrayTo);
                    }
                    else
                    {
                        var valueTo = Activator.CreateInstance(propertyTypeTo);
                        propertyTo.SetValue(to, CopyMembersTo(valueFrom, valueTo, propertyTypeTo));
                    }
                }
            }

            return to;
        }
        #endregion


    }
}