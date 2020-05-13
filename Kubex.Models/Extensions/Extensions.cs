namespace Kubex.Models.Extensions
{
    public static class Extensions
    {
        public static T UpdateValues<T>(this T entityToUpdate, T newEntity)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties) 
            {
                var newValue = property.GetValue(newEntity);
                property.SetValue(entityToUpdate, newValue);
            }

            return entityToUpdate;
        }
    }
}