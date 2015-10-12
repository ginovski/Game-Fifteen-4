// <copyright file="Dirs.cs" company="GameFifteen4Team">
// Copyright(c) 2015 Team "Game-Fifteen-4"
// </copyright>
// <summary>
// Static class Constants
// </summary>
// <author>GameFifteen4Team</author>
namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Linq;

    /// <summary>Represents directions.</summary>
    public static class Dirs
    {
        /// <summary>The possible directions.</summary>
        public static readonly Point[] GetDirection =
        {
            new Point(0, 1), new Point(0, -1), new Point(1, 0),
            new Point(-1, 0)
        };
    }
}