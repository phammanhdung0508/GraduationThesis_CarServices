using System.Globalization;

namespace GraduationThesis_CarServices.Mapping
{
    public static class FormatCurrency
    {
        public static string FormatNumber(decimal value)
        {
            // Convert the decimal value to a string
            string valueString = value.ToString("0.000");

            // Split the value into whole and decimal parts
            string[] parts = valueString.Split('.');

            // Reorganize the digits of the whole part
            string wholePart = "";
            for (int i = 0; i < parts[0].Length; i++)
            {
                if (i > 0 && (parts[0].Length - i) % 3 == 0)
                    wholePart += ".";
                wholePart += parts[0][i];
            }

            // Construct the formatted value
            string formattedValue = $"{wholePart}.{parts[1]}";

            return formattedValue;
        }

        public static decimal ConvertCurrencyStringToDecimal(string currencyString)
        {
            // Splitting the input string to separate the numeric value and the currency code
            string[] parts = currencyString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid currency string format");
            }

            // Parsing the numeric value using the appropriate culture (assuming the culture uses ',' as decimal separator)
            decimal numericValue = decimal.Parse(parts[0], CultureInfo.InvariantCulture);

            return numericValue;
        }
    }
}