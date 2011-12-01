﻿using System;
using System.Linq.Expressions;

namespace NValidator.Validators
{
    public sealed class NotEqualValidator<T, TProperty> : ComparisonValidator<T, TProperty> where TProperty : IComparable
    {
        internal const string Message = "@PropertyName must not equal to @ComparisonValue.";

        public NotEqualValidator(TProperty value)
            : base(value, Message, EqualValidator<T, TProperty>.Message)
        {
        }

        public NotEqualValidator(Expression<Func<T, TProperty>> expression)
            : base(expression, Message, EqualValidator<T, TProperty>.Message)
        {
        }

        public override bool IsValid(TProperty value, ValidationContext validationContext)
        {
            if (_value == null)
            {
                return value != null;
            }
            return ((IComparable)_value).CompareTo(value) != 0;
        }
    }
}