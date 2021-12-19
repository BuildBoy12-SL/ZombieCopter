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
            if (!sender.CheckPermission("zc.bypass"))
            {
                response = "Insufficient permission. Required: zc.bypass";
                return false;
            }

            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "This command must be executed from the game level.";
                return false;
            }

            if (arguments.Count > 0)
            {
                if (!(Player.Get(arguments.At(0)) is Player target))
                {
                    response = $"Could not find a player from '{arguments.At(0)}'";
                    return false;
                }

                player = target;
            }

            if (player.Role != RoleType.Scp0492)
            {
                response = $"The target ({player.Nickname}) must be a Scp049-2 to use this command on them.";
                return false;
            }

            if (!player.GameObject.TryGetComponent(out ZombieComponent targetZombieComponent))
            {
                response = $"The target ({player.Nickname}) does not have an attached {nameof(ZombieComponent)}.";
                return false;
            }

            targetZombieComponent.HasCooldown = !targetZombieComponent.HasCooldown;
            response = targetZombieComponent.HasCooldown
                ? $"The cooldown of {player.Nickname} has been enabled."
                : $"The cooldown of {player.Nickname} has been disabled.";
            return true;
        }
    }
}