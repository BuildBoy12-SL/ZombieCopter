// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ZombieCopter
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public sealed class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the minimum rotation required by a zombie to trigger flight.
        /// </summary>
        [Description("The minimum rotation required by a zombie to trigger flight.")]
        public int MinimumRotation { get; set; } = 180;

        /// <summary>
        /// Gets or sets the amount of seconds between each cooldown reset.
        /// </summary>
        [Description("The amount of seconds between each cooldown reset.")]
        public int SecondsPerInterval { get; set; } = 5;

        /// <summary>
        /// Gets or sets the maximum amount of boosts a zombie can trigger per interval.
        /// </summary>
        [Description("The maximum amount of boosts a zombie can trigger per interval.")]
        public int MaximumBoosts { get; set; } = 2;

        /// <summary>
        /// Gets or sets the amount of units a zombie will be boosted upwards.
        /// </summary>
        [Description("The amount of units a zombie will be boosted upwards.")]
        public int BoostedUnits { get; set; } = 3;

        /// <summary>
        /// Gets or sets the hint a zombie will receive when they spawn.
        /// </summary>
        [Description("The hint a zombie will receive when they spawn.")]
        public string Hint { get; set; } = "You feel the urge to fly...";

        /// <summary>
        /// Gets or sets the duration of the hint.
        /// </summary>
        [Description("The duration of the hint.")]
        public int HintDuration { get; set; } = 5;

        /// <summary>
        /// Gets or sets a value indicating whether the fall damage sound should be played when a zombie takes flight.
        /// </summary>
        [Description("Whether the fall damage sound should be played when a zombie takes flight.")]
        public bool PlayFlyNoise { get; set; } = true;
    }
}