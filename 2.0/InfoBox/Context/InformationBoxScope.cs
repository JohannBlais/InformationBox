using System;
using System.Collections.Generic;

namespace InfoBox
{
    /// <summary>
    /// Represents the scope of a set of parameters for the InformationBoxes.
    /// </summary>
    public class InformationBoxScope : IDisposable
    {
        #region Attributes

        private static readonly Stack<InformationBoxScope> scopesStack = new Stack<InformationBoxScope>();
        private readonly InformationBoxScopeParameters definedParameters = new InformationBoxScopeParameters();

        internal InformationBoxScopeParameters effectiveParameters = new InformationBoxScopeParameters();

        #endregion Attributes

        #region Properties

        /// <summary>
        /// Gets the current scope.
        /// </summary>
        /// <value>The current.</value>
        internal static InformationBoxScope Current
        {
            get
            {
                if (scopesStack.Count > 0)
                    return scopesStack.Peek();
                return null;
            }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public InformationBoxScopeParameters Parameters
        {
            get { return effectiveParameters; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxScope"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public InformationBoxScope(InformationBoxScopeParameters parameters)
        {
            this.definedParameters = parameters;

            scopesStack.Push(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxScope"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="behavior">The behavior.</param>
        public InformationBoxScope(InformationBoxScopeParameters parameters, InformationBoxScopeBehavior behavior)
        {
            this.definedParameters = parameters;
            this.effectiveParameters = parameters;
            
            if (behavior == InformationBoxScopeBehavior.InheritParent)
            {
                if (null != Current)
                {
                    // Merge with the parameters defined explicitly in the direct parent
                    this.effectiveParameters.Merge(Current.definedParameters);
                }
            }
            else if (behavior == InformationBoxScopeBehavior.InheritAll)
            {
                if (null != Current)
                {
                    // Merge the effective parameters from the parent
                    this.effectiveParameters.Merge(Current.Parameters);
                }
            }

            scopesStack.Push(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxScope"/> class.
        /// </summary>
        public InformationBoxScope()
        {
            scopesStack.Push(this);
        }

        #endregion Constructors

        #region Dispose

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            scopesStack.Pop();
        }

        #endregion Dispose
    }
}
