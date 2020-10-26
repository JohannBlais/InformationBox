namespace InfoBox.Designer.CodeGeneration
{
    /// <summary>
    /// Code generator factory
    /// </summary>
    public class CodeGeneratorFactory : ICodeGeneratorFactory
    {
        #region ICodeGeneratorFactory Members

        /// <summary>
        /// Creates the code generator based on the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public ICodeGenerator CreateGenerator(Language language)
        {
            switch (language)
            {
                case Language.CSharp:
                    return new CSharpGenerator();
                case Language.VBNET:
                    return new VbNetGenerator();
                default:
                    return null;
            }
        }

        #endregion
    }
}
