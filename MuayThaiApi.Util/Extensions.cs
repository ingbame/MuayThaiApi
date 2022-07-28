using System.Reflection;

namespace System
{
    public static class Extensions
    {
        public static TCopy CopyProperties<TSource,TCopy>(this TSource source,TCopy copy)
        {
            if(source == null || copy == null)
                throw new Exception("Both variables must be instantiated.");
            PropertyInfo[] propSource = source.GetType().GetProperties();
            foreach (PropertyInfo prop in propSource)
            {
                if(copy.GetType().GetProperties().Select(s => s.Name.ToLower()).Contains(prop.Name.ToLower()))
                {
                    var propToCopy = copy.GetType().GetProperties().Where(w => w.Name.ToLower() == prop.Name.ToLower()).FirstOrDefault();
                    if (propToCopy.CanWrite)
                        propToCopy.SetValue(copy, prop.GetValue(source, null), null);
                }
            }
            return copy;
        }
    }
}