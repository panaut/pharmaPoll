using Questionnaire.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service.Localization
{
    public class CollectionAggregator
    {
        public static Dynamico<T> Aggregate<T>(IEnumerable<T> objects, AggregatorDescriptor<T> descriptor) where T : class, new()
        {
            Dynamico<T> dls = new Dynamico<T>(objects.First());

            // Get name of the proerty to aggregate by
            var aggregateBy = (descriptor.AggregateBy.Body as MemberExpression).Member.Name;
            var aggregateMember = (descriptor.AggregateMember.Body as MemberExpression).Member.Name;

            // Remove aggregation columns
            dls.RemoveMember(aggregateBy);
            dls.RemoveMember(aggregateMember);

            var discreteValuesAreDefined = descriptor.AggregationValues != null && descriptor.AggregationValues.Any();

            if (discreteValuesAreDefined)
            {
                foreach (var aggVal in descriptor.AggregationValues)
                {
                    dls.AddMember(aggVal.ToString());
                }
            }

            foreach (var item in objects)
            {
                var additionalMemberName = descriptor.AggregateBy.Compile().Invoke(item).ToString();
                var additionalMemberValue = descriptor.AggregateMember.Compile().Invoke(item);
                if (!discreteValuesAreDefined)
                {

                    dls.AddMember(additionalMemberName);
                }

                if(!dls.TrySetMember(additionalMemberName, additionalMemberValue))
                {
                    throw new CustomException(new Exception("Failed to Aggregate dynamic object"), 0);
                }
            }

            return dls;
        }

        public static IEnumerable<T> Disassemble<T>(Dynamico<T> sourceObject, AggregatorDescriptor<T> descriptor) where T : class, new()
        {
            List<T> retVal = new List<T>();

            // Get name of the proerty to aggregate by
            var aggregateBy = (descriptor.AggregateBy.Body as MemberExpression).Member.Name;
            var aggregateMember = (descriptor.AggregateMember.Body as MemberExpression).Member.Name;

            // Remove aggregation columns
            sourceObject.AddMember(aggregateBy);
            sourceObject.AddMember(aggregateMember);

            foreach (var aggregatorValue in descriptor.AggregationValues)
            {
                object aggVal = null;
                sourceObject.TryGetMember(aggregatorValue.ToString(), out aggVal);

                sourceObject.TrySetMember(aggregateBy, aggregatorValue);
                sourceObject.TrySetMember(aggregateMember, aggVal);

                T aggregatedElement = null;
                if (!sourceObject.TryConvert(out aggregatedElement))
                    throw new CustomException(new Exception("Failed to disassemble dynamic object"), 0);

                retVal.Add(aggregatedElement);
            }

            return retVal;
        }

        public class AggregatorDescriptor<T>
        {
            public Expression<Func<T, object>> AggregateBy { get; set; }

            public Expression<Func<T, object>> AggregateMember { get; set; }

            public IEnumerable<object> AggregationValues { get; set; }

            public AggregatorDescriptor()
            {

            }

            public AggregatorDescriptor(
                Expression<Func<T, object>> aggregateBy,
                Expression<Func<T, object>> aggregateMember,
                IEnumerable<object> aggregationValues = null)
            {
                this.AggregateBy = aggregateBy;
                this.AggregateMember = aggregateMember;
                this.AggregationValues = aggregationValues;
            }
        }
    }
}
}
