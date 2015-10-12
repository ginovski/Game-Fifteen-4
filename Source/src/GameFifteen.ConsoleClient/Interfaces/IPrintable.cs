// <copyright file="IPrintable.cs" company="GameFifteen4Team">
// Copyright(c) 2015 Team "Game-Fifteen-4"
// </copyright>
// <summary>
// Iprintable Interface
// </summary>
// <author>GameFifteen4Team</author>
namespace GameFifteen.ConsoleClient.Interfaces
{
    /// <summary>
    /// The IPrintable strategy interface
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// The method required for a class to be IPrintable
        /// </summary>
        /// <returns>A string value</returns>
        string ToPrintable();    
    }
}
