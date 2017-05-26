using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service.Localization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Exception = System.Exception;

    /// <summary>
    /// Serialize and Deserialize Lists of any object type to CSV.
    /// </summary>
    public class CsvSerializer
    {
        /// <summary>
        /// The properties.
        /// </summary>
        //private IList<string> properties;

        /// <summary>
        /// The ignore empty lines.
        /// </summary>
        private bool ignoreEmptyLines = true;

        /// <summary>
        /// The ignore reference types except string.
        /// </summary>
        private bool ignoreReferenceTypesExceptString = true;

        /// <summary>
        /// The newline replacement.
        /// </summary>
        private string newlineReplacement = ((char)0x254).ToString();

        /// <summary>
        /// The replacement.
        /// </summary>
        private string replacement = ((char)0x255).ToString();

        /// <summary>
        /// The row number column title.
        /// </summary>
        private string rowNumberColumnTitle = "RowNumber";

        /// <summary>
        /// The separator.
        /// </summary>
        private char separator = '\t';

        /// <summary>
        /// The value indicating whether line numbers should be used.
        /// </summary>
        private bool useLineNumbers = true;

        /// <summary>
        /// Gets or sets a value indicating whether ignore empty lines.
        /// </summary>
        /// <value>The ignore empty lines.</value>
        public bool IgnoreEmptyLines
        {
            get
            {
                return this.ignoreEmptyLines;
            }

            set
            {
                this.ignoreEmptyLines = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ignore reference types except string.
        /// </summary>
        /// <value>The ignore reference types except string.</value>
        public bool IgnoreReferenceTypesExceptString
        {
            get
            {
                return this.ignoreReferenceTypesExceptString;
            }

            set
            {
                this.ignoreReferenceTypesExceptString = value;
            }
        }

        /// <summary>
        /// Gets or sets the newline replacement.
        /// </summary>
        /// <value>The newline replacement.</value>
        public string NewlineReplacement
        {
            get
            {
                return this.newlineReplacement;
            }

            set
            {
                this.newlineReplacement = value;
            }
        }

        /// <summary>
        /// Gets or sets the replacement.
        /// </summary>
        /// <value>The replacement.</value>
        public string Replacement
        {
            get
            {
                return this.replacement;
            }

            set
            {
                this.replacement = value;
            }
        }

        /// <summary>
        /// Gets or sets the row number column title.
        /// </summary>
        /// <value>The row number column title.</value>
        public string RowNumberColumnTitle
        {
            get
            {
                return this.rowNumberColumnTitle;
            }

            set
            {
                this.rowNumberColumnTitle = value;
            }
        }

        /// <summary>
        /// Gets or sets the separator.
        /// </summary>
        /// <value>The separator.</value>
        public char Separator
        {
            get
            {
                return this.separator;
            }

            set
            {
                this.separator = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether use EOF literal.
        /// </summary>
        /// <value>The use EOF literal.</value>
        public bool UseEofLiteral { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use line numbers.
        /// </summary>
        /// <value>The use line numbers.</value>
        public bool UseLineNumbers
        {
            get
            {
                return this.useLineNumbers;
            }

            set
            {
                this.useLineNumbers = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether use text qualifier.
        /// </summary>
        /// <value>The use text qualifier.</value>
        public bool UseTextQualifier { get; set; }

        /// <summary>
        /// The method that performs Deserialization.
        /// </summary>
        /// <param name="stream">
        /// The target Stream.
        /// </param>
        /// <returns>The <see cref="IList" />.</returns>
        public IList<Dynamico<T>> Deserialize<T>(Stream stream) where T : class, new()
        {
            // Columns (fields)
            string[] columns;

            // Rows (items)
            string[] rows;

            try
            {
                // Use Stream Reader
                using (var sr = new StreamReader(stream))
                {
                    columns = sr.ReadLine().Split(this.Separator);
                    rows = sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "The CSV File is invalid.",
                    ex);
            }

            // List of deserialized items
            var data = new List<Dynamico<T>>();

            // Use String Builder to construct cumulative error message
            var errorBuilder = new StringBuilder();

            // Perform deserialization of each item
            for (int row = 0; row < rows.Length; row++)
            {
                try
                {
                    // Current line (item)
                    var line = rows[row];

                    if (this.IgnoreEmptyLines && string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    if (!this.IgnoreEmptyLines && string.IsNullOrWhiteSpace(line))
                    {
                        throw new Exception(
                            string.Format(@"Empty line at line number: {0}", row));
                    }

                    // Columns of current line (fields)
                    var parts = line.Split(this.Separator);

                    // Index of initial column
                    // 2 - if line numbers occupy first column
                    // 1 - otherwise
                    var firstColumnIndex = this.UseLineNumbers ? 2 : 1;
                    if (parts.Length == firstColumnIndex && parts[firstColumnIndex - 1] != null
                        && parts[firstColumnIndex - 1] == "EOF")
                    {
                        break;
                    }

                    // New instance of object
                    var objItem = new Dynamico<T>();

                    // Initial index
                    var start = this.UseLineNumbers ? 1 : 0;

                    // Process all columns
                    for (int i = start; i < parts.Length; i++)
                    {
                        // Value if the property
                        var value = parts[i];

                        // Current column
                        var column = columns[i];

                        // continue of deviant RowNumber column condition
                        // this allows for the deserializer to implicitly ignore the RowNumber column
                        if (column.Equals(this.RowNumberColumnTitle))
                        {
                            continue;
                        }

                        value =
                            value.Replace(this.Replacement, this.Separator.ToString())
                                .Replace(this.NewlineReplacement, Environment.NewLine)
                                .Trim();

                        // Value of property
                        var curProp = column;

                        // ignore property csv column, Property not found on targing type
                        if (curProp == null)
                        {
                            continue;
                        }

                        if (this.UseTextQualifier)
                        {
                            if (value.IndexOf("\"") == 0)
                            {
                                value = value.Substring(1);
                            }

                            if (value[value.Length - 1].ToString() == "\"")
                            {
                                value = value.Substring(0, value.Length - 1);
                            }
                        }

                        if (!objItem.TrySetMember(curProp, value))
                        {
                            objItem.AddMember(curProp);
                            objItem.SetMember(curProp, value);
                        }
                    }

                    data.Add(objItem);
                }
                catch (Exception ex)
                {
                    errorBuilder
                        .AppendLine(
                                string.Format(
                                        "Row: {0} - Failed to deserialize item ({1})",
                                        row,
                                        ex.Message));
                }
            }

            if (!string.IsNullOrEmpty(errorBuilder.ToString()))
            {
                throw new Exception(
                                errorBuilder.ToString(),
                                new InvalidCastException());
            }

            return data;
        }

        /// <summary>
        /// Performs actual Serialization.
        /// </summary>
        /// <param name="stream">
        /// The Stream to write output to.
        /// </param>
        /// <param name="data">
        /// The Data to be serialized.
        /// </param>
        public void Serialize<T>(
            Stream stream,
            IEnumerable<Dynamico<T>> data,
            IEnumerable<string> allProperties) where T : class, new()
        {
            // Use string builder
            var sb = new StringBuilder();

            // List to store properties
            var values = new List<string>();

            sb.AppendLine(this.GetHeader(allProperties));

            // row index
            var row = 1;

            // Serialize each item individually
            foreach (var item in data)
            {
                values.Clear();

                if (this.UseLineNumbers)
                {
                    values.Add((row++).ToString());
                }

                // serialize each property
                foreach (var prop in allProperties)
                {
                    // Raw Value of property
                    var raw = item.GetMember(prop);

                    // Value of property
                    var value = raw == null
                                    ? string.Empty
                                    : raw.ToString()
                                          .Replace(this.Separator.ToString(), this.Replacement)
                                          .Replace("\n", this.NewlineReplacement)
                                          .Replace(Environment.NewLine, this.NewlineReplacement);

                    if (this.UseTextQualifier)
                    {
                        value = string.Format("\"{0}\"", value);
                    }

                    values.Add(value);
                }

                sb.AppendLine(string.Join(this.Separator.ToString(), values.ToArray()));
            }

            if (this.UseEofLiteral)
            {
                values.Clear();

                if (this.UseLineNumbers)
                {
                    values.Add((row++).ToString());
                }

                values.Add("EOF");

                sb.AppendLine(string.Join(this.Separator.ToString(), values.ToArray()));
            }

            // Use stream writter
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(sb.ToString().Trim());
            }
        }

        /// <summary>
        /// Get Header.
        /// </summary>
        /// <returns>The <see cref="string" />.</returns>
        private string GetHeader(IEnumerable<string> allProperties)
        {
            // Property names
            var header = allProperties;

            if (this.UseLineNumbers)
            {
                header = new[] { this.RowNumberColumnTitle }.Union(header);
            }

            return string.Join(this.Separator.ToString(), header.ToArray());
        }
    }
}
