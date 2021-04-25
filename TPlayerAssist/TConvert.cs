namespace TPlayerAssist
{
    public static class TConvert
    {
        #region Json

        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T ToJson<T>(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
                }
                catch
                {
                }
            }
            return default(T);
        }

        public static dynamic ToJson(this string value)
        {
            return Newtonsoft.Json.Linq.JToken.Parse(value) as dynamic;
        }

        #endregion
    }
}
