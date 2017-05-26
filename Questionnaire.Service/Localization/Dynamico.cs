using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Questionnaire.Service.Localization
{
    public class Dynamico<T> where T : class, new()
    {
        private ILogger logger = LogManager.GetLogger("Dynamico");

        private int nextIndex = 0;           // Member indexer

        private IDictionary<string, int> propertyIndexes = new Dictionary<string, int>();
        private IDictionary<string, object> properties;

        private IEnumerable<PropertyInfo> typePropertyInfo;

        private bool ignoreReferenceTypesExceptString;

        public IEnumerable<string> Properties
        {
            get
            {
                return this.properties
                    .Join(this.propertyIndexes, p => p.Key, p => p.Key, (p, q) => new { index = q.Value, Property = p.Key })
                    .OrderBy(a => a.index).Select(a => a.Property);
            }
        }

        public Dynamico(T instance, bool ignoreReferenceTypesExceptString = true)
            : this(ignoreReferenceTypesExceptString)
        {
            foreach (var propInfo in this.typePropertyInfo)
            {
                this.TrySetMember(propInfo.Name, propInfo.GetValue(instance));
            }
        }

        public Dynamico(bool ignoreReferenceTypesExceptString = true)
        {
            this.ignoreReferenceTypesExceptString = ignoreReferenceTypesExceptString;

            var type = typeof(T);

            // Get properties through reflection.
            var props =
                type.GetProperties(
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);

            // Convert array into IQueriable
            var queriableProps = props.AsQueryable();

            if (this.ignoreReferenceTypesExceptString)
            {
                queriableProps = queriableProps.Where(
                                    prop => prop.PropertyType.IsValueType || prop.PropertyType.Name == "String");
            }

            this.typePropertyInfo = queriableProps;

            this.properties = queriableProps.ToDictionary(pi => pi.Name, pi => new object());
            this.propertyIndexes = queriableProps.ToDictionary(pi => pi.Name, pi => this.nextIndex++);
        }

        public bool TryAddMember(string memberName)
        {
            try
            {
                if (this.properties.ContainsKey(memberName))
                {
                    this.logger.Info("Failed to add Member {0} to dynamic type. Member already exists", memberName);
                    return false;
                }

                this.properties.Add(memberName, null);
                this.propertyIndexes.Add(memberName, this.nextIndex++);
                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error("Failed to add Member {0} to dynamic type.", memberName);
                this.logger.Error(ex.Message);
                this.logger.Error(ex.ToString());
                return false;
            }
        }

        public void AddMember(string memberName)
        {
            this.properties.Add(memberName, null);
            this.propertyIndexes.Add(memberName, this.nextIndex++);
        }

        public bool TryRemoveMember(string memberName)
        {
            try
            {
                if (!this.properties.ContainsKey(memberName))
                {
                    this.logger.Info("Failed to remove Member {0} from dynamic type. Member doesn't exists", memberName);
                    return false;
                }

                this.properties.Remove(memberName);
                this.propertyIndexes.Remove(memberName);

                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error("Failed to remove Member {0} from dynamic type.", memberName);
                this.logger.Error(ex.Message);
                this.logger.Error(ex.ToString());
                return false;
            }
        }

        public void RemoveMember(string memberName)
        {
            this.properties.Remove(memberName);
            this.propertyIndexes.Remove(memberName);
        }

        public void SetMember(string memberName, object value)
        {
            this.properties[memberName] = value;
        }

        public object GetMember(string memberName)
        {
            return this.properties[memberName];
        }

        public bool TrySetMember(string memberName, object value)
        {
            if (!this.properties.ContainsKey(memberName))
            {
                this.logger.Error("Failed to set member {0} of Dynamico<{1}> object", memberName, typeof(T).Name);
                return false;
            }

            this.properties[memberName] = value;
            return true;
        }

        public bool TryGetMember(string memberName, out object result)
        {
            result = null;
            if (!this.properties.ContainsKey(memberName))
            {
                this.logger.Error("Failed to get member {0} of Dynamico<{1}> object", memberName, typeof(T).Name);
                return false;
            }

            result = this.properties[memberName];
            return true;
        }

        public bool TryConvert(Type type, out object result)
        {
            result = null;

            try
            {
                var instance = System.Convert.ChangeType(Activator.CreateInstance(type, BindingFlags.CreateInstance), type);

                var props = type.GetProperties();

                foreach (var prop in props)
                {
                    if (this.properties.ContainsKey(prop.Name))
                    {
                        // Converter for type
                        var converter = TypeDescriptor.GetConverter(prop.PropertyType);

                        // Convert value
                        var convertedValue = converter.ConvertFrom(this.properties[prop.Name]);

                        prop.SetValue(instance, convertedValue);
                    }
                }

                result = instance;

                return true;
            }
            catch (Exception)
            {
                this.logger.Error("Failed to Convert Dynamic object into Type: {0}", type.Name);
                return false;
            }
        }

        public object Convert(Type type)
        {
            var instance = System.Convert.ChangeType(Activator.CreateInstance(type, BindingFlags.CreateInstance), type);

            var props = type.GetProperties();

            foreach (var prop in props)
            {
                if (this.properties.ContainsKey(prop.Name))
                {
                    // Converter for type
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);

                    // Convert value
                    var convertedValue = converter.ConvertFrom(this.properties[prop.Name]);

                    prop.SetValue(instance, convertedValue);
                }
            }

            return instance;
        }

        public T Convert()
        {
            var instance = (T)(Activator.CreateInstance(typeof(T)));

            foreach (var prop in this.typePropertyInfo)
            {
                if (this.properties.ContainsKey(prop.Name))
                {
                    // Converter for type
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);

                    // Convert value
                    var convertedValue = converter.ConvertFrom(this.properties[prop.Name]);

                    prop.SetValue(instance, convertedValue);
                }
            }

            return instance;
        }

        public bool TryConvert(out T result)
        {
            result = default(T);

            try
            {
                var instance = new T();

                foreach (var prop in this.typePropertyInfo)
                {
                    if (this.properties.ContainsKey(prop.Name))
                    {
                        // Converter for type
                        var converter = TypeDescriptor.GetConverter(prop.PropertyType);

                        // Convert value
                        var convertedValue = converter.ConvertFrom(this.properties[prop.Name]);

                        prop.SetValue(instance, convertedValue);
                    }
                }

                result = instance;

                return true;
            }
            catch (Exception)
            {
                this.logger.Error("Failed to Convert Dynamic object into Type: {0}", typeof(T).Name);
                return false;
            }
        }
    }
}
