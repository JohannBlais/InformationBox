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
        private readonly InformationBoxScopeParameters parameters;

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
            get { return parameters; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxScope"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public InformationBoxScope(InformationBoxScopeParameters parameters)
        {
            this.parameters = parameters;

            scopesStack.Push(this);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Completes this instance.
        /// </summary>
        public void Complete()
        {
            Dispose();
        }

        #endregion Methods

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
