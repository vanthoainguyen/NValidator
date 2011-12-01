﻿using System;
using System.Linq.Expressions;

namespace NValidator.Validators
{
    public sealed class LessThanOrEqualValidator<T, TProperty> : ComparisonValidator<T, TProperty> where TProperty : IComparable
    {
        internal const string Message = "@PropertyName must be less than or equal @ComparisonValue.";

        public LessThanOrEqualValidator(TProperty value)
            : base(value, Message, GreaterThanValidator<T, TProperty>.Message)
        {
        }

        public LessThanOrEqualValidator(Expression<Func<T, TProperty>> expression)
            : base(expression, Message, GreaterThanValidator<T, TProperty>.Message)
        {
        }

        public override bool IsValid(TProperty value, ValidationContext validationContext)
        {
            if (_value == null)
            {
                return value != null && ((IComparable)value).CompareTo(_value) <= 0;
            }
            return ((IComparable)_value).CompareTo(value) >= 0;
        }
    }
}