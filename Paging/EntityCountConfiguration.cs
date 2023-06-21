using GraduationThesis_CarServices.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Paging
{
    public class EntityCountConfiguration<T> where T : struct, IComparable<T>
    {
        public static async Task<int> Count(DataContext context, string entityName)
        {
            var entityType = Type.GetType($"GraduationThesis_CarServices.Models.Entity.{entityName}");
            if (entityType != null)
            {
                var dbSetProperty = context.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.IsGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                    prop.PropertyType.GetGenericArguments()[0] == entityType)!;
                if (dbSetProperty != null)
                {
                    object entitySet = dbSetProperty.GetValue(context)!;

                    IQueryable<object> query = (IQueryable<object>)entitySet;
                    int count = await query.CountAsync();
                    return count;
                }
            }
            return 0;
        }
    }
}