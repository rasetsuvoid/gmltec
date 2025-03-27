using gmltec.application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace gmltec.application.Util
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> BuildExpression<T>(List<FilterCondition> filters)
        {
            if (filters == null || filters.Count == 0)
                return x => true; // Si no hay filtros, devolver una expresión siempre verdadera.

            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression finalExpression = null;

            foreach (var filter in filters)
            {
                MemberExpression property = Expression.Property(parameter, filter.Property);
                Type propertyType = Nullable.GetUnderlyingType(property.Type) ?? property.Type;
                Expression valueExpression;

                try
                {
                    object convertedValue = null;

                    // Convertir el valor adecuadamente
                    if (filter.Value is JsonElement jsonElement)
                    {
                        string valueString = jsonElement.GetRawText().Trim('"');

                        if (propertyType == typeof(bool))
                        {
                            convertedValue = bool.TryParse(valueString, out bool parsedBool) && parsedBool;
                        }
                        else if (propertyType == typeof(int))
                        {
                            convertedValue = int.TryParse(valueString, out int parsedInt) ? parsedInt : 0;
                        }
                        else
                        {
                            convertedValue = Convert.ChangeType(valueString, propertyType);
                        }
                    }
                    else
                    {
                        convertedValue = Convert.ChangeType(filter.Value, propertyType);
                    }

                    valueExpression = Expression.Constant(convertedValue, propertyType);
                }
                catch
                {
                    continue; // Ignorar filtros con error de conversión
                }

                // 🔹 Evaluar `Operator` y construir la condición correspondiente
                Expression condition = filter.Operator switch
                {
                    "==" => Expression.Equal(property, valueExpression),
                    "!=" => Expression.NotEqual(property, valueExpression),
                    ">" => Expression.GreaterThan(property, valueExpression),
                    ">=" => Expression.GreaterThanOrEqual(property, valueExpression),
                    "<" => Expression.LessThan(property, valueExpression),
                    "<=" => Expression.LessThanOrEqual(property, valueExpression),
                    "contains" when property.Type == typeof(string) =>
                        Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, valueExpression),
                    _ => Expression.Equal(property, valueExpression) // Si el operador no es válido, usa `==` por defecto
                };

                // Combinar con la expresión final
                if (finalExpression == null)
                {
                    finalExpression = condition;
                }
                else
                {
                    finalExpression = filter.LogicalOperator.Equals("OR", StringComparison.OrdinalIgnoreCase)
                        ? Expression.OrElse(finalExpression, condition)
                        : Expression.AndAlso(finalExpression, condition);
                }
            }

            return finalExpression != null
                ? Expression.Lambda<Func<T, bool>>(finalExpression, parameter)
                : x => true; // Si no se creó ninguna expresión, devolver `true`.
        }
    }
}
