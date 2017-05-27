using Questionnaire.Data.Model;
using Questionnaire.Service.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Questionnaire.Service.Localization.CollectionAggregator;

namespace Questionnaire.Service.Extensions
{
    internal static class LocalizableStringExtensions
    {
        public static byte[] ToCSV(this IEnumerable<LocalizedString> surveyLocalizations)
        {
            List<Dynamico<LocalizedString>> serializerDataSource = new List<Dynamico<LocalizedString>>();
            byte[] bytes = null;

            // Group the localizations by key
            var grouppedLocalizations = surveyLocalizations.GroupBy(ls =>
                        $"{ls.SurveyId}-{ls.TypeIdentifier}-{ls.TypeUniqueId}-{ls.FieldIdentifier}");

            var aggregationDescriptor = new AggregatorDescriptor<LocalizedString>(
                ls => ls.Culture,
                ls => ls.LocalizedValue,
                Enum.GetValues(typeof(ECulture)).Cast<object>());

            foreach (var localization in grouppedLocalizations)
            {
                var csvRow = Aggregate(localization, aggregationDescriptor);
                serializerDataSource.Add(csvRow);
            }

            if (serializerDataSource.Any())
            {
                // Get list of properties to feed the serializer
                var props = serializerDataSource.First().Properties;

                // Now data source is ready for serialization
                CsvSerializer serializer = new CsvSerializer();
                serializer.NewlineReplacement = "#|newline|#";

                using (var ms = new MemoryStream())
                {
                    serializer.Serialize(ms, serializerDataSource, props);
                    bytes = ms.ToArray();
                }
            }
            else
            {
                // File is empty
                throw new InvalidDataException("No available data for localizations");
            }

            return bytes;
        }

        public static IEnumerable<LocalizedString> ParseLocalizationsCSV(this byte[] bytes)
        {
            CsvSerializer serializer = new CsvSerializer();
            serializer.NewlineReplacement = "#|newline|#";
            List<LocalizedString> localizedStrings = new List<LocalizedString>();
            IList<Dynamico<LocalizedString>> dynamicos = new List<Dynamico<LocalizedString>>();

            var aggregationDescriptor = new AggregatorDescriptor<LocalizedString>(
                    ls => ls.Culture,
                    ls => ls.LocalizedValue,
                    Enum.GetValues(typeof(ECulture)).Cast<object>());

            using (var ms = new MemoryStream(bytes))
            {
                dynamicos = serializer.Deserialize<LocalizedString>(ms);
            }

            foreach (var localization in dynamicos)
            {
                var singelStringLocalizations = Disassemble(
                                        localization,
                                        aggregationDescriptor);
                localizedStrings.AddRange(singelStringLocalizations);
            }

            return localizedStrings;
        }
    }
}
