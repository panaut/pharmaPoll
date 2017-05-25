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
            byte[] bytes;

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

            // Now data source is ready for serialization
            CsvSerializer serializer = new CsvSerializer();

            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, serializerDataSource, Enum.GetNames(typeof(ECulture)));

                bytes = ms.ToArray();
            }

            return bytes;
        }

        public static IEnumerable<LocalizedString> FromCSV(this byte[] bytes)
        {
            CsvSerializer serializer = new CsvSerializer();
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
