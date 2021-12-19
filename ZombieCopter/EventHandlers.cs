// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ZombieCopter
{
    using Exiled.Events.EventArgs;
    using UnityEngine;
    using ZombieCopter.Components;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnChangingRole(ChangingRoleEventArgs)"/>
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.Scp0492)
            {
                ev.Player.GameObject.AddComponent<ZombieComponent>();
                return;
            }

            if (ev.Player.GameObject.TryGetComponent(out ZombieComponent zombieComponent))
                Object.Destroy(zombieComponent);
        }
    }
}