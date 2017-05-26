using Questionnaire.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Questionnaire.Service.Localization
{
    public class CollectionAggregator
    {
        public static Dynamico<T> Aggregate<T>(IEnumerable<T> objects, AggregatorDescriptor<T> descriptor) where T : class, new()
        {
            Dynamico<T> dls = new Dynamico<T>(objects.First());

            // Get name of the proerty to aggregate by
            var aggregateBy = GetAggregationPropertyName(descriptor.AggregateBy);
            var aggregateMember = (descriptor.AggregateMember.Body as MemberExpression).Member.Name;

            // Remove aggregation columns
            dls.RemoveMember(aggregateBy);
            dls.RemoveMember(aggregateMember);

            var discreteValuesAreDefined = descriptor.AggregationValues != null && descriptor.AggregationValues.Any();

            var additionalMemberNameRawFunc = descriptor.AggregateBy.Compile();
            var additionalMemberValueRawFunc = descriptor.AggregateMember.Compile();
            var additionalMemberNameFunc = descriptor.AggregateByTransformation.Compile();
            var additionalMemberValueFunc = descriptor.AggregateMemberTransformation.Compile();

            if (discreteValuesAreDefined)
            {
                foreach (var aggVal in descriptor.AggregationValues)
                {
                    var transformedVal = additionalMemberNameFunc.Invoke(aggVal);
                    dls.AddMember(transformedVal);
                }
            }

            foreach (var item in objects)
            {
                var additionalMemberNameRaw = additionalMemberNameRawFunc.Invoke(item);
                var additionalMemberValueRaw = additionalMemberValueRawFunc.Invoke(item);

                var additionalMemberName = additionalMemberNameFunc
                    .Invoke(additionalMemberNameRaw)
                    ?.ToString();

                var additionalMemberValue = additionalMemberValueFunc
                    .Invoke(additionalMemberValueRaw)
                    ?.ToString();

                if (!discreteValuesAreDefined)
                {
                    dls.TryAddMember(additionalMemberName);
                }

                if (!dls.TrySetMember(additionalMemberName, additionalMemberValue))
                {
                    throw new CustomException(new Exception("Failed to Aggregate dynamic object"));
                }
            }

            return dls;
        }

        public static IEnumerable<T> Disassemble<T>(
            Dynamico<T> sourceObject,
            AggregatorDescriptor<T> descriptor,
            bool ignoreMissingCields = true)
            where T : class, new()
        {
            List<T> retVal = new List<T>();

            // Get name of the proerty to aggregate by
            var aggregateBy = GetAggregationPropertyName(descriptor.AggregateBy);
            var aggregateMember = (descriptor.AggregateMember.Body as MemberExpression).Member.Name;

            // Add aggregation columns if they don't already exist
            sourceObject.TryAddMember(aggregateBy);
            sourceObject.TryAddMember(aggregateMember);

            var additionalFieldNameFunc = descriptor.AggregateByTransformation.Compile();
            var additionalFieldNameInverseFunc = descriptor.AggregateByInverseTransformation.Compile();
            var additionalFieldValueInverseFunc = descriptor.AggregateMemberInverseTransformation.Compile();

            foreach (var aggregatorValue in descriptor.AggregationValues)
            {
                var additionalFieldName = additionalFieldNameFunc.Invoke(aggregatorValue);

                object aggVal = null;

                if (ignoreMissingCields)
                {
                    sourceObject.TryGetMember(additionalFieldName, out aggVal);
                }
                else
                {
                    aggVal = sourceObject.GetMember(additionalFieldName);
                }

                var aggregateByTransformedVal = additionalFieldNameInverseFunc.Invoke(aggregatorValue.ToString());
                var aggregateMemberTransformedVal = additionalFieldValueInverseFunc.Invoke(aggVal);

                sourceObject.TrySetMember(aggregateBy, aggregateByTransformedVal);
                sourceObject.TrySetMember(aggregateMember, aggregateMemberTransformedVal);

                T aggregatedElement = null;
                if (!sourceObject.TryConvert(out aggregatedElement))
                    throw new CustomException(new Exception("Failed to disassemble dynamic object"));

                retVal.Add(aggregatedElement);
            }

            return retVal;
        }

        private static string GetAggregationPropertyName<T>(Expression<Func<T, object>> aggregateBy)
        {
            var memberExp = aggregateBy.Body as MemberExpression;

            if (memberExp != null)
                return memberExp.Member.Name;

            var unaryExp = aggregateBy.Body as UnaryExpression;
            if (unaryExp != null)
            {
                // Get parameter
                var onlyParameter = aggregateBy.Parameters.Single().Name;

                memberExp = unaryExp.Operand as MemberExpression;
                if (memberExp != null)
                    return memberExp.Member.Name;
            }

            throw new CustomException(new ArgumentException("Parameter 'aggregateBy' must be a Member expression"));
        }

        public class AggregatorDescriptor<T>
        {
            public Expression<Func<T, object>> AggregateBy { get; set; }

            public Expression<Func<object, string>> AggregateByTransformation { get; set; }

            public Expression<Func<string, object>> AggregateByInverseTransformation { get; set; }

            public Expression<Func<object, object>> AggregateMemberTransformation { get; set; }

            public Expression<Func<object, object>> AggregateMemberInverseTransformation { get; set; }

            public Expression<Func<T, object>> AggregateMember { get; set; }

            public IEnumerable<object> AggregationValues { get; set; }

            public AggregatorDescriptor()
            {

            }

            public AggregatorDescriptor(
                Expression<Func<T, object>> aggregateBy,
                Expression<Func<T, object>> aggregateMember,
                IEnumerable<object> aggregationValues = null,
                Expression<Func<object, string>> aggregateByTransformation = null,
                Expression<Func<string, object>> aggregateByInverseTransformation = null,
                Expression<Func<object, object>> aggregateMemberTransformation = null,
                Expression<Func<object, object>> aggregateMemberInverseTransformation = null
                )
            {
                this.AggregateBy = aggregateBy;
                this.AggregateMember = aggregateMember;
                this.AggregationValues = aggregationValues;

                if (aggregateByTransformation == null)
                {
                    aggregateByTransformation = obj => obj.ToString();
                }

                this.AggregateByTransformation = aggregateByTransformation;

                if (aggregateByInverseTransformation == null)
                {
                    aggregateByInverseTransformation = obj => obj;
                }

                this.AggregateByInverseTransformation = aggregateByInverseTransformation;

                if (aggregateMemberTransformation == null)
                {
                    aggregateMemberTransformation = obj => obj;
                }

                this.AggregateMemberTransformation = aggregateMemberTransformation;

                if (aggregateMemberInverseTransformation == null)
                {
                    aggregateMemberInverseTransformation = obj => obj;
                }

                this.AggregateMemberInverseTransformation = aggregateMemberInverseTransformation;
            }
        }
    }
}
