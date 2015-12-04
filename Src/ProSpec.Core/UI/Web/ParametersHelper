namespace ProSpec.Core.UI.Web
{
    public class ParametersHelper
    {
        public static string ToString(string[] RESTParameters, string queryString)
        {
            string parametersAsString = string.Empty;

            if (RESTParameters != null && RESTParameters.Length > 0)
            {
                parametersAsString = string.Join("/", RESTParameters);
            }

            if (queryString != null)
            {
                if (!queryString.StartsWith("?"))
                {
                    queryString = string.Concat("?", queryString);
                }

                parametersAsString += queryString;
            }

            return parametersAsString;
        }
    }
}
