/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Apache License, Version 2.0, please send an email to 
 * dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Apache License, Version 2.0.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

#if !FEATURE_CORE_DLR
using Microsoft.Scripting.Ast;
#else
using System.Linq.Expressions;
#endif

using System.Dynamic.Utils;

namespace System.Dynamic {
    /// <summary>
    /// Represents the unary dynamic operation at the call site, providing the binding semantic and the details about the operation.
    /// </summary>
    public abstract class UnaryOperationBinder : DynamicMetaObjectBinder {
        private ExpressionType _operation;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryOperationBinder"/> class.
        /// </summary>
        /// <param name="operation">The unary operation kind.</param>
        protected UnaryOperationBinder(ExpressionType operation) {
            ContractUtils.Requires(OperationIsValid(operation), "operation");
            _operation = operation;
        }

        /// <summary>
        /// The result type of the operation.
        /// </summary>
        public override sealed Type ReturnType {
            get {
                switch(_operation) {
                    case ExpressionType.IsFalse:
                    case ExpressionType.IsTrue:
                        return typeof(bool);
                    default:
                        return typeof(object);
                }
            }
        }

        /// <summary>
        /// The unary operation kind.
        /// </summary>
        public ExpressionType Operation {
            get {
                return _operation;
            }
        }

        /// <summary>
        /// Performs the binding of the unary dynamic operation if the target dynamic object cannot bind.
        /// </summary>
        /// <param name="target">The target of the dynamic unary operation.</param>
        /// <returns>The <see cref="DynamicMetaObject"/> representing the result of the binding.</returns>
        public DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target) {
            return FallbackUnaryOperation(target, null);
        }

        /// <summary>
        /// Performs the binding of the unary dynamic operation if the target dynamic object cannot bind.
        /// </summary>
        /// <param name="target">The target of the dynamic unary operation.</param>
        /// <param name="errorSuggestion">The binding result in case the binding fails, or null.</param>
        /// <returns>The <see cref="DynamicMetaObject"/> representing the result of the binding.</returns>
        public abstract DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

        /// <summary>
        /// Performs the binding of the dynamic unary operation.
        /// </summary>
        /// <param name="target">The target of the dynamic operation.</param>
        /// <param name="args">An array of arguments of the dynamic operation.</param>
        /// <returns>The <see cref="DynamicMetaObject"/> representing the result of the binding.</returns>
        public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args) {
            ContractUtils.RequiresNotNull(target, "target");
            ContractUtils.Requires(args == null || args.Length == 0, "args");

            return target.BindUnaryOperation(this);
        }

        // this is a standard DynamicMetaObjectBinder
        internal override sealed bool IsStandardBinder {
            get {
                return true;
            }
        }

        internal static bool OperationIsValid(ExpressionType operation) {
            switch (operation) {
                #region Generated Unary Operation Binder Validator

                // *** BEGIN GENERATED CODE ***
                // generated by function: gen_unop_validator from: generate_tree.py

                case ExpressionType.Negate:
                case ExpressionType.UnaryPlus:
                case ExpressionType.Not:
                case ExpressionType.Decrement:
                case ExpressionType.Increment:
                case ExpressionType.OnesComplement:
                case ExpressionType.IsTrue:
                case ExpressionType.IsFalse:

                // *** END GENERATED CODE ***

                #endregion

                case ExpressionType.Extension:
                    return true;

                default:
                    return false;
            }
        }
    }
}
