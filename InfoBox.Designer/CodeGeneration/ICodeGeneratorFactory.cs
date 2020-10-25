namespace InfoBox.Designer.CodeGeneration
{
    /// <summary>
    /// Represents an abstract code generator factory
    /// </summary>
    internal interface ICodeGeneratorFactory
    {
        /// <summary>
        /// Creates the code generator based on the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        ICodeGenerator CreateGenerator(Language language);
    }
}
