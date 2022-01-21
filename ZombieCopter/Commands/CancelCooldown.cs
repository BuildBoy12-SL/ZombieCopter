// -----------------------------------------------------------------------
// <copyright file="CancelCooldown.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ZombieCopter.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using ZombieCopter.Components;

    /// <summary>
    /// A command to toggle the <see cref="ZombieComponent.HasCooldown"/> property of zombies.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CancelCooldown : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "copterbypass";

        /// <inheritdoc />
        public string[] Aliases { get; } = { "cb" };

        /// <inheritdoc />
        public string Description { get; } = "Allows a user to bypass the boost limit.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null && !sender.CheckPermission("zc.bypass"))
            {
                response = "Insufficient permission. Required: zc.bypass";
                return false;
            }

            if (player == null)
                return HandleServerCommand(arguments, out response);

            return HandlePlayerCommand(arguments, player, out response);
        }

        private bool HandleServerCommand(ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count == 0 || string.IsNullOrEmpty(arguments.At(0)))
            {
                response = "You must specify a target to bypass.";
                return false;
            }

            Player player = Player.Get(arguments.At(0));
            if (player == null)
            {
                response = "Unable to find a player with the specified parameters.";
                return false;
            }

            if (!player.GameObject.TryGetComponent(out ZombieComponent zombieComponent))
            {
                response = $"The target player ({player.Nickname}) does not have attached copter logic.";
                return false;
            }

            zombieComponent.HasCooldown = !zombieComponent.HasCooldown;
            response = zombieComponent.HasCooldown
                ? $"The cooldown of {player.Nickname} has been enabled."
                : $"The cooldown of {player.Nickname} has been disabled.";

            return true;
        }

        private bool HandlePlayerCommand(ArraySegment<string> arguments, Player sender, out string response)
        {
            if (arguments.Count != 0 || string.IsNullOrEmpty(arguments.At(0)))
                return HandleServerCommand(arguments, out response);

            if (!sender.GameObject.TryGetComponent(out ZombieComponent zombieComponent))
            {
                response = $"You do not have attached copter logic.";
                return false;
            }

            zombieComponent.HasCooldown = !zombieComponent.HasCooldown;
            response = zombieComponent.HasCooldown
                ? $"Cooldown enabled."
                : $"Cooldown disabled.";

            return true;
        }
    }
}