using System.ComponentModel;

namespace ReflectionParser;

public class Parser<T>
{
    public static ParseResult<T> ParseLine(string line, Dictionary<int, string> mapping, T targetObject, string delimeter = ",")
    {
        var splitLine = line.Split(delimeter);
        return ParseLine(splitLine, mapping, targetObject, delimeter);
    }

    public static ParseResult<T> ParseLine(string[] splitLine, Dictionary<int, string> mapping, T targetObject, string delimeter = ",")
    {
        try
        {
            if (mapping.Count > splitLine.Length)
            {
                return new ParseResult<T>(targetObject, "Mapping column count must be less than or equal to input line column count.");
            }

            foreach (KeyValuePair<int, string> pair in mapping)
            {
                targetObject = AddToTargetObject(splitLine[pair.Key], pair.Value, targetObject);
            }

            return new ParseResult<T>(targetObject);
        }
        catch (Exception e)
        {
            return new ParseResult<T>(e);
        }
    }

    private static T AddToTargetObject(string fieldData, string mapTo, T targetObject)
    {
        if (targetObject == null)
        {
            throw new ArgumentNullException(nameof(targetObject), "targetObject cannot be null");
        }

        var propInfo = targetObject.GetType().GetProperty(mapTo);
        if (propInfo == null)
        {
            throw new Exception($"targetObject does not contain property {mapTo}");
        }

        var convertedData = TypeDescriptor
            .GetConverter(propInfo.PropertyType)
            .ConvertFromString(fieldData);

        propInfo.SetValue(targetObject, convertedData);

        return targetObject;
    }
}